using UnityEngine;
using System.Collections;

public class Opcoes : MonoBehaviour {
	bool MostrarOp = false;
	public GameObject _opcoes;
	void Start()
	{

	}
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			MostrarOp =! MostrarOp;
		}
		if(MostrarOp == true)
		{
			_opcoes.SetActive(true);
		}
		else {
			_opcoes.SetActive(false);
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast (ray,out hit))
		{
			if(hit.collider.tag == "Voltar" && Input.GetMouseButtonDown(0))
			{
				Application.LoadLevel(0);
			}
			if(hit.collider.tag == "Sair" && Input.GetMouseButtonDown(0))
			{
				Application.Quit();
			}
		}
	}
}
