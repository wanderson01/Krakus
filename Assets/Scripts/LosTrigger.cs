using UnityEngine;
using System.Collections;

public class LosTrigger : MonoBehaviour {

	public GameObject losTarget;
	public bool triggered;
	public bool onSightArea;

	void OnTriggerStay (Collider col) {
		
		if(col.gameObject.tag == "Player"){
			losTarget = col.gameObject;
			onSightArea = true;
			triggered = true;
		}
	}

	void OnTriggerExit (Collider col){

		if (col.gameObject.tag == "Player") {
			onSightArea = false;
		}
	}
}
