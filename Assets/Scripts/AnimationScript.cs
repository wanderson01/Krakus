using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	private PlayerController controller;

	void Start () {

		controller = transform.parent.GetComponent<PlayerController> ();
	}

	void CastMagic(){
		GetComponentInChildren<Magic>().CastMagic();
	}

	void AttackCombo1(){

		if (controller.attackCombo <= 1){
			controller.attacking = false;
			controller.attackCombo = 0;
		}
	}

	void AttackCombo2(){

		if (controller.attackCombo == 2){
			controller.attacking = false;
			controller.attackCombo = 0;
		}
	}

	void AttackCombo3(){
		
		if (controller.attackCombo == 3){
			controller.attacking = false;
			controller.attackCombo = 0;
		}
	}

	void AttackCombo4(){
		
		if (controller.attackCombo >= 4){
			controller.attacking = false;
			controller.attackCombo = 0;
		}
	}
}
