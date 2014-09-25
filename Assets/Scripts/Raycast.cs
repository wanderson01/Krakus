using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {
	
	public float groundedRaycastLength = 2f;
	public float walljumpRaycastLength = 0.9f;
	private RaycastHit walljumpRaycastHit;
	public Vector3 raycastPoint{ get; private set;}
	public bool grounded;
	private int layerMask = 1 << 8;
	
	void Update () {
		IsGrounded ();
		CanWallJump ();
		grounded = IsGrounded();
	}
	
	public GameObject IsGrounded(){
		
		RaycastHit hit;
		var down = -(transform.TransformDirection(Vector3.up));
		Debug.DrawRay(transform.position, down * groundedRaycastLength, Color.yellow);
		
		if (Physics.Raycast(transform.position, down, out hit, groundedRaycastLength, layerMask)){
			if(hit.collider.tag == "Map"){
				return hit.collider.gameObject;
			}
			else {
				return null;
			}
		}
		else {
			return null;
		}
	}
	
	public bool CanWallJump(){
		
		var right = transform.TransformDirection(Vector3.right);
		Debug.DrawRay(transform.position, right * walljumpRaycastLength, Color.yellow);
		
		if (Physics.Raycast(transform.position, right, out walljumpRaycastHit, walljumpRaycastLength)){
	//		print (walljumpRaycastHit.collider.name);
			if (!IsGrounded()){
				if(walljumpRaycastHit.collider.tag == "Map"){ 
					raycastPoint = walljumpRaycastHit.point;
					return true;
				}
				else {
					return false;
				}
			}
			else {
				return false;
			}
		}
		else {
			return false;
		}
	}
}


