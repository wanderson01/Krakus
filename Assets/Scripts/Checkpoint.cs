using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public int orderId;

	void OnTriggerEnter(Collider col){

		if (col.tag == "Player") {
			print (GameData.spawnId);
			if (GameData.spawnId < orderId){
				print ("ID");
				GameData.SpawnPoint = this.transform.position;
				GameData.spawnId = orderId;
			}
		}
	}
}
