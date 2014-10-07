using UnityEngine;
using System.Collections;

public class Magic : MonoBehaviour {

	public GameObject fireObj;
	public GameObject iceObj;
	private PlayerController.ActionState playerState;
	private BaseStats baseStats;

	void Start () {

		playerState = transform.parent.parent.GetComponent<PlayerController> ().state;
		baseStats = transform.parent.parent.GetComponent<BaseStats> ();
	}

	public void CastMagic(){

		if (baseStats.damageEffect != BaseStats.DamageEffect.Physical){
			GameObject magicProjectile = Instantiate (MagicEffect(), transform.position, transform.rotation) as GameObject;
		}
	}

	GameObject MagicEffect(){
		
		if (baseStats.damageEffect == BaseStats.DamageEffect.Fire){
			return fireObj;
		}
		else if (baseStats.damageEffect == BaseStats.DamageEffect.Ice){
			return iceObj;
		}
		else {
			Debug.Log ("Error: No magic effect selected.");
			return null;
		}
	}
}
