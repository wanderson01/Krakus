using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {
	
	public float groundedRaycastLength = 2f;
	public float groundedRaycastOffsetY;
	public float walljumpRaycastLength = 0.9f;
	private RaycastHit walljumpRaycastHit;
	public Vector3 raycastPoint{ get; private set;}
	public bool grounded;
	public GameObject groundedObject;
	private int layerMask = 1 << 8;
	
	void Update () {

		IsGrounded ();
		CanWallJump ();
	}

	public GameObject IsGrounded(){
		
		RaycastHit hit;
		Vector3 down = -(transform.TransformDirection(Vector3.up));

		Vector3 posLeft = new Vector3 (transform.position.x - groundedRaycastOffsetY, transform.position.y);
		Vector3 posRight = new Vector3(transform.position.x + groundedRaycastOffsetY, transform.position.y);

		Debug.DrawRay(transform.position, down * groundedRaycastLength, Color.yellow);
		Debug.DrawRay(posLeft, down * groundedRaycastLength, Color.yellow);
		Debug.DrawRay(posRight, down * groundedRaycastLength, Color.yellow);


		if (Physics.Raycast(transform.position, down, out hit, groundedRaycastLength, layerMask)){
			if(hit.collider.tag == "Map"){
				return hit.collider.gameObject;
			}
			else {
				return null;
			}
		}

		if (Physics.Raycast(posLeft, down, out hit, groundedRaycastLength, layerMask)){
			if(hit.collider.tag == "Map"){
				return hit.collider.gameObject;
			}
			else {
				return null;
			}
		}

		if (Physics.Raycast(posRight, down, out hit, groundedRaycastLength, layerMask)){
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


