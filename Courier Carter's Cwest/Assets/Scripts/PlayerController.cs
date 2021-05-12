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
	public int levelsCompleted = 1;
	private bool canDub = false;
	private bool canDash = false;
	private bool canSpecial = false;
	private bool hasDashed = false;
	public bool reloaded = false;
	private bool dubPower = false;
	private bool dashPower = false;

	private Vector3 moveDirection;
	private Vector3 respawn;
	public float gravityScale;
	private bool dead = false;
	float time;
	Text finalTime;


	// Start is called before the first frame update
	void Start()
	{
		finalTime = GetComponent<Text>();
		//RB = GetComponent<Rigidbody>();
		charController = GetComponent<CharacterController>();
	}


	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
		string seconds = (time % 60).ToString("00");
		//finalTime.text = minutes + ":" + seconds;

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
			if (hasDashed)
			{
				moveSpeed = moveSpeed / 4;
				hasDashed = false;
			}
			canDub = false;
			canDash = false;
			canSpecial = true;
			moveDirection.y = 0f;
			if (Input.GetButton("Jump"))
			{
				moveDirection.y = jumpForce;
			}

		}
		if (Input.GetButtonUp("Jump"))
		{
			if ((canSpecial) && (dubPower))
			{
				canDub = true;
				Debug.Log("dubtru");
			}
			if ((canSpecial) && (dashPower))
			{
				canDash = true;
				Debug.Log("dashtru");
			}
		}

		if (canDub)
		{
			if (Input.GetButton("Jump"))
			{
				moveDirection.y = jumpForce;
				canDub = false;
				canSpecial = false;
			}
		}
		if (canDash)
		{
			if (Input.GetButton("Jump"))
			{
				moveSpeed = moveSpeed * 4;
				canDash = false;
				canSpecial = false;
				hasDashed = true;
			}
		}
		
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
		if (dead)
		{

			//Debug.Log(transform.position);
			charController.enabled = false;
			charController.transform.position = respawn;
			if (charController.transform.position == respawn)
			{
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

		if (other.tag == "Checkpoint")
		{
			respawn = other.transform.position;
			Debug.Log(respawn);
		}
		if (other.tag == "Death")
		{
			//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			dead = true;
			Debug.Log("touched death" + respawn);
		}
		if (other.tag == "Finish")
		{
			Debug.Log(time);
			SceneManager.LoadScene(sceneName: "Level 1 - City");
			levelsCompleted++;
			//displayText.text = "You win!";
			Debug.Log("touched finish");
		}
		if (other.tag == "Level1")
		{
			SceneManager.LoadScene(sceneName: "Level 1 - City");
			Debug.Log("touched level 1 portal");
		}
		if (other.tag == "DubJump")
		{
			if(!(dubPower)){
			dubPower = true;
			} else {
				dubPower = false;
				dashPower = true;
			}
			Destroy(other.gameObject);
		}
	}
}
