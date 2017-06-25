using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class TelaCarregamento : MonoBehaviour {
	private int proximacena;
	private GameObject data;
	public   const  int MENU = 0;
	public   const  int CREDITOS=1, ESCOLHAPERSONAGEM=2, CARREGAMENTO=3, 
	CLASSIFICACAO=4,PARTIDA=5,MAPA=6,CONECTAR_CHROMECAST=7, AGUARDANDO_JOGADORES=8, EXPLICACAO=9;
	private Persistencia p;

	void Awake(){
		data = GameObject.Find("persistencia");
		//data = new GameObject(); 
		//data.AddComponent<Persistencia>();
		p = data.GetComponent<Persistencia>();

		proximacena = p.ProximacenaIndex;

		Debug.Log(" CARREGANDO: "+proximacena);

		switch(proximacena){
		case TelaCarregamento.MENU:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"Menu Principal";
			break;
		case TelaCarregamento.CREDITOS:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"Créditos";
			break;
		case TelaCarregamento.ESCOLHAPERSONAGEM:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"Escolha de Personagem";
			if(!p.isChromecast){
				proximacena = 10;
			}

			break;
		case CARREGAMENTO:
			Debug.Log(3);

			break;
		case TelaCarregamento.CLASSIFICACAO:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"Classifição Geral";
			if(!p.isChromecast){
				proximacena = 11;
			}
			break;
		case TelaCarregamento.MAPA:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"Mapa";
			break;
		case TelaCarregamento.PARTIDA:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"Partida";
			Debug.Log(" CARREGANDO: partida");
			loadPartida();
			Debug.Log(" CARREGANDO: partida carregada");
			if(!p.isChromecast){
				proximacena = 12;
			}

			break;
		case TelaCarregamento.CONECTAR_CHROMECAST:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"CONECTAR CHROMECAST";
			break;
		case TelaCarregamento.AGUARDANDO_JOGADORES :
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"NÚMERO DE JOGADORES";
			if(!p.isChromecast){
				proximacena = 13;
			}
			break;
		case TelaCarregamento.EXPLICACAO:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"INSTRUÇÕES";
			if(!p.isChromecast){
				proximacena = 14;
			}

			break;
		default:
			proximacena = 0;			

			break;

		}
//		Debug.Log(proximacena);
		Debug.Log(" CARREGAR CENA "+proximacena);

		SceneManager.LoadScene(proximacena);
	}


	private void loadPartida(){
		Debug.Log(" CARREGANDO: MAPA");

		// carrega gerador de questoes
		GeradorQuestao gquestao;
		gquestao = new GeradorQuestao();

		if(p.mapas == null){
			p.mapas = loadMaps();
			p.partidasJogadas=0;
		}else{
			p.partidasJogadas++;
		}

		int mapaNum= p.partidasJogadas % p.mapas.ToArray().Length ;
		Debug.Log(" HÁ "+p.mapas.ToArray().Length+" MAPA(S)" );
		/*
		persistencia.NCasasTabuleiro = casasConfig.Length;
		persistencia.CasasTabuleiro = casasConfig;
		persistencia.gQuestao = gquestao;
		*/
		int [] mapaAux =  ((List<int> )p.mapas.ToArray()[mapaNum]).ToArray(); 

		p.NCasasTabuleiro = mapaAux.Length ;
		p.CasasTabuleiro = mapaAux;
		p.gQuestao = gquestao;

	}
	private ArrayList loadMaps(){
		Debug.Log(" CARREGANDO: MAPA");
		//carrega mapa
		string path = "./Assets/Resources/maps/mapa_.csv";
		string text ="";
		TextAsset textAsset= Resources.Load("maps/mapa_") as TextAsset;

		if( textAsset != null ){
			Debug.Log(" CARREGANDO: exists");
			text = textAsset.text; //System.IO.File.ReadAllText(path);
		}else{
			Debug.Log(" CARREGANDO: not exists");
			text = "0;0\n2;1\n2;0\n2;0\n2;1\n2;0\n2;0\n2;1";
		}

		string[] splitSeparador= new string[] {";","\n"};
		string[] split = text.Split(splitSeparador,StringSplitOptions.RemoveEmptyEntries);
		ArrayList maps = new ArrayList();
		List<int> mapAux = new List<int>();
		Debug.Log(text);
		int aux=0;
		for(int i=0; i< split.Length; i++ ){
			//Debug.Log("CARREGADO DO MAPA: "+split[i]+" "+i+"/"+split.Length);
			if(split[i].Contains("*")  ){
				maps.Add(mapAux);
				mapAux = new List<int>();

			}else{
				aux = int.Parse(split[i]);
				mapAux.Add(aux);
			}
		}
		maps.Add(mapAux);
		//Persistencia pers = data.GetComponent<Persistencia>();//.CasasTabuleiro = new int[nCasas][];

		return maps;
	}
	
}
