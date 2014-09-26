using UnityEngine;
using System.Collections;

public class LosTrigger : MonoBehaviour {

	public GameObject losTarget;
	public bool triggered;
	public bool onSightArea;

	void OnTriggerStay (Collider collision) {
		
		if(collision.gameObject.tag == "Player"){
			losTarget = collision.gameObject;
			onSightArea = true;
			triggered = true;
		}
	}

	void OnTriggerExit (Collider collision){

		if (collision.gameObject.tag == "Player") {
			onSightArea = false;	
		}
	}
}
