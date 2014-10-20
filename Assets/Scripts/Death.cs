using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	
	void OnTriggerEnter(Collider col){

		if (col.transform.parent.name == "Arthur"){
			Destroy (gameObject);
		}
	}

}
