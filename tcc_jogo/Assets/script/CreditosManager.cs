using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditosManager : MonoBehaviour {
	private Persistencia persistencia;
	// Use this for initialization
	void Start () {
		persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
		persistencia = persistencia==null? new Persistencia(): persistencia;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void voltarMenu(){
		persistencia.CarregarCena(TelaCarregamento.MENU);
	}
}
