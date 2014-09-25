using UnityEngine;
using System.Collections;

public class LosTrigger : MonoBehaviour {

	public GameObject losTarget;
	public bool triggered;
	
	void Awake (){
		losTarget = GameObject.Find("EmptyTarget");
	}
		
	void OnTriggerEnter (Collider collision) {
		
		if (collision){
			if(collision.gameObject.tag == "Player"){
				
				losTarget = collision.gameObject;
				triggered = true;
			}
		}
	}
}
