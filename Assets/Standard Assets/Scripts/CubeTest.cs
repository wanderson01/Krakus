using UnityEngine;
using System.Collections;

public class CubeTest : MonoBehaviour {

	int cont = 0;
	Vector2 posx;
	Vector2 posy;

	void Start () {
	
		posx = new Vector2(transform.position.x+1, transform.position.y);
		posy = new Vector2(transform.position.x, transform.position.y+1);
	}
	
	void Update () {


		transform.position = posx;

	
	}
}
