#pragma strict
var losTarget : GameObject;
public var triggered : boolean;

function Awake (){
	losTarget = GameObject.Find("EmptyTarget");
}


function OnTriggerEnter (collision : Collider) {

	if (collision){
		if(collision.gameObject.tag == "Player"){

			losTarget = collision.gameObject;
			triggered = true;
		}
	}
}

