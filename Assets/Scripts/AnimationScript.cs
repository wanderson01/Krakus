using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	private MovementController controller;
	private Animator _animator;

	// Use this for initialization
	void Start () {

		controller = transform.parent.GetComponent<MovementController> ();
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
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
