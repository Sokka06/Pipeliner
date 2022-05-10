using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public void GetSpawn(out Vector3 position, out Quaternion rotation)
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    private void OnDrawGizmos()
    {
        var color = Color.green;
        color.a *= 0.5f;
        Gizmos.color = color;
        
        GetSpawn(out var position, out var rotation);
        Gizmos.DrawRay(position, rotation * Vector3.forward);
        
        Gizmos.DrawCube(position, transform.localScale);
    }
}
