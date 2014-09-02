using UnityEngine;
using System.Collections;

public class JumpWall : MonoBehaviour {	
		
	public bool _canWallJump; 
	
	public bool CanWallJump { 

		get{
			return _canWallJump;
		}
	
	}
	
	void Start () {
		
		_canWallJump = false;
	}
	
	void OnTriggerStay (Collider col){

		if (col.name == "MapObject") {
			_canWallJump = true;
		}
	}
	
	void OnTriggerExit(Collider col){

		if (col.name == "MapObject") {
			_canWallJump = false;
		}
	}
}
