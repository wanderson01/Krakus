using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public bool switchOn;
	public bool automatic = false;
	public float maxSpeed;
	public float speedDamp;
	public float distanceToDamp;
	public GameObject switchObject;
	public Transform waypointEnd;
	public Transform waypointStart;
	public Vector3 currentWaypoint;
	private float distanceTotal;
	private float distanceToWaypoint;
	private float speedModifier;
	public float movementSpeed;

	void Start () {

		currentWaypoint = waypointEnd.position;
		distanceTotal = Vector3.Distance(waypointStart.position, waypointEnd.position);
		movementSpeed = maxSpeed;
	}

	void Update () {

		switchOn = switchObject.GetComponent<SwitchBehavior> ().switchOn;

		if (automatic){
			if (switchOn) {
				AutomaticMovement();
			}
		}
		else {
			ManualMovement ();
		}

	}

	void ManualMovement(){

		distanceToWaypoint = Vector3.Distance (transform.position, currentWaypoint);
		
		DampMovement ();
					
		if (switchOn){
			if (currentWaypoint == waypointStart.position){
				movementSpeed = maxSpeed;
			}
			currentWaypoint = waypointEnd.position;	
		}
		else {
			if (currentWaypoint == waypointEnd.position){
				movementSpeed = maxSpeed;
			}
			currentWaypoint = waypointStart.position;
		}

		transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, movementSpeed * Time.deltaTime);
	}

	void AutomaticMovement(){

		distanceToWaypoint = Vector3.Distance (transform.position, currentWaypoint);

		DampMovement ();

		if (distanceToWaypoint <= 0){

			if (currentWaypoint == waypointEnd.position){
				currentWaypoint = waypointStart.position;
			}
			else {
				currentWaypoint = waypointEnd.position;
			}
			movementSpeed = maxSpeed;
		}
		transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, movementSpeed * Time.deltaTime);
	}
	
	void DampMovement(){
		
		if (distanceToWaypoint <= distanceToDamp){

			float distPercent = (distanceTotal - distanceToWaypoint) / distanceTotal;
			speedModifier = distPercent * speedDamp;
			movementSpeed -= speedModifier * Time.deltaTime;
			if (movementSpeed < 1) movementSpeed = 1;
		}
	}
}
