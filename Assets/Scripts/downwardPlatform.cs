using UnityEngine;
using System.Collections;

public class downwardPlatform : MonoBehaviour {

	public bool switchOn;
	public float maxSpeed;
	public bool hasObjectOnTop = false;
	public Transform waypointTop;
	public Transform waypointBottom;
	public Vector3 currentWaypoint;
	private float distanceTotal;
	private float distanceToWaypoint;
	public float movementSpeed;
	private Vector3 moveDirection;

	void Start () {
		
		currentWaypoint = waypointTop.position;
		distanceTotal = Vector3.Distance(waypointTop.position, waypointBottom.position);
		movementSpeed = maxSpeed;
	}
	
	void Update () {
		ChangeWaypointWhenPlayerOnTop ();
	}
	
	void ChangeWaypointWhenPlayerOnTop(){
		
		distanceToWaypoint = Vector3.Distance (transform.position, currentWaypoint);
		
		if (hasObjectOnTop){
			currentWaypoint = waypointBottom.position;
		}
		else {
			currentWaypoint = waypointTop.position;
		}
		transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, movementSpeed * Time.deltaTime);
	//	moveDirection = transform.up;// Teste com MovePosition.
	//	moveDirection *= movementSpeed;
	//	rigidbody.MovePosition (rigidbody.position + (currentWaypoint - rigidbody.position).normalized * movementSpeed * Time.deltaTime);
	}
}
