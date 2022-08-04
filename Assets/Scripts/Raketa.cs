using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raketa : MonoBehaviour
{
    public float angleChangingSpeed;
    public float movementSpeed;

    private Transform target;
    public Transform Target
    {
        set
        {
            target = value;
        }
        get
        {
            return target;
        }
    }
    void Update()
    {
        Vector2 direction = (Vector2)Target.position - (Vector2)transform.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        GetComponent<Rigidbody2D>().angularVelocity = -angleChangingSpeed * rotateAmount;
        GetComponent<Rigidbody2D>().velocity = transform.up * movementSpeed;
    }
}
