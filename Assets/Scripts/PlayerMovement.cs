using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public CharacterController controller;
    Vector3 velocity;
    bool hitGround = false;
    public float GroundDistance;
    public float jumpHeight = 3f;
    public float distToGround = 1f;
    public Vector3 offsetRaycast;
    public Camera maincamera;
    public float gravity = -19.6f;
    void FixedUpdate()
    {
        playerMovement();
        playergravity();
    }

    void playerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (hitGround && Input.GetKey(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        hitGround = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    void playergravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (hitGround)
        {
            velocity.y = 0;
        }
    }
}
