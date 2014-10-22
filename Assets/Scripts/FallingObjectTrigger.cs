using UnityEngine;
using System.Collections;

public class FallingObjectTrigger : MonoBehaviour {

	public GameObject fallingObj;
	private Rigidbody rigidObj;

	void Start() {
		fallingObj = transform.GetChild (0).gameObject;
		rigidObj = transform.GetComponentInChildren<Rigidbody>();
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player" && rigidObj != null) {
			rigidObj.useGravity = true;
		}
	}
}
