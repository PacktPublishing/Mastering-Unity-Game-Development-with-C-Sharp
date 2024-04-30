using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelfRotation : MonoBehaviour
{
    public float Speed;

    public void Update()
    {
        transform.Rotate(Vector3.up, Speed * Time.deltaTime);
    }
}
