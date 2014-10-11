using UnityEngine;
using System.Collections;

public class FallingObjectTrigger : MonoBehaviour {

	public GameObject fallingObj;

	void Start(){

		fallingObj = transform.GetChild (0).gameObject;
	}

	void OnTriggerEnter(Collider collider){
		
		if (collider.gameObject.tag == "Player"){
			transform.GetComponentInChildren<Rigidbody>().useGravity = true;
		}
	}
}
