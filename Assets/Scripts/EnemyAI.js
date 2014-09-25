#pragma strict
var hit : RaycastHit;
var target : GameObject;
var health : int;
var attackRange = 9.0;
var moveSpeed= 5.0;
var attackSpeed : float;
var damage : int;
var targetLastPosition : Vector3;
var alerted : boolean;
var lostrigger : LoSTrigger;
var targetonSight : boolean;
var isGrounded;
var verticalPower;
var jumpPower : float = 10;
var jumpObstacle : boolean;
var lastGroundedPosition : Vector3;
var moveDirection : Vector2 = Vector2.zero;
var jumpState : boolean;
var _animator : Animator;
var animationClip : AnimationClip;
var jumpEnd : boolean;
var gravity : float = 4.0;
var attacking = false;
private var nextAttack = 0;
private var layerMask : int = 1 << 8 | 1 << 12;

function Awake(){

	Physics.IgnoreLayerCollision(9, 9);
	Physics.IgnoreLayerCollision(8, 9);
	Physics.IgnoreLayerCollision(9, 10);
	
	health = 100;
	lostrigger = GetComponentInChildren(LoSTrigger);
	alerted = false;
	targetLastPosition = Vector3.zero;
	jumpObstacle = false;
	isGrounded = false;
	verticalPower = jumpPower;
	jumpState = false;
	_animator = GetComponent(Animator);
	jumpEnd = false;
}

function Update(){
	
	IsGrounded();
	PathClear();
	//Jump(jumpState);
//	ApplyGravity();

	
	if(lostrigger.triggered){
		RaycastTargetDetection();
	}

			//verticalPower -= gravity * Time.deltaTime; // <----------- TO DO
			//moveDirection.y = verticalPower;
}

function ApplyGravity(){
	
	if (!IsGrounded()){
		moveDirection.y -= gravity * Time.deltaTime;
		rigidbody.MovePosition(rigidbody.position + moveDirection * Time.deltaTime);
	}
}

