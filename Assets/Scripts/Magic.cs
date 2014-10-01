using UnityEngine;
using System.Collections;

public class Magic : MonoBehaviour {

	public GameObject fireObj;
	public GameObject iceObj;
	private PlayerController.ActionState playerState;
	private BaseCharacter baseChar;

	void Start () {

		playerState = transform.parent.parent.GetComponent<PlayerController> ().state;
		baseChar = transform.parent.parent.GetComponent<BaseCharacter> ();
	}

	public void CastMagic(){

		if (baseChar.damageEffect != BaseCharacter.DamageEffect.Physical){
			GameObject magicProjectile = Instantiate (MagicEffect(), transform.position, transform.rotation) as GameObject;
		}
	}

	GameObject MagicEffect(){
		
		if (baseChar.damageEffect == BaseCharacter.DamageEffect.Fire){
			return fireObj;
		}
		else if (baseChar.damageEffect == BaseCharacter.DamageEffect.Ice){
			return iceObj;
		}
		else {
			Debug.Log ("Error: No magic effect selected.");
			return null;
		}
	}
}
