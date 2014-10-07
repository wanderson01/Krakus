using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	public GameObject loot;

	void OnTriggerEnter(Collider col){
		DestroyBarrel (col);
	}

	void DestroyBarrel(Collider col){

		if (col.tag == "Weapon"){
			if (loot){
				Instantiate (loot, transform.position, transform.rotation);
			}
			Destroy (this.gameObject);
		}
	}
}