function IsGrounded(){

	var raycastHit : RaycastHit;
	var down = -(transform.TransformDirection(Vector3.up));
	Debug.DrawRay(transform.position, down*2.3, Color.yellow);
	
	if (Physics.Raycast(transform.position, down, raycastHit, 2.3)){
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

function PathClear(){

	var raycastHit : RaycastHit;
	var right = transform.TransformDirection(Vector3.right);
	Debug.DrawRay(transform.position, right*2.5, Color.yellow);
	
	if (Physics.Raycast(transform.position, right, raycastHit, 2.5)){
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

function RaycastTargetDetection(){
	
	if (lostrigger.losTarget != null){
		if (Physics.Raycast(transform.position, (lostrigger.losTarget.transform.position - transform.position), hit, Mathf.Infinity, layerMask)) {

	    	target = hit.collider.gameObject;
	  	 	Debug.DrawRay(transform.position, (lostrigger.losTarget.transform.position - transform.position), Color.blue);
	    	
	    	if (hit.collider.tag == "Player"){
	    		alerted = true;
	    		targetonSight = true;
	    	}
	    	else{
	    		targetonSight = false;
	    	}
	    	
			CreateWaypoint();
		}
	}
}

function CreateWaypoint(){

	if (alerted){
		
		if (targetonSight){
	    	targetLastPosition = Vector3(hit.point.x, transform.position.y, transform.position.z);
		}
		else{
			transform.FindChild("EnemyWaypoint").transform.position = targetLastPosition;
			Debug.DrawRay(transform.position, (hit.point - transform.position), Color.red);
			Debug.DrawRay(transform.position, (transform.FindChild("EnemyWaypoint").transform.position - transform.position), Color.green);

		}
		LookAtIgnoreHeight(targetLastPosition);
		CheckDistanceToTarget(targetLastPosition);
	}
}

function LookAtIgnoreHeight(targetLastPosition : Vector3) {

	if(targetLastPosition.x < transform.position.x){
		transform.eulerAngles = Vector2(0, 180);
	}
	else if (targetLastPosition.x > transform.position.x){
		transform.eulerAngles = Vector2(0, 0);
	}
}

function CheckDistanceToTarget(targetLastPosition : Vector3){

	if(targetonSight){
		
		var targetDistance = Vector3.Distance(targetLastPosition, transform.position);
		
		if (targetDistance < attackRange)
		{
			Attacking();
		}
		else
		{
			EnemyMovement(targetLastPosition);
		}
	}
}

function Attacking(){

	if (Time.time > nextAttack) {
 			
 			SendDamage();
			nextAttack = Time.time + attackSpeed;
	}
}

function AttackAnimation(){

	if (transform.position.x > target.transform.position.x){
		animation.Play("Alien_Attack_180");
	}
	else{
		animation.Play("Alien_Attack");
	}
}

function SendDamage(){
	
	if (target.tag == "Player"){
		
		AttackAnimation();
		target.gameObject.SendMessage("ReceiveDamage", damage);
	}
	
}

function EnemyMovement(targetLastPosition : Vector3){

	if((IsGrounded()) && (PathClear())){
	
		lastGroundedPosition = transform.position;
		moveDirection = transform.right;
		moveDirection *= moveSpeed;
		rigidbody.MovePosition(rigidbody.position + moveDirection * Time.deltaTime);
	}
	
	else if ((IsGrounded()) && !(PathClear())) {
//		jumpObstacle = true;
//		jump();
		/*lastGroundedPosition = transform.position;
		moveDirection = transform.right;
		moveDirection *= moveSpeed;
		rigidbody.MovePosition(rigidbody.position + moveDirection * Time.deltaTime);*/

	}
	else{
	//	rigidbody.velocity = Vector2.zero;
		return;
	}
}
/*
function Jump(jumpState : boolean){
	 var jumpStart;
	if (jumpObstacle){
//		_animator.SetBool("jump", true);
 //  <----------- TO DO

   			rigidbody.AddForce(Vector3.up * 4, ForceMode.VelocityChange);
			//rigidbody.MovePosition(rigidbody.position + moveDirection * Time.deltaTime);
			//rigidbody.MovePosition(Vector2(GameObject.Find("jumpWaypoint").transform.position.x, -2));
			jumpObstacle = false;
   			Debug.Log("UP");

	}
	
	else {
		Debug.Log("DOWN");	
//		_animator.SetBool("jump", false);
		//rigidbody.AddForce(moveDirection * 40);
		//rigidbody.AddForce(Vector3.down * 150);
		//moveDirection.y = verticalPower;
	}
}

function JumpForward(){
	if ()
		rigidbody.AddForce(moveDirection * 4, ForceMode.VelocityChange);
	}
}
*/

function jump(){

//	lastPos = transform.position;
//	animation.Play("Alien2_Jump");
//	_animator.SetBool("jump", true);
//	_animator.enabled = true;
 /* if(_animator.playbackTime > _animator.length){ //<--------------- TO DO!
    _animator.SetBool("jump", false);
  }*/
}

function ChangeParentPosition(){
  jumpEnd = true;
}

/*function CreateJumpClip(){

  var curve = new AnimationCurve.Linear(5, lastGroundedPosition.x, lastGroundedPosition.y, lastGroundedPosition.z);
  // Create the clip with the curve
  Debug.Log("jump");
  animationClip.SetCurve("", Transform, "localPosition.x", curve);
//  animationClip.SetCurve("", Transform, "localPosition.y", curve);
 // animationClip.SetCurve("", Transform, "localPosition.z", curve);

  // Add and play the clip
  animation.AddClip(animationClip, "test");
  animation.Play("test");

}
*/
/*
function JumpFalse(){
  Debug.Log("JUMP");
  transform.position = lastPos;
  _animator.SetBool("jump", false);
  _animator.enabled = false;
}
*/