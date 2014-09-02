using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	Collider col1;
	Collision col2;

	void Start () {
	
	}
	
	void Update () {

	}

	void OnTriggerEnter(Collider col){

		col1 = col;
		print (col.collider.name);

		if (col.transform.parent.name == "Arthur"){
			Destroy (gameObject);
		}
	}

}
