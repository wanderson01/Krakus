using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public RaycastHit hit;
	public GameObject target;
	public GameObject initialCheckpoint;
	public State state;
	public int health;
	public float attackRange = 9.0f;
	public float alertedSpeed;
	public float patrolSpeed;
	public float attackSpeed;
	public int damage;
	public Vector3 targetLastPosition;
	public float alertMaxTime;
	public float alertTimer;
	public LosTrigger lostrigger;
	public bool targetonSight;
	public bool isGrounded;
	public float verticalPower;
	public float jumpPower = 10f;
	public bool jumpObstacle;
	public Vector3 lastGroundedPosition;
	public Vector3 moveDirection = Vector2.zero;
	public bool jumpState;
	public Animator _animator;
	public AnimationClip animationClip;
	public bool jumpEnd;
	public float gravity = 4.0f;
	public bool attacking = false;
	private float nextAttack = 0;
	private int layerMask = 1 << 10 | 1 << 8;
	private Transform waypoint;
	
	void Awake(){
		
	//	Physics.IgnoreLayerCollision(9, 9);
	//	Physics.IgnoreLayerCollision(8, 9);
	//	Physics.IgnoreLayerCollision(9, 10);

		state = State.Patrol;
		target = initialCheckpoint;
		health = 100;
		lostrigger = GetComponentInChildren<LosTrigger>();
		waypoint = transform.FindChild("EnemyWaypoint");
		targetLastPosition = Vector3.zero;
		jumpObstacle = false;
		isGrounded = false;
		verticalPower = jumpPower;
		jumpState = false;
		_animator = GetComponent<Animator>();
		jumpEnd = false;
	}

	void FixedUpdate () {
	//	print ("Grounded: " + IsGrounded());
	//	print ("PathClear: " + PathClear ());
		IsGrounded();
		PathClear();

		if(lostrigger.triggered){
			RaycastTargetDetection();
		}
		StateMachine ();
	}

	public enum State{

		Patrol,
		Alerted
	}

	void StateMachine(){

		switch (state) {
		
		case State.Patrol:
			Patrol();
			break;

		case State.Alerted:
			SetTargetWaypoint();
			AlertTimer();
			break;	
		}
	}

	void Patrol(){

		EnemyMovement ();
		LookAtIgnoreHeight(target.transform.position);
	}
	/*
	void OnTriggerEnter(Collider col){

		if (state == State.Patrol && col.tag == "Patrol Checkpoint"){
			print ("collide");
			if (target == patrolCheckpoint[0]){
				target = patrolCheckpoint[1];
			}
			else {
				target = patrolCheckpoint[0];
			}
			LookAtIgnoreHeight(target.transform.position);
		}
	}
*/
	void ApplyGravity(){
		
		if (!IsGrounded()){
			moveDirection.y -= gravity * Time.deltaTime;
			rigidbody.MovePosition(rigidbody.position + moveDirection * Time.deltaTime);
		}
	}
	
	bool IsGrounded(){
		
		RaycastHit raycastHit;
		var down = -(transform.TransformDirection(Vector3.up));
		Debug.DrawRay(transform.position, down * 2.3f, Color.yellow);
		
		if (Physics.Raycast(transform.position, down, out raycastHit, 2.3f)){
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

	bool PathClear(){
		
		RaycastHit raycastHit;
		var right = transform.TransformDirection(Vector3.right);
		Debug.DrawRay(transform.position, right * 2.5f, Color.yellow);
		
		if (Physics.Raycast(transform.position, right, out raycastHit, 2.5f)){
			if(raycastHit.collider.tag == "Map"){
				return false;
			}
			else{
				return true;
			}
		}
		else{
			return true;
		}
	}
	
	void RaycastTargetDetection(){
		
		if (lostrigger.losTarget != null){
			if (Physics.Raycast(transform.position, (lostrigger.losTarget.transform.position - transform.position), out hit, Mathf.Infinity, layerMask)) {
								
				if (hit.collider.tag == "Player"){
					target = hit.collider.gameObject;
					Debug.DrawRay(transform.position, (lostrigger.losTarget.transform.position - transform.position), Color.blue);
					state = State.Alerted;
					targetonSight = true;
				}
				else{
					targetonSight = false;
				}
			}
		}
	}
	
	void SetTargetWaypoint(){
			
		if (state == State.Alerted){
			
			if (targetonSight){
				targetLastPosition = new Vector3(hit.point.x, transform.position.y, transform.position.z);
				waypoint.position = Vector3.zero;
			}
			else{
				if (waypoint.position == Vector3.zero){
					targetLastPosition = hit.point;
					transform.FindChild("EnemyWaypoint").transform.position = targetLastPosition;
					Debug.DrawRay(transform.position, (hit.point - transform.position), Color.red);
					Debug.DrawRay(transform.position, (transform.FindChild("EnemyWaypoint").transform.position - transform.position), Color.green);
				}
			}
			LookAtIgnoreHeight(targetLastPosition);
			CheckDistanceToTarget(targetLastPosition);
		}
	}
	
	void LookAtIgnoreHeight(Vector3 targetLastPosition) {
		
		if(targetLastPosition.x < transform.position.x){
			transform.eulerAngles = new Vector2(0, 180);
		}
		else if (targetLastPosition.x > transform.position.x){
			transform.eulerAngles = new Vector2(0, 0);
		}
	}
	
	void CheckDistanceToTarget(Vector3 targetLastPosition){
		
		if(targetonSight){
			
			var targetDistance = Vector3.Distance(targetLastPosition, transform.position);
			
			if (targetDistance < attackRange)
			{
				Attacking();
			}
			else
			{
				EnemyMovement();
			}
		}

		else if (state == State.Alerted){
			EnemyMovement();
		}
	}

	
	void Attacking(){
		
		if (Time.time > nextAttack) {
			
			SendDamage();
			nextAttack = Time.time + attackSpeed;
		}
	}
	
	void AttackAnimation(){
		
		if (transform.position.x > target.transform.position.x){
			//animation.Play("Enemy_Attack_180");
		}
		else{
			//animation.Play("Enemy_Attack");
		}
	}
	
	void SendDamage(){
		
		if (target.tag == "Player"){
			
			AttackAnimation();
	//		target.gameObject.SendMessage("ReceiveDamage", damage);
		}
		
	}
	
	void EnemyMovement(){

		float moveSpeed;

		if (state == State.Alerted){
			moveSpeed = alertedSpeed;
		}
		else {
			moveSpeed = patrolSpeed;
		}
		
		if((IsGrounded()) && (PathClear())){

			moveDirection = transform.right;
			moveDirection *= moveSpeed;
			rigidbody.MovePosition(rigidbody.position + moveDirection * Time.deltaTime);
		}
		
		else if ((IsGrounded()) && !(PathClear())) {

			moveDirection = transform.right;
			moveDirection *= moveSpeed;
			rigidbody.MovePosition(rigidbody.position + moveDirection * Time.deltaTime);
		}
	}

	void AlertTimer(){

		if (targetonSight){
			alertTimer = alertMaxTime;
		}
		else {
			if (alertTimer > 0){
				alertTimer -= Time.deltaTime;
			}
			else {
				AlertReset();
			}
		}
	}

	void AlertReset(){

		target = initialCheckpoint;
		lostrigger.losTarget = null;
		lostrigger.triggered = false;
		state = State.Patrol;
	}
}
