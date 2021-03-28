using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float sprintMultiplyer;
    public float jumpForce;
    //public Rigidbody RB;
    public CharacterController charController;

    private Vector3 moveDirection;
    public float gravityScale;
    
    // Start is called before the first frame update
    void Start()
    {
        //RB = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //RB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, RB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

        /*if (Input.GetButtonDown("Jump"))
        {
            RB.velocity = new Vector3(RB.velocity.x, jumpForce, RB.velocity.z);
        }*/

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.LeftShift)) moveDirection = moveDirection.normalized * moveSpeed * sprintMultiplyer;
        else moveDirection = moveDirection.normalized * moveSpeed;

        moveDirection.y = yStore;

        if (charController.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        charController.Move(moveDirection * Time.deltaTime);
    }
}
