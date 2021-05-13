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
	public float time;
	Text finalTime;


	void Start()
	{
		finalTime = GetComponent<Text>();
		charController = GetComponent<CharacterController>();
	}


	void Update()
	{
		time += Time.deltaTime;
		string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
		string seconds = (time % 60).ToString("00");
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
		
		moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
		charController.Move(moveDirection * Time.deltaTime);
		if (dead)
		{
			charController.enabled = false;
			charController.transform.position = respawn;
			if (charController.transform.position == respawn)
			{
				charController.enabled = true;
				dead = false;
				reloaded = false;
				Debug.Log("Thinks it worked");
			}

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
			dead = true;
			Debug.Log("touched death" + respawn);
		}
		if (other.tag == "Finish")
		{
			Debug.Log(time);
			if(time < 80){
			SceneManager.LoadScene(sceneName: "Level 2 - Temple");
			} else { 
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			levelsCompleted++;
			Debug.Log("touched finish");
		}
		if (other.tag == "Finish2")
		{
			Debug.Log(time);
			if(time < 60){
			SceneManager.LoadScene(sceneName: "Level 3 - Volcano");
			} else { 
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			levelsCompleted++;
			Debug.Log("touched finish");
		}
		if (other.tag == "Finish3")
		{
			Debug.Log(time);
			if(time < 180){
			SceneManager.LoadScene(sceneName: "Level 0 - Hub");
			} else { 
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			levelsCompleted++;
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
