using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject[] Cena = new GameObject[4];
	public GameObject Sele_CenaHolder;
	int Num_Cena;
	void Start(){
		Num_Cena = 1;
		Cena[0].SetActive(true);
		Cena[1].SetActive(true);
		Cena[2].SetActive(true);
		Cena[3].SetActive(true);
		Sele_CenaHolder.SetActive(false);
	}
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast (ray,out hit))
		{
			if(hit.collider.tag == "Play" && Input.GetMouseButtonDown(0))
			{
				Sele_CenaHolder.SetActive(true);
			}
			if(hit.collider.tag == "Comecar" && Input.GetMouseButtonDown(0))
			{
				Application.LoadLevel(Num_Cena);
			}
			if(hit.collider.tag == "Extras" && Input.GetMouseButtonDown(0))
			{
				Debug.Log("Extras");
			}
			if(hit.collider.tag == "Creditos" && Input.GetMouseButtonDown(0))
			{
				Application.LoadLevel("Creditos");
				Debug.Log("Creditos");
			}
			if(hit.collider.tag == "Direita" && Input.GetMouseButtonDown(0))
			{
				print("Direita");
				Cena[0].SetActive(false);
				Cena[1].SetActive(true);
				Cena[2].SetActive(false);
				Cena[3].SetActive(true);
				Num_Cena = 2;
		
			}
			if(hit.collider.tag == "Esquerda" && Input.GetMouseButtonDown(0))
			{
				print("Esquerda");
				Cena[0].SetActive(true);
				Cena[1].SetActive(false);
				Cena[2].SetActive(true);
				Cena[3].SetActive(false);
				Num_Cena = 1;
			}


		}
	
	}
}
