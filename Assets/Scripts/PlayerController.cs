using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float jumpForce = 0.5f;
    private Rigidbody rb;
    private bool isGrounded;

    // Load player.
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Allow for WASD and Spacebar jump movement.
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed;
        rb.AddForce(movement);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Ensure we are on a step surface to enable jumping.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Step"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Step"))
        {
            isGrounded = false;
        }
    }

}
