using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class classificacaoManager : ManagerSceneTopLevel {
	public GameObject classPlaceHolder;
	//public Persistencia persistencia;
	// Use this for initialization
	void Start () {
		base.setCommom();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Awake(){
		base.persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
		if(base.persistencia==null){
			Debug.Log("classificacao: persistencia é null ");
		}
		JogadorInfo[] jogadorInf = persistencia.jogadoresInfo;
		GameObject[] classif = new GameObject[jogadorInf.Length];
		int deltaY=3;

		for(int i=0; i<jogadorInf.Length; i++){
			Debug.Log("classificação: "+jogadorInf[i].nick+" "+jogadorInf[i].pontuacao+" ");
			classif[i]= Instantiate(Resources.Load("prefabs/classificados") ) as GameObject;
			classif[i].transform.GetChild(0).GetComponentInChildren<Text>().text = jogadorInf[i].nick+" - "+jogadorInf[i].pontuacao+(jogadorInf[i].pontuacao>1?" pts":" pt" )  ;
			classif[i].transform.GetChild(1).GetComponentInChildren<Text>().text = (i+1)+"º" ;
			classif[i].transform.SetParent(this.classPlaceHolder.transform );
			classif[i].transform.localScale=  new Vector3(1,1,1) ;
		//	classif[i].transform.position =  new Vector3(0,0,0) ;
			classif[i].transform.localPosition =  new Vector3(0,deltaY,0) ;
			deltaY+=-90;
		}

	}
	public void BotaoContinuar(){
		persistencia.CarregarCena(TelaCarregamento.ESCOLHAPERSONAGEM);
	}
		
}
