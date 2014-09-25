using UnityEngine;
using System.Collections;

public class OneWayCollision : MonoBehaviour {
	
	public bool BehaviorActive {get; set;}
	
	void Start () {
		BehaviorActive = true;
	}
	
	void Update () {
		
	}
	
	void OnTriggerStay(Collider col){
		
		OneWayPlatformBehavior (col);
	}
	
	void OneWayPlatformBehavior(Collider col){
		
		if (BehaviorActive){
			if (col.tag == "Player"){
				if (col.transform.position.y > this.transform.position.y){
					Physics.IgnoreCollision(col, transform.parent.collider, false);
				}
				else {
					Physics.IgnoreCollision(col, transform.parent.collider, true);
				}
			}
		}
		else {
			if (col.transform.position.y < 0){
				BehaviorActive = true;
			}
		}
	}
}
