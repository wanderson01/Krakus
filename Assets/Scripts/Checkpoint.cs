using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public int orderId;
	public bool activated = false;
	private Animator animator;

	void Start(){
		animator = GetComponent<Animator> ();
	}

	void Update(){

		if (activated){
			animator.SetBool("Enabled", true);
		}
	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Player") {
			activated = true;
			if (GameData.currentGame.SpawnId < orderId){
				GameData.currentGame.SpawnPoint = this.transform.position;
				GameData.currentGame.SpawnId = orderId;
			}
		}
	}
}
