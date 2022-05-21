using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demos.Demo4
{
    public struct GroundData
    {
        public bool HasGround;
        public Vector3 Normal;
        public Vector3 Point;
    }

    public class VehicleInputs
    {
        public float Throttle;
        public float Brake;
        public float Lean;
    }

    public abstract class ControllerModule
    {
        public bool Enabled = true;

        public VehicleController Controller { get; private set; }

        public virtual void SetupModule(VehicleController controller)
        {
            Controller = controller;
        }

        public abstract void UpdateModule(float deltaTime);
    }

    [Serializable]
    public class VehicleInput : ControllerModule
    {
        public VehicleInputs Inputs { get; private set; }

        public override void SetupModule(VehicleController controller)
        {
            base.SetupModule(controller);

            Inputs = new VehicleInputs();
        }

        public void SetInputs(ref VehicleInputs inputs)
        {
            if (!Enabled)
            {
                inputs = new VehicleInputs();
            }
            
            inputs.Throttle = Mathf.Clamp01(inputs.Throttle);
            inputs.Brake = Mathf.Clamp01(inputs.Brake);
            inputs.Lean = Mathf.Clamp(inputs.Lean, -1f, 1f);
            
            Inputs = inputs;
        }
        
        public override void UpdateModule(float deltaTime)
        {
            /*if (!Enabled)
            {
                Inputs = new VehicleInputs();
            }*/
        }
    }

    [Serializable]
    public class VehicleDrive : ControllerModule
    {
        [Space]
        public float Speed;
        public float Acceleration;
        
        public override void UpdateModule(float deltaTime)
        {
            if (!Enabled || !Controller.GroundData.HasGround || !(Controller.Input.Inputs.Throttle > 0f))
                return;

            var targetSpeed = Speed * Controller.Input.Inputs.Throttle;

            for (int i = 0; i < Controller.Wheels.Length; i++)
            {
                var isGrounded = Controller.Wheels[i].GetGroundHit(out var wheelHit);
                    
                if (!isGrounded)
                    continue;

                var forward = wheelHit.forwardDir;
                var velocity = Controller.PointVelocity(wheelHit.point);
                var velocityDiff = forward * targetSpeed - velocity;
                var force = velocityDiff * Acceleration;
                
                var load = Controller.Wheels[i].sprungMass;

                Controller.Rigidbody.AddForceAtPosition(force * load, wheelHit.point);
            }
        }
    }

    [Serializable]
    public class VehicleBrake : ControllerModule
    {
        [Space]
        public float Force;
        
        public override void UpdateModule(float deltaTime)
        {
            if (!Enabled || !Controller.GroundData.HasGround || !(Controller.Input.Inputs.Brake > 0f))
                return;
            
            for (int i = 0; i < Controller.Wheels.Length; i++)
            {
                var isGrounded = Controller.Wheels[i].GetGroundHit(out var wheelHit);
                if (!isGrounded)
                    continue;

                var velocity = Controller.PointVelocity(wheelHit.point);
                    
                var load = Controller.Wheels[i].sprungMass;
                var force = Vector3.ClampMagnitude(-Vector3.ProjectOnPlane(velocity, wheelHit.normal), 1f) * load * Force * Controller.Input.Inputs.Brake;

                Controller.Rigidbody.AddForceAtPosition(force, wheelHit.point);
            }
        }
    }

    [Serializable]
    public class VehicleLean : ControllerModule
    {
        [Space]
        public float Speed;
        public float Acceleration;
        
        public override void UpdateModule(float deltaTime)
        {
            if (!Enabled || !(Mathf.Abs(Controller.Input.Inputs.Lean) > 0f))
                return;
            
            var axis = -Vector3.forward;
            var force = Controller.Input.Inputs.Lean * Speed;
            var velocity = Controller.Rigidbody.angularVelocity;
            var velocityDiff = force - Vector3.Dot(velocity, axis);
            var torque = velocityDiff * Acceleration;
            Controller.Rigidbody.AddTorque(axis * torque * Controller.Rigidbody.mass);
        }
    }

    public class VehicleController : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public WheelCollider[] Wheels;

        [Space] 
        public VehicleInput Input = new VehicleInput();
        public VehicleDrive Drive = new VehicleDrive{Speed = 10f, Acceleration = 2f};
        public VehicleBrake Brake = new VehicleBrake{Force = 10f};
        public VehicleLean Lean = new VehicleLean{Speed = 10f, Acceleration = 1f};
        
        public GroundData GroundData { get; private set; }
        public Collider[] LocalColliders { get; private set; }
            
        public Transform Transform => Rigidbody.transform;
        public Vector3 Position
        {
            get => Rigidbody.position;
            set => Rigidbody.MovePosition(value);
        }
        public Quaternion Rotation
        {
            get => Rigidbody.rotation;
            set => Rigidbody.MoveRotation(value);
        }
        public Vector3 Velocity
        {
            get => Rigidbody.velocity;
            set => Rigidbody.velocity = value;
        }
        public Vector3 AngularVelocity
        {
            get => Rigidbody.angularVelocity;
            set => Rigidbody.angularVelocity = value;
        }
        public float Mass => Rigidbody.mass;
        public Vector3 LocalVelocity => Transform.InverseTransformVector(Velocity);
        public Vector3 PointVelocity(Vector3 point) => Rigidbody.GetPointVelocity(point);

        private void OnValidate()
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody>();
        }

        private void Awake()
        {
            GroundData = new GroundData();
            
            Input.SetupModule(this);
            Drive.SetupModule(this);
            Brake.SetupModule(this);
            Lean.SetupModule(this);
        }

        private void Start()
        {
            LocalColliders = FindLocalColliders();
            
            Rigidbody.maxAngularVelocity *= 2f;
                
            // Ignore collisions between wheels and local colliders.
            for (int i = 0; i < Wheels.Length; i++)
            {
                for (int j = 0; j < LocalColliders.Length; j++)
                {
                    Physics.IgnoreCollision(Wheels[i], LocalColliders[j]);
                }
            }
        }

        private Collider[] FindLocalColliders()
        {
            var allColliders = new List<Collider>(Transform.GetComponentsInChildren<Collider>());
            var colliders = new List<Collider>();

            // Ignore wheel colliders
            for (int i = 0; i < allColliders.Count; i++)
            {
                //Debug.Log("Collider: " + allColliders[i].name);
                if (allColliders[i] is WheelCollider)
                {
                    //Debug.Log("Removed Wheel Collider");
                    continue;
                }
                    
                colliders.Add(allColliders[i]);
            }
                
            return colliders.ToArray();
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.deltaTime;

            for (int i = 0; i < Wheels.Length; i++)
            {
                // Weird bug fix
                Wheels[i].motorTorque = 0.00001f;
                //Wheels[i].brakeTorque = 0.00001f;
            }
            
            // Update Ground
            var groundData = new GroundData();
            ProbeGround(ref groundData);
            GroundData = groundData;

            Input.UpdateModule(deltaTime);
            Drive.UpdateModule(deltaTime);
            Brake.UpdateModule(deltaTime);
            Lean.UpdateModule(deltaTime);
        }
        
        private void ProbeGround(ref GroundData groundData)
        {
            var hasGround = false;
            var normal = Vector3.zero;
            var point = Vector3.zero;

            var count = 0;
            var inv = 1f / Wheels.Length;
            
            for (int i = 0; i < Wheels.Length; i++)
            {
                if (!Wheels[i].isGrounded)
                    continue;
                
                Wheels[i].GetGroundHit(out var hit);

                hasGround = true;
                normal += hit.normal;
                point += hit.point;

                count++;
            }

            if (count > 0)
            {
                normal = (normal / count).normalized;
                point = point / count;
            }

            groundData.HasGround = hasGround;
            groundData.Point = point;
            groundData.Normal = normal;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying || !GroundData.HasGround)
                return;
            
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(GroundData.Point, GroundData.Normal);
            Gizmos.DrawSphere(GroundData.Point, 0.01f);
        }
    }
}