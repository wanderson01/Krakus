using UnityEngine;
using System.Collections;

public class Magic : MonoBehaviour {

	public GameObject magicObj;
	private PlayerController.ActionState playerState;

	void Start () {
		playerState = transform.parent.parent.GetComponent<PlayerController> ().state;
	}
	
	void Update () {
		CastMagic ();
	}

	void CastMagic(){

		if (Input.GetButtonDown("Fire1")){

			GameObject magicBall = Instantiate (magicObj, transform.position, transform.rotation) as GameObject;
		}
	}
}
