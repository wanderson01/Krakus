using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {
	
	void OnTriggerEnter(Collider col){
		
		if (col.name == "Sword" && col.transform.parent.tag == "Player"){
			//Destroy (gameObject);
		}
	}
}
