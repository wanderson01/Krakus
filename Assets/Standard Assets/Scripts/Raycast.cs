using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {

	public float groundedRaycastLength = 2f;
	public float walljumpRaycastLength = 0.9f;
	private RaycastHit walljumpRaycastHit;
	public Vector3 raycastPoint{ get; private set;}
	public bool grounded;

	void Update () {
		IsGrounded ();
		CanWallJump ();
		grounded = IsGrounded();
	}

	public bool IsGrounded(){
		
		RaycastHit raycastHit;
		var down = -(transform.TransformDirection(Vector3.up));
		Debug.DrawRay(transform.position, down * groundedRaycastLength, Color.yellow);
		
		if (Physics.Raycast(transform.position, down, out raycastHit, groundedRaycastLength)){
			if(raycastHit.collider.tag == "Map"){
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
	
	public bool CanWallJump(){
		
		var right = transform.TransformDirection(Vector3.right);
		Debug.DrawRay(transform.position, right * walljumpRaycastLength, Color.yellow);
		
		if (Physics.Raycast(transform.position, right, out walljumpRaycastHit, walljumpRaycastLength)){
			//print (walljumpRaycastHit.collider.name);
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
}
