using UnityEngine;
using System.Collections;

public class Target_Behavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		
		if (col.collider.tag == "Arrow") {
			
			Destroy(gameObject);
		}
	}
}
