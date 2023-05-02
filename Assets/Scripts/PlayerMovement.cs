using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    private Rigidbody rb;
    private GroundChecker groundChecker;
    
    private Vector3 moveDirection;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 jumpVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        groundChecker = gameObject.GetComponentInChildren<GroundChecker>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * Time.deltaTime);

        if(groundChecker.isGrounded)
        {
            Physics.gravity = new Vector3(0, -9.8f, 0);

            if (Input.GetKeyDown("space"))
            {
                jumpVector = rb.velocity;
                jumpVector.y = 10f;
                rb.velocity = jumpVector;
            }
        }
        else
        {
            if(Input.GetKey("space"))
            {
                Physics.gravity = new Vector3(0, -15f, 0);
            }
            else
            {
                Physics.gravity = new Vector3(0, -20f, 0);
            }
        }
    }
}