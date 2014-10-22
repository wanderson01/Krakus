using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float jumpForce = 28f;
	public float gravityForce = 60.0f;
	public bool applyGravity = true;
	public float minVerticalPower = -30;
	public ActionState state;
	public int attackCombo = 0;
	public float tempPosition;
	private Raycast raycast;
	public bool attacking = false;
	public int wjCounter;
	public float onMovingPlatformCheckInterval = 0.2f;

	private Transform movingPlatform;
	private Vector2 moveDirection = Vector2.zero;
	private CharacterController controller;
	private float verticalPower = 0;
	private Animator _animator;
	private float gravity;
	private float speed;

	void Start ()
	{
		MoveToStartingPosition ();
		speed = GetComponent<BaseStats> ().movementSpeed;
		tempPosition = transform.position.x;
		controller  = GetComponent<CharacterController>();
		raycast = GetComponentInChildren<Raycast> ();
		_animator = GetComponentInChildren<Animator>();
	}
	
	public enum ActionState{
		
		Stand,
		Jump,
		WallJump,
		BackJump,
		AttackGround,
		AttackJump,
	}
	
	void Update() {

		ApplyGravity ();
		Orientation();
		Attack ();
		Move ();
		StayOnMovingPlatform ();
		StepDownOneWayPlatform ();
	}

	void ApplyGravity(){

		if (applyGravity) {
			gravity = gravityForce;
		}
		else {
			gravity = 0;
		}
	}

	void MoveToStartingPosition(){

		transform.position = new Vector3(GameData.currentGame.SpawnPoint.x, 
		                                 GameData.currentGame.SpawnPoint.y, 
		                                 transform.position.z);
	}
	
	void Move(){

		if (raycast.IsGrounded()){
			wjCounter = 0;
		}

		if (raycast.IsGrounded() && !attacking) {
			
			state = ActionState.Stand;
			
			if (Input.GetButtonDown ("Jump") && !Input.GetKey(KeyCode.S)){
				
				_animator.SetBool("jump", true);
				verticalPower = jumpForce;
				state = ActionState.Jump;
			}
		}
		
		else if (!raycast.IsGrounded() && state != ActionState.WallJump && 
		       						      state != ActionState.BackJump &&
		       							  state != ActionState.AttackJump){
			state = ActionState.Jump;
		}
		
		switch (state){
			
		case ActionState.Stand:

			_animator.SetBool("jump", false);
			_animator.SetFloat("walk", Mathf.Abs(Input.GetAxis("Horizontal")));
			moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			moveDirection *= speed;
			break;
			
		case ActionState.Jump:
			
			_animator.SetBool("jump", true);
			moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			moveDirection *= speed;
			
			WallJump();

			break;
			
		case ActionState.WallJump:

			if (DistanceFromObject (raycast.raycastPoint) > 8) {
				state = ActionState.BackJump;
			}

			WallJump();

			break;
			
		case ActionState.BackJump:

			if (Input.GetButton("Horizontal")){
				moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
				moveDirection *= speed + 10;
				state = ActionState.Jump;
			}

			WallJump();

			break;
		}

		if (verticalPower >= minVerticalPower){

			verticalPower -= gravity * Time.deltaTime;
		}
		else {
			verticalPower = minVerticalPower;
		}

		moveDirection.y = verticalPower;

		controller.Move(moveDirection * Time.deltaTime);
	//	print (moveDirection.y);
	}

	void WallJump(){

		if (raycast.CanWallJump()){
//			print (raycast.CanWallJump());
			if (Input.GetButtonDown ("Jump") && Input.GetAxisRaw("Horizontal") != 0){
				if (state != ActionState.BackJump){
					state = ActionState.WallJump;
				}
				wjCounter += 1;
				print ("Vert Antes " + verticalPower);
				verticalPower = jumpForce; //+ 3 * wjCounter;

				print ("Vert Depois " + verticalPower);
				moveDirection = transform.TransformDirection(-Vector3.right.x + 0.2f, 0, 0);
				moveDirection *= speed;
			}
		}
	}
	
	void Orientation(){
		
		if (transform.position.x > tempPosition) {
			transform.eulerAngles = new Vector3(0,0,0);
		}
		else if (transform.position.x < tempPosition){
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
		tempPosition = transform.position.x;
	}
	
	float DistanceFromObject (Vector2 ObjectPosition){
		
		float distance = Vector2.Distance (ObjectPosition, transform.position);
		//print (distance);
		return distance;
	}
	
	void Attack(){
		
		if (Input.GetButtonDown("Fire1")){
			
			switch (state){
			case ActionState.Stand:
				AttackGround();
				break;
				
			case ActionState.AttackGround:
				AttackGround();
				break;
				
			case ActionState.Jump:
				AttackJump();
				break;
				
			case ActionState.AttackJump:
			//	AttackJump();
				break;
				
			case ActionState.WallJump:
				AttackJump();
				break;
				
			case ActionState.BackJump:
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
		state = ActionState.AttackGround;
		attackCombo += 1;
		_animator.SetInteger("combo", attackCombo);
	}
	
	void AttackJump(){
		
		state = ActionState.AttackJump;
		attackCombo = 1;
		_animator.SetInteger("combo", attackCombo);
	}
	
	void StopWalking(){
		
		moveDirection = Vector2.zero;
		_animator.SetFloat("walk", 0);
	}

	void OnControllerColliderHit(ControllerColliderHit hit){

		MovingPlatform (hit);
		Pickuploot (hit);
		SpikeDamage (hit);
	}

	void MovingPlatform(ControllerColliderHit hit){

		if (hit.transform.name == "Moving Platform" && Vector3.Angle (hit.normal, Vector3.up) < controller.slopeLimit){
			movingPlatform = hit.transform;
			onMovingPlatformCheckInterval = 0.2f;
			movingPlatform.GetComponent<downwardPlatform>().hasObjectOnTop = true;
		}
	}

	void StayOnMovingPlatform(){
		
		if (movingPlatform && onMovingPlatformCheckInterval > 0){
			transform.parent = movingPlatform;
		}
		onMovingPlatformCheckInterval -= Time.deltaTime;
		
		if (movingPlatform && onMovingPlatformCheckInterval <= 0){
			if (movingPlatform.parent.name == "PlatformWeightable"){
				movingPlatform.GetComponent<downwardPlatform>().hasObjectOnTop = false;
			}
			movingPlatform = null;
			transform.parent = null;
		}
	}
	
	void StepDownOneWayPlatform(){
		
		if (raycast.IsGrounded ()) {
			if (raycast.IsGrounded().collider.name == "Platform_OneWay"){
				GameObject oneWayPlatform = raycast.IsGrounded().collider.gameObject;
				
				if (Input.GetButtonDown ("Jump") && Input.GetKey(KeyCode.S)){
					
					oneWayPlatform.GetComponentInChildren<OneWayCollision>().BehaviorActive = false;
					Physics.IgnoreCollision(this.gameObject.collider, oneWayPlatform.collider);
				}
			}
		}
	}

	void Pickuploot(ControllerColliderHit hit){
		
		if (hit.collider.tag == "Loot") {
			print ("loot");
			AddLoot(hit.gameObject);
			Destroy(hit.collider.gameObject);
		}
	}
	
	void AddLoot(GameObject loot){
		
		switch (loot.name){
		case "GoldCoin":
			GameControl.AddGold(10);
			break;
		case "IronIngot":
			GameControl.AddIron(1);
			break;
		case "Stone":
			GameControl.AddStone(1);
			break;
		case "Grimoire":
			GameControl.AddGrimoire(1);
			break;
		}		
	}

	void SpikeDamage(ControllerColliderHit hit){
		
		if (hit.transform.name == "Spike"){
			this.gameObject.SendMessage("ReceiveDamage", hit.collider.GetComponent<BaseStats>().damage);
		}
	}
}
