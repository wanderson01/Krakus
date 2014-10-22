using UnityEngine;
using System.Collections;

public class FallingObject : MonoBehaviour {

	public int damage;
	public BaseStats.DamageEffect effect = BaseStats.DamageEffect.Physical;
	public int fallingSpeed = 1;

	void FixedUpdate(){

		if (rigidbody.useGravity){
			rigidbody.AddForce (Physics.gravity * fallingSpeed);
		}
	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.tag == "Map"){
			Destroy(transform.parent.gameObject);
		}
		else if (col.gameObject.tag == "Player"){

						col.gameObject.SendMessage("ReceiveDamage", new DamageType(damage, effect));
			Destroy(transform.parent.gameObject);
		}
	}
}