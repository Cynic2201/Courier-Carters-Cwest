using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float sprintMultiplyer;
    public float jumpForce;
    //public Rigidbody RB;
    public CharacterController charController;
	public Text displayText;
	public int levelsCompleted = 0;
	public bool canDub = true;
	public bool canDub2 = false;
	public bool reloaded = false;

    private Vector3 moveDirection;
	private Vector3 respawn;
    public float gravityScale;
	private bool dead = false;
    
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

        if (Input.GetKey(KeyCode.LeftShift)) moveDirection = (transform.forward * Input.GetAxis("Vertical") * moveSpeed * sprintMultiplyer) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed);
        else moveDirection = moveDirection.normalized * moveSpeed;

        moveDirection.y = yStore;
		
		

        if (charController.isGrounded)
        {
			canDub = false;
			canDub2 = true;
            moveDirection.y = 0f;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
			
        }
		if(Input.GetButtonUp("Jump")){
			if((canDub2) && levelsCompleted == 1){
				canDub = true;
				Debug.Log("dubtru");
			}
			}
			
	    if(canDub){
			if(Input.GetButton("Jump"))
			{
				moveDirection.y  = jumpForce;
				canDub = false;
				canDub2 = false;
			}
		}
        /*else if (canDub){
			if(Input.GetButtonUp("Jump")){
				canDub2 = true;
				Debug.Log("dub2");
			}
			Debug.Log(canDub2);
			if(canDub2){
				Debug.Log("firstiftrue");
			 if (Input.GetButtonDown("Jump"))
			 {
                moveDirection.y = jumpForce;
				Debug.Log("doublejumped");
			 }
			 canDub2 = false;
			 canDub = false;
			}
		}*/

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        charController.Move(moveDirection * Time.deltaTime);
		
		if (dead) { 
		
		//Debug.Log(transform.position);
		charController.enabled = false;
		charController.transform.position = respawn;
		if(charController.transform.position == respawn){
		charController.enabled = true;
		dead = false;
		reloaded = false;
		Debug.Log("Thinks it worked");
		}
		//Debug.Log("seen dead");
		//Debug.Log(transform.position);
		}
    }
	
		private void OnTriggerEnter(Collider other)
	{
		
		if(other.tag == "Checkpoint"){
			respawn = other.transform.position;
			Debug.Log(respawn);
		}
		if(other.tag == "Death"){
			//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			dead = true;
			Debug.Log("touched death" + respawn);
		}
		if(other.tag == "Finish"){
			SceneManager.LoadScene(sceneName: "Level 1 - City");
			levelsCompleted++;
			//displayText.text = "You win!";
			Debug.Log("touched finish");
		}
		if(other.tag == "Level1"){
			SceneManager.LoadScene(sceneName: "Level 1 - City");
			Debug.Log("touched level 1 portal");
		}
	}
}
