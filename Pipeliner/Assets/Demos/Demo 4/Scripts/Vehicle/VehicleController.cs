using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct GroundData
{
    public bool HasGround;
    public Vector3 Normal;
    public Vector3 Point;
}

public struct VehicleInputs
{
    public float Throttle;
    public float Brake;
    public float Lean;
}

[Serializable]
public struct VehicleDrive
{
    public float Speed;
    public float Acceleration;
}

[Serializable]
public struct VehicleBrake
{
    public float Force;
}

[Serializable]
public struct VehicleLean
{
    public float Speed;
    public float Acceleration;
}

public class VehicleController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public WheelCollider[] Wheels;

    [Space] 
    public VehicleDrive Drive = new VehicleDrive{Speed = 10f, Acceleration = 2f};
    public VehicleBrake Brake = new VehicleBrake{Force = 10f};
    public VehicleLean Lean = new VehicleLean{Speed = 10f, Acceleration = 1f};
    
    public GroundData GroundData { get; private set; }
    public VehicleInputs Inputs { get; private set; }
    
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

    public void SetInputs(ref VehicleInputs inputs)
    {
        Inputs = inputs;
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

        GetDrive();
        GetBrake();
        GetLean(deltaTime);
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

    /*private void ProbeGround(ref GroundData groundData)
    {
        var ray = new Ray(Collider.bounds.center, -Collider.transform.up);
        var radius = Collider.radius - 0.1f;
        var distance = Mathf.Abs(radius - Collider.radius) + 0.2f;
        
        var results = new RaycastHit[8];
        //var count = Physics2D.RaycastNonAlloc(ray.origin, ray.direction, results, distance);
        var count = Physics.SphereCastNonAlloc(ray.origin, radius, ray.direction, results, distance);

        var closest = float.MaxValue;
        for (int i = 0; i < count; i++)
        {
            if (results[i].rigidbody == Rigidbody || results[i].collider.isTrigger || results[i].distance > closest)
                continue;
            
            groundData.HasGround = true;
            groundData.Normal = results[i].normal;
            groundData.Point = results[i].point;
            
            closest = results[i].distance;
        }
    }*/

    private void GetDrive()
    {
        if (!GroundData.HasGround || !(Inputs.Throttle > 0f))
            return;

        var targetSpeed = Drive.Speed * Inputs.Throttle;

        for (int i = 0; i < Wheels.Length; i++)
        {
            var isGrounded = Wheels[i].GetGroundHit(out var wheelHit);
                
            if (!isGrounded)
                continue;

            var forward = wheelHit.forwardDir;
            var velocity = PointVelocity(wheelHit.point);
            var velocityDiff = forward * targetSpeed - velocity;
            var force = velocityDiff * Drive.Acceleration;
            
            var load = Wheels[i].sprungMass;

            Rigidbody.AddForceAtPosition(force * load, wheelHit.point);
        }
    }
    
    private void GetBrake()
    {
        if (!GroundData.HasGround || !(Inputs.Brake > 0f))
            return;
        
        for (int i = 0; i < Wheels.Length; i++)
        {
            var isGrounded = Wheels[i].GetGroundHit(out var wheelHit);
            if (!isGrounded)
                continue;

            var velocity = PointVelocity(wheelHit.point);
                
            var load = Wheels[i].sprungMass;
            var force = Vector3.ClampMagnitude(-Vector3.ProjectOnPlane(velocity, wheelHit.normal), 1f) * load * Brake.Force * Inputs.Brake;

            Rigidbody.AddForceAtPosition(force, wheelHit.point);
        }
    }

    private void GetLean(float deltaTime)
    {
        if (!(Mathf.Abs(Inputs.Lean) > 0f))
            return;
        
        var axis = -Vector3.forward;
        var force = Inputs.Lean * Lean.Speed;
        var velocity = Rigidbody.angularVelocity;
        var velocityDiff = force - Vector3.Dot(velocity, axis);
        var torque = velocityDiff * Lean.Acceleration;
        Rigidbody.AddTorque(axis * torque * Rigidbody.mass);
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
