using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Persistencia : MonoBehaviour {

	public string proximacena = "Default";
	public int proximacenaIndex=0;

	public Jogador[] jogadores;
	public JogadorInfo[] jogadoresInfo;

	public int nJogadores=0;
	public int[] casasTabuleiro;
	public int nCasasTabuleiro;

	public bool isChromecast = false;
	public ArrayList mapas;
	public GeradorQuestao gQuestao;
	public int partidasJogadas;
	void Awake(){
		
		this.gameObject.name= "persistencia";

		if (FindObjectsOfType(GetType()).Length == 0)
		{
			DontDestroyOnLoad(this.gameObject);
			//Destroy(gameObject);
		}

	}
	// Use this for initialization
	void Start () {
	
	}

	public int NJogadores {
		get {
			return nJogadores;
		}
		set{
			nJogadores= value;
		}
	}
	
	// Update is called once per frame
	void Update () {
	/*	for(int i=0; i< jogadores.Length; i++ ){
			DontDestroyOnLoad(Jogadores[i]);
		}
*/
	
	}

	public int NCasasTabuleiro {
		get {
			return nCasasTabuleiro;
		}
		set{
			nCasasTabuleiro = value;
		}
	}

	public int[]CasasTabuleiro {
		get {
			return casasTabuleiro;
		}
		set{
			casasTabuleiro = value;
		}
	}


	public Jogador[] Jogadores {
		get {
			return jogadores;
		}
		set{
			jogadores= value;
	/*		for(int i=0; i<jogadores.Length;i++){
				jogadores[i].transform.parent =null;
				//jogadores[i].GetComponent<RectTransform>().parent = null;
				jogadores[i].GetComponent<RectTransform>().SetParent(this.transform);
				//	DontDestroyOnLoad(jogadores[i]);
			}
*/
		}
	}

	public string Proximacena {
		get {
			return proximacena;
		}
		set{
			proximacena= value;
		}
	}

	public int ProximacenaIndex {
		get {
			return proximacenaIndex;
		}
		set{
			proximacenaIndex=value;
		}
	}

	public void CarregarCena(int cena){
		
		proximacenaIndex = cena;
		SceneManager.LoadScene(TelaCarregamento.CARREGAMENTO);

	}

}
