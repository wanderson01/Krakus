using UnityEngine;
using System.Collections;

public class DamageType {
	public int damage = 1;
	public BaseStats.DamageEffect effect = BaseStats.DamageEffect.Physical;

	public DamageType() {
	}

	public DamageType(int damage, BaseStats.DamageEffect effect) {
		this.damage = damage;
		this.effect = effect;
	}
}
