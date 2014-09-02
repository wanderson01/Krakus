using UnityEngine;
using System.Collections;

public class UnitRaycast : MonoBehaviour {
	
	public bool nearLadder;
	public float rayLength;
	private int layerMask = 1 << 11;
	
	void Update () {
		
		DetectNearbyLadder();
	}
	
	void DetectNearbyLadder(){

		RaycastHit raycastHit;
		Vector3 right = transform.TransformDirection(Vector3.right);
		Debug.DrawRay(transform.position, right * rayLength, Color.blue);

		GameObject target = transform.parent.GetComponent<BaseUnit>().target;

		if (Physics.Raycast(transform.position, right, out raycastHit, rayLength, layerMask)){
			print ("Unit Raycast: " + raycastHit.collider.name);
			GameObject ladder = raycastHit.collider.transform.parent.gameObject;

			if(ladder == target && raycastHit.collider.name == "Center"){	
				nearLadder = true;
			}
			else{
				nearLadder = false;
			}
		}
		else {
			nearLadder = false;
		}
	}
}
