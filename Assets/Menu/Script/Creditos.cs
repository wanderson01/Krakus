using UnityEngine;
using System.Collections;

public class Creditos : MonoBehaviour {
	public string[] _Creditos;
	public GameObject _Parent;
	public GameObject Texto;
	void Start () {
		for(int i=0; i<_Creditos.Length; i++)
		{

			Texto.GetComponent<TextMesh>().text = _Creditos[i];
			Instantiate(Texto,new Vector2(this.transform.position.x,this.transform.position.y+(i*5)),Quaternion.identity);
		}
	}

	void Update () {

	}
}
