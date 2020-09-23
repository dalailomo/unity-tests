using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    public float impulseForceY = 0f;
    public float impulseForceX = 0f;
    public bool canJump = true;

    private Rigidbody rb;
    private float accelerationForce = 10.0f;
    private float impulseForceMultiplier = 2f;
    private float maxVelocity = 8f;
    private float canJumpThreshold = 0.5f;
    private string moveLeftKey = "a";
    private string moveRightKey = "d";
    private string moveUpKey = "w";
    private string moveDownKey = "s";
    private string jumpKey = "space";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // x axis
        if (Input.GetKey(moveRightKey) && rb.velocity.x < maxVelocity) rb.AddForce(accelerationForce, 0, 0, ForceMode.Acceleration);
        if (Input.GetKey(moveLeftKey) && rb.velocity.x > -maxVelocity) rb.AddForce(-accelerationForce, 0, 0, ForceMode.Acceleration);

        // z axis
        if (Input.GetKey(moveUpKey) && rb.velocity.z < maxVelocity) rb.AddForce(0, 0, accelerationForce, ForceMode.Acceleration);
        if (Input.GetKey(moveDownKey) && rb.velocity.z > -maxVelocity) rb.AddForce(0, 0, -accelerationForce, ForceMode.Acceleration);

        // y axis
        if (Input.GetKey(jumpKey) && canJump)
        {
            rb.AddForce(impulseForceX, impulseForceY, 0, ForceMode.Impulse);
            canJump = false;
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red);

            if (contact.normal.y > canJumpThreshold)
            {
                canJump = true;
                impulseForceY = contact.normal.y * impulseForceMultiplier;
                impulseForceX = contact.normal.x * impulseForceMultiplier / 2;
            }


        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if (contact.normal.y < canJumpThreshold)
            {
                canJump = false;
                impulseForceY = contact.normal.y * impulseForceMultiplier;
                impulseForceX = contact.normal.x * impulseForceMultiplier / 2;
            }
        }
    }
}
