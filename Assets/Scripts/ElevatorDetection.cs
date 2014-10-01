using UnityEngine;
using System.Collections;

public class ElevatorDetection : MonoBehaviour {
	
	public GameObject passenger;
	public float onPlatformInterval = 0.2f;

	void Start () {
	
	}

	void Update () {
	//	TriggerExitTimer ();
	}
	/*
	void TriggerExitTimer(){

		if (passenger && onPlatformInterval > 0){
			onPlatformInterval -= Time.deltaTime;
		}

		if (onPlatformInterval <= 0){
			passenger.gameObject.transform.parent = null;
			passenger.GetComponent<PlayerController>().applyGravity = true;
		}

	}*/
	
	void OnTriggerStay (Collider col){
		
		print (col.name);
		if (col.tag == "Player"){

			print ("Player");
			passenger = col.gameObject;
	//		GameObject groundedObject = col.gameObject.GetComponentInChildren<Raycast>().IsGrounded();
	//		print ("Grounded: " + groundedObject);

	//		if (groundedObject == this.gameObject){
				onPlatformInterval = 0.2f;
				col.transform.parent = this.transform.parent;
				col.GetComponent<PlayerController>().applyGravity = false;
		//	}
		}
	}
	/*
	void OnTriggerExit (Collider col){
		
		if (col.tag == "Player"){
			if (onPlatformInterval <= 0){
				GameObject groundedObject = col.gameObject.GetComponentInChildren<Raycast>().IsGrounded();
				
				if (groundedObject != this.gameObject){

					col.gameObject.transform.parent = null;
					col.GetComponent<PlayerController>().applyGravity = true;
				}
			}
		}
	}*/
}
