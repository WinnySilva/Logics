using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;

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
			break;
		case CARREGAMENTO:
			Debug.Log(3);

			break;
		case TelaCarregamento.CLASSIFICACAO:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"Classifição Geral";
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
			break;
		case TelaCarregamento.CONECTAR_CHROMECAST:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"CONECTAR CHROMECAST";
			break;
		case TelaCarregamento.AGUARDANDO_JOGADORES :
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"NÚMERO DE JOGADORES";
			break;
		case TelaCarregamento.EXPLICACAO:
			this.transform.Find("carregando_identificacao").GetComponent<Text>().text=
				"INSTRUÇÕES";
			break;
		default:
			proximacena = 0;			

			break;

		}
//		Debug.Log(proximacena);
		Debug.Log(" CARREGAR CENA ");
		SceneManager.LoadScene(proximacena);
	}

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
	
	}
	private void loadPartida(){
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

		int nCasas = split.Length/2;
		int[][] casasConfig = new int[nCasas][];
		data.GetComponent<Persistencia>().CasasTabuleiro = new int[nCasas][];

		int aux=0;

		for(int i=0; i< split.Length;i++ ){

			casasConfig[aux] = new int[2];

			casasConfig[aux][0] = int.Parse(split[i]) ;
			i++;
			casasConfig[aux][1] = int.Parse(split[i]) ;
			aux++;
		}

		// carrega gerador de questoes
		GeradorQuestao gquestao;
		gquestao = new GeradorQuestao();


		data.GetComponent<Persistencia>().NCasasTabuleiro = casasConfig.Length;
		data.GetComponent<Persistencia>().CasasTabuleiro = casasConfig;
		data.GetComponent<Persistencia>().gQuestao = gquestao;

	}


	
}
