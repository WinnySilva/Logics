using UnityEngine.UI ;
using UnityEngine;
using System.Collections;
//using UnityEditor;
using UnityEngine.SceneManagement;

/**
 * Script para a cena de selecao de personagens
 * cria e edita o vetor de personagens para o resto do jogo
 * 
**/
public class EscolhaPersonagem : ManagerSceneTopLevel {
	private GameObject[] jogadores;
	private ArrayList jog;
	private int nJog;
	private int jogSelecionado=0;
	private bool sexo=true;
	private bool allSelected= false;
	public Text nick;
	public Text nick2;
	public Text pontuacao;
	public GameObject placeholderPersonagem;
	private GameObject infoReload;
	private ReloadInfo info;

	private class ReloadInfo : MonoBehaviour{

		public int sceneNumber;
		public int jogSelecionado=0;
		public bool first = true;
		public GameObject[] jogadores1;
		public JogadorInfo[] jogadores;
		public int[] personagensSet;
		public void personagensNumeros(GameObject[] jog){
			if(jog == null) return;
			personagensSet = new int[jog.Length];
			for (int i=0; i< jog.Length;i++){
				personagensSet[i]= jog[i].GetComponent<Jogador>().NumeroPersonagem;
			}
		}
	}

	// Use this for initialization
	void Awake () {
		base.setCommom();
		/*
	infoReload = GameObject.Find("infoReaload") ;

		if(infoReload == null){
			infoReload = new GameObject();
			infoReload.AddComponent<ReloadInfo>();
			info = infoReload.GetComponent<ReloadInfo>();
			info.jogSelecionado = jogSelecionado;
			info.jogadores = JogadorInfo.gerarInfo(jogadores) ;
			info.jogadores1 = this.jogadores;
			info.name = "infoReaload";
			info.first=true;
			DontDestroyOnLoad(infoReload);
		}else{
			info = infoReload.GetComponent<ReloadInfo>();
			info.first=false;
			jogSelecionado=info.jogSelecionado;
		}
	*/
		//	Debug.Log("info.first: "+info.first );
		iniciarJogadores(2);
		//info.personagensNumeros(jogadores);
		SelecionarJogador(jogSelecionado);
		//nick.gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3());
	}
	void Start () {
		InvokeRepeating("refreshScreen", 8.0f, 2f);
	}

	public void iniciarJogadores(int nJogadores){
	//	if(info.first)
		jogadores = JogadorInfo.gerarJogadores(base.persistencia.jogadoresInfo) ; // GameObject.FindGameObjectsWithTag("Player");
	//	else{
	//	jogadores =JogadorInfo.gerarJogadores(info.jogadores);
	//	}

		bool jogadoresAtivos = jogadores !=null; 

		if( jogadoresAtivos){
			nJog = jogadores.Length;

			Debug.Log(" ENCONTRADOS PLAYERS "+jogadores.Length+" "+nJogadores);
		}
		else{
			nJog = nJogadores;
			jogadores = new GameObject[nJog];

		//	Debug.Log(" NÃO ENCONTRADOS JOGADORES ");
		}
		

		Jogador jog =null;
		for(int i=0; i<nJog; i++ ){
	//		Debug.Log("iniciarJogadores && info.first: "+info.first+" ");
			jog = jogadores[i].GetComponent<Jogador>();
			if(  (!jogadoresAtivos || (jogadores[i] == null)) ){
				jogadores[i] = Instantiate(Resources.Load("prefabs/jogador") ) as GameObject;
			//	jogadores[i].AddComponent<RectTransform>();
			//	jogadores[i].AddComponent<Jogador>();
				jogadores[i].name="JOGADOR"+i;
				jog.setPersonagem(0);
				//	jogadores[i].GetComponent<RectTransform>().localPosition;
				jog.nick="JOGADOR "+i;
				Debug.Log("inside true iniciarJogadores");
				jog.SetScalePersonagem(new Vector3(0.007f,0.0047f,0));
			}
			else{
				jogadores[i].GetComponent<Jogador>().SetScalePersonagem(new Vector3(1f,1f,1f));
			}

			jogadores[i].GetComponent<RectTransform>().SetParent(placeholderPersonagem.transform); //(this.GetComponent<RectTransform>());
			jogadores[i].transform.localPosition=new Vector3(10,0,0);
			jogadores[i].transform.localScale=new Vector3(11,11,0);
			//jog.SetPositionPeao( new Vector3(4.11f,-0.42f,0) );
			//jog.SetScalePeao( new Vector3(0.01f,0.01f,0) );
			//jog.peao.transform.localPosition = new Vector3(4.11f,-0.42f,0);

			jog.personagem.transform.localPosition = new Vector3(0,0,0);
						//Debug.Log(jogadores[i].GetComponent.<RectTransform>().localPosition );
			//Debug.Log(jogadores[i].GetComponent<RectTransform>().position );
			jogadores[i].GetComponent<Jogador>().Visibilidade(false);
			//jogadores[i].GetComponent<RectTransform>().rect.width=1;
			//jogadores[i].GetComponent<RectTransform>().rect.height =1;

		}
	}

