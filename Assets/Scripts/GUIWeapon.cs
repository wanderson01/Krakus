using UnityEngine;
using System.Collections;

public class GUIWeapon : MonoBehaviour {

	public GameObject player;
	public Sprite[] weaponSprite;
	private BaseStats playerStats;
	private SpriteRenderer spriteRenderer;

	void Start () {

		playerStats = player.GetComponent<BaseStats> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		ShowWeaponType ();
	}

	void ShowWeaponType(){

		switch(playerStats.damageEffect){

		case BaseStats.DamageEffect.Physical:
			print ("teste");
			spriteRenderer.sprite = weaponSprite[0];
			break;

		case BaseStats.DamageEffect.Fire:
			spriteRenderer.sprite = weaponSprite[1];
			break;

		case BaseStats.DamageEffect.Ice:
			spriteRenderer.sprite = weaponSprite[2];
			break;
		}
	}
}
