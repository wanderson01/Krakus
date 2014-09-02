using UnityEngine;
using System.Collections;

public class ArrowPhysics : MonoBehaviour {

	private Vector3 mousePosition;
	private Vector3 direction;
	private float distanceFromObject;

	public float arrowForce;
	public Vector3 startPosition = Vector3.zero;
	public Vector3 endPosition = Vector3.zero;
	public bool fire = false;

	void Start () {

		//Physics.gravity = new Vector3 (0, -300, 0);
		rigidbody.useGravity = false;
		rigidbody.AddForce (transform.up * arrowForce, ForceMode.Impulse);
	}

	void FixedUpdate(){

		fire = true;
		rigidbody.useGravity = true;

		transform.up = Vector3.Slerp (transform.up, rigidbody.velocity.normalized, 10 * Time.deltaTime);
	}

	void OnCollisionEnter(Collision col){

		if (col.collider.tag == "Map" || col.collider.tag == "Target") {

			Destroy(gameObject);
		}
	}
}
	






/*
	void Update () {

		if (Input.GetMouseButtonDown(1)){
			startPosition = new Vector3(transform.position.x, transform.position.y, Input.mousePosition.z);
		}
		if (Input.GetMouseButtonUp(1)){
			endPosition = new Vector3(transform.position.x, transform.position.y, Input.mousePosition.z);


			//rigidbody.AddForce (transform.up * arrowForce, ForceMode.Impulse);
		}

		//transform.LookAt(startPosition);
	
		Debug.DrawLine(startPosition, endPosition);
		Vector3 planeRotation = new Vector3 (0, 270, 90);
		Plane rightClick_plane = new Plane(planeRotation, transform.position);
		Ray rightClick_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float rightClick_ray_HitPosition = 0.0f;
		rightClick_plane.Raycast (rightClick_ray, out rightClick_ray_HitPosition);

	
}*/
