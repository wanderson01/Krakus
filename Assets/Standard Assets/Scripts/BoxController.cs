using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour {
	
	
	public float speed = 2.5f;
	public float jumpPower = 23f;
	public float gravity = 50.0f;
	public bool jumping = false;
	public string action = "";
	private Vector2 moveDirection = Vector2.zero;
	private CharacterController controller;
	private float verticalPower = 0;
	private Animator _animator;
	private bool canWallJump;
	private RaycastHit walljumpRaycastHit;
	private Vector3 temp;
	private float tempPosition;
	void Start ()
	{
		controller  = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
		
	}
	
	/*void OnControllerColliderHit (ControllerColliderHit col){
		
		moveDirection *= 0;
		print(moveDirection);
		transform.up = Vector3.Slerp (transform.up, col.transform.up, 10 * Time.deltaTime);
	}*/
	
	void Update() {
		
		//canWallJump = GetComponentInChildren<JumpWall>().CanWallJump;
		CharacterOrientation();
		teste ();
		
		if (IsGrounded()) {
			
			moveDirection *= 0;
			jumping = false;
			action = "stand";
			if (Input.GetButtonDown ("Jump")){
				
				verticalPower = jumpPower;
				jumping = true;
				action = "jump";
				//_animator.SetBool("jump", false);
			}
		}
		else if (CanWallJump()){
			
			jumping = false;
			if (Input.GetButtonDown ("Jump")){
				if (action != "backjump"){
					action = "wallJump";
				}
				
				verticalPower = jumpPower;
				transform.eulerAngles = new Vector2(0,180);
				moveDirection = new Vector2(-Input.GetAxisRaw ("Horizontal"), 0);
				moveDirection *= speed;
				//_animator.SetBool("jump", false);
			}
		}
		
		if (action == "wallJump") {
			if (DistanceBetweenObjects (temp - transform.position) > 60) {
				jumping = true;
				action = "backjump";
			}
		}
		
		if (action == "backjump"){
			if (Input.GetButton("Horizontal")){
				moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
				moveDirection *= speed + 10;
				action = "jump";
			}
			
		}
		if (action == "jump"){
			
			moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			moveDirection *= speed;
		}
		
		verticalPower -= gravity * Time.deltaTime;
		moveDirection.y = verticalPower;
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	void CharacterOrientation(){
		
		if (transform.position.x > tempPosition) {
			transform.eulerAngles = new Vector2(0,0);
		}
		else if (transform.position.x < tempPosition){
			transform.eulerAngles = new Vector2(0, 180);
		}
		
		tempPosition = transform.position.x;
		
		/*
		if (Input.GetAxisRaw("Horizontal") > 0){
			transform.eulerAngles = new Vector2(0,0);
		}
		if (Input.GetAxisRaw("Horizontal") < 0){
			transform.eulerAngles = new Vector2(0,180);
		}*/
	}
	
	bool IsGrounded(){
		
		RaycastHit raycastHit;
		var down = -(transform.TransformDirection(Vector3.up));
		Debug.DrawRay(transform.position, down * 0.5f, Color.yellow);
		
		if (Physics.Raycast(transform.position, down, out raycastHit, 0.5f)){
			if(raycastHit.collider.tag == "Map"){
				return true;
			}
			else {
				return false;
			}
		}
		else {
			return false;
		}
	}
	
	bool CanWallJump(){
		
		var right = transform.TransformDirection(Vector3.right);
		Debug.DrawRay(transform.position, right * 0.5f, Color.yellow);
		
		if (Physics.Raycast(transform.position, right, out walljumpRaycastHit, 0.5f)){
			print (walljumpRaycastHit.collider.name);
			if(walljumpRaycastHit.collider.tag == "Map"){ 
				temp = walljumpRaycastHit.point;
				return true;
			}
			else {
				return false;
			}
		}
		else {
			return false;
		}
	}
	
	float DistanceBetweenObjects (Vector2 ObjectPosition){
		
		float distance = Vector2.Distance (ObjectPosition, transform.position);
		print (distance);
		return distance;
	}
	
	void teste(){
		//DistanceBetweenObjects (temp - transform.position);
		//Debug.DrawRay(transform.position, temp - transform.position, Color.red);
		
		/*var right = transform.TransformDirection(Vector3.right);
		Debug.DrawRay(transform.position, right * 0.5f, Color.yellow);
		if (Physics.Raycast(transform.position, right, out walljumpRaycastHit, 0.5f)){

		}*/
	}
}
