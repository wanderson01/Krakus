using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	Collision col2;

	void Start () {
	
	}
	
	void Update () {

	}

	void OnTriggerEnter(Collider col){
		print (col.collider.name);

		if (col.transform.parent.name == "Arthur"){
			Destroy (gameObject);
		}
	}

}
