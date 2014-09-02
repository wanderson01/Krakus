using UnityEngine;
using System.Collections;

public class GUI_Aim : MonoBehaviour {

	public GameObject gui_pointer;
	public float rotateSpeed = 200;

	private Vector3 mousePosition;
	private Vector3 direction;
	private float distanceFromObject;
	private bool fire;
	private float minZ = 240f;
	private float maxZ = 345f;
	private SpriteRenderer pointerSprite;
	private Vector3 lastMousePosition;
	private BaseUnit unit;
	
	void Start () {

		unit = transform.parent.GetComponent<BaseUnit>();
		pointerSprite  = gui_pointer.GetComponent<SpriteRenderer>();
		pointerSprite.enabled = false;
	}
	
	void Update () {

		if (Input.GetMouseButton(2)){

			pointerSprite.enabled = true;

			Vector3 mouseDirection = Input.mousePosition - lastMousePosition;

			if (mouseDirection.y > 0) {
				
				transform.Rotate (Vector3.forward * rotateSpeed * Time.deltaTime);
			}
			
			if(mouseDirection.y < 0) {
				
				transform.Rotate (Vector3.back * rotateSpeed * Time.deltaTime);
			}
		}
		else {
			pointerSprite.enabled = false;
		}
		
		lastMousePosition = Input.mousePosition;
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y,
		                                          Mathf.Clamp (transform.localEulerAngles.z, minZ, maxZ));
	}
}

	/*
	 * Rotacionar objeto pelo mouse.
	 *transform.eulerAngles = new Vector3(0,0,Mathf.Clamp(Mathf.Atan2((Input.mousePosition.y - transform.position.y), (Input.mousePosition.x - transform.position.x))*Mathf.Rad2Deg - 90, minZ, maxZ));
	*/

