using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public float movementSpeed;
	private Vector3 moveDirection = Vector3.up;
	public float maxHeight;
	public float minHeight;
	private Vector3 startingHeight;
	public bool elevatorUp = true;
	private float direction;

	void Start () {
		startingHeight = rigidbody.position;
		direction = maxHeight;
	}

	void FixedUpdate () {

		Movement ();
	}
	void Movement(){

		if (rigidbody.position.y >= maxHeight && elevatorUp == true){
			moveDirection = Vector3.down;
			direction = minHeight;
			elevatorUp = false;
		}
		if (rigidbody.position.y <= minHeight && elevatorUp == false){
			moveDirection = Vector3.up;
			direction = maxHeight;
			elevatorUp = true;
		}

		moveDirection *= movementSpeed;
		transform.position = Vector3.MoveTowards (transform.position, new Vector3(transform.position.x, direction, transform.position.z), movementSpeed * Time.deltaTime);
	//	transform.Translate (moveDirection * Time.deltaTime);
	//	rigidbody.MovePosition(rigidbody.position + moveDirection * Time.deltaTime);
	}
}
