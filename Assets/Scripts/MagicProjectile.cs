using UnityEngine;
using System.Collections;

public class MagicProjectile : MonoBehaviour {

	public float movementSpeed;
	public int damage;
	public BaseStats.DamageEffect damageEffect;
	public bool canHitPlayer;

	void Start(){
		Destroy (this.gameObject, 3);
	}

	void FixedUpdate(){
		rigidbody.AddForce (transform.right * movementSpeed, ForceMode.Impulse);
	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Map") {
			Destroy(this.gameObject);
		} else if (col.tag == "Enemy") {
			col.GetComponent<BaseStats>().ReceiveDamage(new DamageType(damage, damageEffect));
			Destroy(this.gameObject);
		}
		else if (canHitPlayer && col.tag == "Player"){
			col.GetComponent<BaseStats>().ReceiveDamage(new DamageType(damage, damageEffect));
		}
	}
}
