using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayForwardDirection : MonoBehaviour
{
    [SerializeField]
    private Color gizmoColor = Color.blue; // Color for the arrow gizmo

    [SerializeField]
    private float gizmoSize = 1f; // Size of the arrow gizmo

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Set the gizmo color
        Gizmos.color = gizmoColor;

        // Calculate the forward direction in world space
        Vector3 forwardDirection = transform.TransformDirection(Vector3.forward) * gizmoSize;

        // Draw the arrow gizmo
        Gizmos.DrawRay(transform.position, forwardDirection);
    }
#endif
}
