using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	
	public float speed = 2.5f;
	public float jumpPower = 23f;
	public float gravity = 50.0f;
	public State state;
	public int attackCombo = 0;
	private Vector2 moveDirection = Vector2.zero;
	private CharacterController controller;
	private float verticalPower = 0;
	private Animator _animator;
	private float tempPosition;
	private float newTime = 0;
	private Raycast raycast;
	public bool attacking = false;

	void Start ()
	{
		controller  = GetComponent<CharacterController>();
		raycast = GetComponentInChildren<Raycast> ();
		_animator = GetComponentInChildren<Animator>();
	}

	public enum State{

		Stand,
		Jump,
		WallJump,
		BackJump,
		AttackGround,
		AttackJump,
	}

	void Update() {

		Orientation();
		Attack ();

		if (raycast.IsGrounded() && !attacking) {

			state = State.Stand;
			_animator.SetBool("jump", false);
			_animator.SetFloat("walk", Mathf.Abs(Input.GetAxis("Horizontal")));
			moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			moveDirection *= speed;

			if (Input.GetButtonDown ("Jump")){

				_animator.SetBool("jump", true);
				verticalPower = jumpPower;
				state = State.Jump;
			}
		}
		else if (!raycast.IsGrounded()){

			_animator.SetBool("jump", true);

			if (raycast.CanWallJump() && state == State.Jump){

				if (Input.GetButtonDown ("Jump") && Input.GetAxisRaw("Horizontal") != 0){
					if (state != State.BackJump){
						state = State.WallJump;
					}

					verticalPower = jumpPower;
					moveDirection = transform.TransformDirection(-Vector3.right.x + 0.2f, 0, 0);
					moveDirection *= speed;
				}
			}
		}

		if (state == State.WallJump) {
		
			if (DistanceFromObject (raycast.raycastPoint) > 8) {
				state = State.BackJump;
			}
		}

		if (state == State.BackJump){
			if (Input.GetButton("Horizontal")){
				moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
				moveDirection *= speed + 10;
				state = State.Jump;
			}		
		}

		if (state == State.Jump){
			
			moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			moveDirection *= speed;
		}

		verticalPower -= gravity * Time.deltaTime;
		moveDirection.y = verticalPower;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void Orientation(){
		
		if (transform.position.x > tempPosition) {
			transform.eulerAngles = new Vector2(0,0);
		}
		else if (transform.position.x < tempPosition){
			transform.eulerAngles = new Vector2(0, 180);
		}
		tempPosition = transform.position.x;

	}

	float DistanceFromObject (Vector2 ObjectPosition){

		float distance = Vector2.Distance (ObjectPosition, transform.position);
//		print (distance);
		return distance;
	}

	void Attack(){

		if (Input.GetButtonDown("Fire1")){
			if (raycast.IsGrounded ()) {
				AttackGround();
			}
			else {
				AttackJump();
			}
		}

		if (attackCombo == 0){
			_animator.SetInteger("combo", 0);
		}
	}

	void AttackGround (){

		attacking = true;
		StopWalking();
		state = State.AttackGround;
		attackCombo += 1;
		_animator.SetInteger("combo", attackCombo);
	}

	void AttackJump(){

		state = State.AttackJump;
		attackCombo = 1;
		_animator.SetInteger("combo", attackCombo);
	}

	void StopWalking(){

		moveDirection *= 0;
		_animator.SetFloat("walk", 0);
	}

	/*void teste(){
		//DistanceBetweenObjects (raycast.raycastPoint - transform.position);
		Debug.DrawRay(transform.position, raycast.raycastPoint - transform.position, Color.red);

		var right = transform.TransformDirection(Vector3.right);
		Debug.DrawRay(transform.position, right * 0.5f, Color.yellow);
		if (Physics.Raycast(transform.position, right, out walljumpRaycastHit, 0.5f)){

		}
	}*/
}
