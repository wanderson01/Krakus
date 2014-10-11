using UnityEngine;
using System.Collections;

public class MagicProjectile : MonoBehaviour {

	public float movementSpeed;
	public int damage;

	void Start(){
		Destroy (this.gameObject, 3);
	}

	void FixedUpdate(){
		rigidbody.AddForce (transform.right * movementSpeed, ForceMode.Impulse);
	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Map"){
			Destroy(this.gameObject);
		}
		else if (col.tag == "Enemy"){
			
			col.GetComponent<BaseStats>().ReceiveDamage(damage);
			Destroy(this.gameObject);
		}
	}
}
