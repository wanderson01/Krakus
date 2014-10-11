using UnityEngine;
using System.Collections;

public class PatrolCheckpointBehavior : MonoBehaviour {

	public GameObject patrollingObj;
	public GameObject otherCheckpoint;
	private EnemyAI enemy;
	private GameObject checkpointLeft;
	private GameObject checkpointRight;
	private bool triggered;
	private int stayCounter;

	void Start(){

		triggered = false;
		enemy = patrollingObj.GetComponent<EnemyAI>();
	}

	void Update(){
		//CheckifContains ();
	}

	void CheckifContains (){

		if (enemy.state == EnemyAI.State.Patrol){
			if (collider.bounds.Contains (patrollingObj.transform.position)) {
				enemy.target = otherCheckpoint;
			}
		}
	}

	void OnTriggerEnter(Collider col){
		
		if (col.gameObject == patrollingObj && enemy.state == EnemyAI.State.Patrol  && triggered == false){

			if (enemy.target == this.gameObject){
				enemy.target = otherCheckpoint;
			}
			else {
				enemy.target = this.gameObject;
			}
			triggered = true;
		//	LookAtIgnoreHeight(target.transform.position);
		}
	}

	void OnTriggerExit(Collider col){
		
		if (col.gameObject == patrollingObj && enemy.state == EnemyAI.State.Patrol && triggered == true){

			triggered = false;
		}
	}

	void OnTriggerStay(Collider col){

		if (col.gameObject == patrollingObj && enemy.state == EnemyAI.State.Patrol){

			stayCounter += 1;
			if (stayCounter > 1){
				enemy.target = otherCheckpoint;

				stayCounter = 0;
			}
		}
	}
}