	public void confirmarJogador(){
		this.jogSelecionado++;
		if(this.jogSelecionado>=this.nJog){
			this.allSelected=true;

			persistencia.nJogadores = jogadores.Length;
			persistencia.jogadores = new Jogador[jogadores.Length];
			persistencia.jogadoresInfo = new JogadorInfo[jogadores.Length];

			for(int i=0; i<jogadores.Length;i++){
				persistencia.jogadoresInfo[i]= jogadores[i].GetComponent<Jogador>().GetInfo();
			}
			Destroy(infoReload);
			persistencia.CarregarCena(TelaCarregamento.EXPLICACAO);
			return;
		}
	//	info.jogSelecionado=jogSelecionado;
	//	info.jogadores = JogadorInfo.gerarInfo(jogadores) ;
/*		/info.personagensNumeros(jogadores);
*/		SelecionarJogador(jogSelecionado);
		return;
	}

	private GameObject SelecionarJogador(int selec){
		Debug.Log("SelecionarJogador: "+selec+" "+jogSelecionado+" "+jogadores[jogSelecionado].GetComponent<Jogador>().NumeroPersonagem+" "+jogadores.Length );
		if(selec>nJog){
			return null;
		}
		GameObject jog = null;

		for(int i=0; i<nJog; i++){
			if(i==selec){
				jog= jogadores[i];
				jogSelecionado=i;
				jogadores[i].GetComponent<Jogador>().Visibilidade(true);
			}
			else{
				jogadores[i].GetComponent<Jogador>().Visibilidade(false);
			}
		}

		nick.text =
			jogadores[jogSelecionado].GetComponent<Jogador>().Nick; 
		nick2.text =
			jogadores[jogSelecionado].GetComponent<Jogador>().Nick;
		pontuacao.text =
			""+jogadores[jogSelecionado].GetComponent<Jogador>().Pontuacao+" "+(jogadores[jogSelecionado].GetComponent<Jogador>().Pontuacao==1?"ponto":"pontos" ) ;

//		info.jogSelecionado=jogSelecionado;
	//	info.jogadores = JogadorInfo.gerarInfo(jogadores) ;
		return jog;
	}

	public GameObject GetJogador(int n){
		if(n>-1 && n<nJog){
			return jogadores[n];
		}
		return null;
	}

	public void SelecionarProximoPersonagem(){
		if(allSelected){
			return;
		}
		Jogador j = jogadores[jogSelecionado].GetComponent<Jogador>() ;
		int n= j.NumeroPersonagem +1;
		n = (n>8)?0:n;
		j.setPersonagem(n);

	//	info.jogSelecionado=jogSelecionado;
	//	info.jogadores = JogadorInfo.gerarInfo(jogadores) ;
	//	info.jogadores1 = this.jogadores;
	}
	public void SelecionarAnteriorPersonagem(){
		if(allSelected){
			return;
		}
		Jogador j = jogadores[jogSelecionado].GetComponent<Jogador>() ;

		int p = ((j.NumeroPersonagem-1%j.NPersonagens));
		p= p<0? 8:p;
	//	Debug.Log("personagem: "+p+"/"+j.NPersonagens);
		j.setPersonagem(p);

	//	info.jogSelecionado=jogSelecionado;
	//	info.jogadores = JogadorInfo.gerarInfo(jogadores) ;
	//	info.jogadores1 = this.jogadores;
	}

	public void ToggleSexo(){
		if(allSelected){
			return;
		}
		this.sexo = !sexo;
		Jogador j = jogadores[jogSelecionado].GetComponent<Jogador>() ;
		j.setPersonagem( j.NumeroPersonagem);
	}

}
