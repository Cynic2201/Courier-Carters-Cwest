using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
public float turnSpeed = 4.0f;
public float moveSpeed = 2.0f;
 
public float minTurnAngle = -90.0f;
public float maxTurnAngle = 90.0f;
private float rotX;
public Rigidbody RB;
public float jumpForce;

void Start()
   {
        RB = GetComponent<Rigidbody>();
        
   }
void Update ()
	{
    MouseAiming();
   // KeyboardMovement();
   RB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, RB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
   transform.Translate(RB.velocity * moveSpeed * Time.deltaTime);
	 if (Input.GetButtonDown("Jump"))
        {
            RB.velocity = new Vector3(RB.velocity.x, jumpForce, RB.velocity.z);
        }
	}
 
void MouseAiming ()
	{
    // get the mouse inputs
    float y = Input.GetAxis("Mouse X") * turnSpeed;
    //rotX += Input.GetAxis("Mouse Y") * turnSpeed;
	rotX = 1;
 
    // clamp the vertical rotation
    //rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
 
    // rotate the camera
    transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
	}
 
/*void KeyboardMovement ()
	{
    Vector3 dir = new Vector3(0, 0, 0);
 
    dir.x = Input.GetAxis("Horizontal");
    dir.z = Input.GetAxis("Vertical");
	
    transform.Translate(dir * moveSpeed * Time.deltaTime);
	}
	*/
}