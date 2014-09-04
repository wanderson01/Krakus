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
		Move ();
	}

	void Move(){

		if (raycast.IsGrounded() && !attacking) {
			
			state = State.Stand;
			
			if (Input.GetButtonDown ("Jump")){
				
				_animator.SetBool("jump", true);
				verticalPower = jumpPower;
			}
		}
		
		else if (!raycast.IsGrounded() && state != State.WallJump && 
		         						  state != State.BackJump &&
		                                  state != State.AttackJump){
			state = State.Jump;
		}

		switch (state){
		
		case State.Stand:

			_animator.SetBool("jump", false);
			_animator.SetFloat("walk", Mathf.Abs(Input.GetAxis("Horizontal")));
			moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			moveDirection *= speed;
			break;

		case State.Jump:

			_animator.SetBool("jump", true);
			moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			moveDirection *= speed;

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
			break;

		case State.WallJump:

			if (DistanceFromObject (raycast.raycastPoint) > 8) {
				state = State.BackJump;
			}
			break;

		case State.BackJump:

			if (Input.GetButton("Horizontal")){
				moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
				moveDirection *= speed + 10;
				state = State.Jump;
			}
			break;
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

			switch (state){
			case State.Stand:
				AttackGround();
				break;

			case State.AttackGround:
				AttackGround();
				break;

			case State.Jump:
				AttackJump();
				break;

			case State.AttackJump:
				AttackJump();
				break;

			case State.WallJump:
				AttackJump();
				break;

			case State.BackJump:
				AttackJump();
				break;
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
}
