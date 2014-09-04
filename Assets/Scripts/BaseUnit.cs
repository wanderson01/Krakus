using UnityEngine;
using System.Collections;

public class BaseUnit : MonoBehaviour {

	public float health = 100;
	public float attackRange = 1;
	public float attackSpeed = 10;
	public float damage;
	public Projector projector;
	protected GameObject _waypoint;
	protected bool _isSelected;
	protected bool isMoving;
	protected float nextAttack = 0;
	public bool climbLadder;
	public bool climbing = false;
	public GameObject target;
	public GameObject targetTemp;
	public string opponentTag;
	
	void Awake(){
		
		_waypoint = gameObject;
	}

	public bool isSelected{
		
		get {
			return _isSelected;
		}
		
		set{
			if(value){

				projector.enabled = true;
			}
			else {
				projector.enabled = false;
			}
			
			_isSelected = value;
		}
		
	}

	public GameObject Waypoint{
		
		get {
			return _waypoint;
		}
		set{
			_waypoint = value;
		}
	}


	void CheckOpponent(){
		
		if (gameObject.tag == "Player"){
			opponentTag =  "Enemy";
		}
		else {
			opponentTag = "Player";
		}
	}
	
	void Start(){

		CheckOpponent ();
//		MouseSelection.AddUnitToList(this);
	}
	
	void Update(){
		DetectOpponent ();
		CheckHealth ();
	}

	/*void Ladder(){

		if (climbLadder) {

			climbLadder = true;
		}

	}*/


	
	void DetectOpponent(){
	
		RaycastHit hit;
		Vector3 right = transform.TransformDirection(Vector3.right);
		Debug.DrawRay(transform.position, right*attackRange, Color.yellow);
		if (Physics.Raycast(transform.position, right, out hit, attackRange)){

			if (hit.collider.tag == opponentTag){

				Attacking(hit.collider.gameObject);
			}
		}
	}

	void Attacking(GameObject attackTarget){

		targetTemp = target;
		target = attackTarget;

		if (Time.time > nextAttack) {
			
			SendDamage(attackTarget);
			nextAttack = Time.time + attackSpeed;
		}
	}
	
	void SendDamage(GameObject attackTarget){
		print ("SendDamage()");
		if (attackTarget.tag == opponentTag) {

			//AttackAnimation();
			target.gameObject.SendMessage ("ReceiveDamage", damage);
		}
	}

	void ReceiveDamage(float damage){

		print ("ReceiveDamage()");
		print (health);
		health -= damage;
	}

	void CheckHealth(){

		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
