using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	private CharacterController controller;
	public GameObject mouseClickParticle;
	public Vector2 waypointPosition;
	public float movementSpeed = 10.0f;
	public float climbingSpeed = 50.0f;
	private Vector3 climbDirection = Vector3.zero;
	public float gravity = -18.0f;
	private Vector3 gravityPower = Vector3.zero;
	public double LadderHeight;
	public float unitHeight;
	public bool applyGravity = true;
	private BaseUnit _baseUnit;
	private Vector3 foregroundPosition = new Vector3(0, 0, 10);
	
	void Awake(){

	}

	void Start(){

		_baseUnit = GetComponent<BaseUnit>();
		waypointPosition = GetComponent<BaseUnit> ().Waypoint.transform.position;
		unitHeight = renderer.bounds.extents.y;
		gravityPower.y = gravity;
		controller = GetComponent<CharacterController>();
	}
	
	void Update () {

		PathClear();
		MoveOnMouseClick();
		PrepareToClimb ();
		ApplyGravity();
	}

	
	void MoveOnMouseClick(){ //TODO SEMPRE EXECUTANDO (EXECUTAR QUANDO A FILA NAO ESTIVER VAZIA).

		float distanceToWaypoint = Mathf.Abs(transform.position.x - waypointPosition.x);
		waypointPosition = GetComponent<BaseUnit>().Waypoint.transform.position;

		if(distanceToWaypoint > 5){
			LookAtDirection(waypointPosition);
			
			if(PathClear() && controller.isGrounded && !_baseUnit.climbing){
				Vector3 moveDirection = transform.right;
				moveDirection *= movementSpeed;
				controller.Move(moveDirection  *  Time.deltaTime);
			}
		}
		else{
			waypointPosition = _baseUnit.Waypoint.transform.position;
		}
	}
	
	void LookAtDirection(Vector2 waypoint){
		
		if(transform.position.x < waypoint.x){
			transform.eulerAngles = new Vector2(0, 0);
		}
		else if (transform.position.x > waypoint.x){
			transform.eulerAngles = new Vector2(0, 180);
		}
	}
	
	bool PathClear(){
		
		RaycastHit raycastHit;
		Vector3 right = transform.TransformDirection(Vector3.right);
		//Debug.DrawRay(transform.position, right*11, Color.yellow);
		
		if (Physics.Raycast(transform.position, right, out raycastHit, 11)){
			if(raycastHit.collider.tag == "Map"){
				print("Blocked");
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

	void PrepareToClimb (){

		bool nearLadder = GetComponentInChildren<UnitRaycast> ().nearLadder;

		if (_baseUnit.climbLadder && nearLadder){

			if (!_baseUnit.climbing){
				Vector3 asd = transform.position -= foregroundPosition;
				Vector3 centerOfStairs = new Vector3(_baseUnit.target.transform.position.x, transform.position.y, asd.z);
				transform.position = centerOfStairs;
				climbDirection = CheckClimbDirection();
			}

			_baseUnit.climbing = true;
			Climb(climbDirection);
		}
	}

	Vector3 CheckClimbDirection(){
		
		if (transform.position.y < _baseUnit.target.transform.position.y) {
			return Vector3.up;
		}
		else {
			return Vector3.down;
		}
	}

	void Climb(Vector3 climbDirection){

		if (_baseUnit.climbing){

			Debug.Log ("Climb");

			applyGravity = false;
			controller.Move(climbDirection  * climbingSpeed *  Time.deltaTime);
		}
	}

	void OnTriggerExit (Collider collider) {

		if (_baseUnit.climbing && climbDirection == Vector3.up && collider.name == "StepCeiling") {
			EndClimb();
		}
	}

	void OnTriggerEnter(Collider collider){

		if (_baseUnit.climbing && climbDirection == Vector3.down && collider.name == "StepFloor") {
			EndClimb();
		}
	}

	void EndClimb(){

		transform.position += foregroundPosition;
		_baseUnit.climbing = false;
		climbDirection = Vector3.zero;
		_baseUnit.climbLadder = false;
		_baseUnit.target = gameObject;
		applyGravity = true;

		return;
	}

	float ObjectDistanceToGround(){ // OBSOLETE
		
		RaycastHit raycastHit;
		Vector3 down = transform.TransformDirection(Vector3.down);
		Debug.DrawRay(transform.position, down*80, Color.yellow);
		float distance;
		
		if (Physics.Raycast(transform.position, down, out raycastHit)){  // <---------- TODO Apply LayerMask
		//	if(raycastHit.collider.tag == "Map"){
				distance = Vector2.Distance(transform.position, raycastHit.point);
				return distance;
			//}
		}
		else {
			return 0;  // <---------- TODO  Confirm
		}
	}

	Vector2 DirectionToClimb(){
		
		float LadderPosition = _baseUnit.target.transform.position.y;
		
		if (transform.position.y > LadderPosition) {
			return Vector2.up;
		}
		else{
			return -Vector2.up;
		}
	}

	void ApplyGravity(){
		
		if (applyGravity){
			controller.Move(gravityPower * Time.deltaTime);
		}
	}
}
