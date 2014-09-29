using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float OffsetY;
		
	void LateUpdate () {
		
		if 	(target != null){
			transform.position = new Vector3 (target.position.x, target.position.y + OffsetY);
		}
	}
}
