using UnityEngine;
using System.Collections;

public class PatrolCheckpointBehavior : MonoBehaviour {

	public GameObject patrollingObj;
	public GameObject otherCheckpoint;
	private EnemyAI enemy;
	private GameObject checkpointLeft;
	private GameObject checkpointRight;
	private bool collisioned;

	void Start(){

		collisioned = false;
		enemy = patrollingObj.GetComponent<EnemyAI>();

		foreach (Transform child in transform.parent) {

			if (child.name == "CheckpointRight") {
				checkpointRight = child.gameObject;
			}
			else{
				checkpointLeft = child.gameObject;
			}
		}
	}

	void OnTriggerEnter(Collider col){
		
		if (col.gameObject == patrollingObj && enemy.state == EnemyAI.State.Patrol  && collisioned == false){
			print ("collider");
			if (enemy.target == this.gameObject){
				enemy.target = otherCheckpoint;
			}
			else {
				enemy.target = this.gameObject;
			}
			collisioned = true;
		//	LookAtIgnoreHeight(target.transform.position);
		}
	}

	void OnTriggerExit(Collider col){
		
		if (col.gameObject == patrollingObj && enemy.state == EnemyAI.State.Patrol && collisioned == true){

			collisioned = false;
		}
	}

	void OnTriggerStay(Collider col){

		if (col.gameObject == patrollingObj && enemy.state == EnemyAI.State.Patrol && collisioned == false){

			if (enemy.target == this.gameObject){
				enemy.target = otherCheckpoint;
			}
			else {
				enemy.target = this.gameObject;
			}
			collisioned = true;
		}
	}
}