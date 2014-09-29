using UnityEngine;
using System.Collections;

public class MagicProjectile : MonoBehaviour {

	public float movementSpeed;
	public int damage;

	void Start(){
		Destroy (this.gameObject, 4);
	}

	void FixedUpdate(){
		rigidbody.AddForce (transform.right * movementSpeed, ForceMode.VelocityChange);
	}

	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Map"){
			Destroy(this.gameObject);
		}
		else if (col.gameObject.tag == "Enemy"){
			
			col.GetComponent<BaseCharacter>().ReceiveDamage(damage);
			Destroy(this.gameObject);
		}
	}
}
