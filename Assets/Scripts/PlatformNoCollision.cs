using UnityEngine;
using System.Collections;

public class PlatformNoCollision : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {

	}
	
	void OnTriggerStay(Collider col){

		if (col.collider.tag == "Player"){
			print ("OnTriggerStay");
			if (col.collider.transform.position.y > this.transform.position.y){
				Physics.IgnoreCollision(GameObject.Find("Arthur").collider, transform.parent.collider, false);
			}
			else {
				Physics.IgnoreCollision(GameObject.Find("Arthur").collider, transform.parent.collider, true);
			}
		}
	}
}
