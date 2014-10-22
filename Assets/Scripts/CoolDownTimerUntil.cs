using UnityEngine;
using System.Collections;

public class CoolDownTimerUntil : CoolDownTimer {
	public float threshold = 0f;
	new void Start () {
		base.Start ();
	}
	
	new void Update () {
		if (timePass > threshold) {
			base.Update ();
		}
	}
}
