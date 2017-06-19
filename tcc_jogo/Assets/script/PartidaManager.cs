using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.SceneManagement;

public class PartidaManager : ManagerSceneTopLevel {
	#region Public data
	public GameObject tabuleiro;
	//public Persistencia persistencia;
	public GameObject[] jogadores;
	public GameObject mensagemPopUpTV;
	public GameObject opcoes;
	public GameObject jogadoresPlaceHolder;
	public GameObject coroa;
	public GameObject dice;
	public GameObject backgroundTV;
	//bool showpopup = false;
	public int showpopupsecs=0;
	public Text nickJogador_tv;
	public Text nickJogador_cel;
	public Text pontosJogador;
	public Text msgPopUp;
	public AudioSource respostaCertaAudio;
	public AudioSource respostaErradaAudio;
	public AudioSource vitoriaAudio;
	public Image canvasCelularBackground;
	public Image jogadorAvatar;
	#endregion

	#region Private data
	private bool ganhador = false;
	private GeradorQuestao gQ;
	private bool respostaJogador = false;
	private bool respostaCerta ;
	private bool condRespondido = false;
	private bool anim = false;
//	private int fade =0;
	private Thread questionamentos;

	private const int	PERGUNTANDO=0, RESPONDENDO=1, MOVENDO=2, INICIO=3,TERMINO=4 ;

	int jogadorSelecionado=0;
	int nSen = 2;
	int estadoMaquina = 0;
	private GameObject infoReload;
	private ReloadInfo info;

	private class ReloadInfo : MonoBehaviour{

		public int jogSelecionado=0;
		public bool first = true;
		public JogadorInfo[] jogadores;
		public string pergunta;
		public bool resposta;
		public int[] posTab;
	}


	#endregion

	#region Awake
	// Use this for initialization
	void Awake(){
		base.setCommom();

		infoReload = GameObject.Find("infoReaload") ;

		if(infoReload == null){
			infoReload = new GameObject();
			infoReload.AddComponent<ReloadInfo>();
			info = infoReload.GetComponent<ReloadInfo>();
			info.jogSelecionado = jogadorSelecionado;
			info.jogadores = JogadorInfo.gerarInfo(jogadores) ;
			//info.jogadores1 = this.jogadores;
			info.name = "infoReaload";
			info.first=true;
			DontDestroyOnLoad(infoReload);
		}else{
			info = infoReload.GetComponent<ReloadInfo>();
			info.first=false;
			jogadorSelecionado = info.jogSelecionado;
		//	jogadores = JogadorInfo.gerarJogadores(info.jogadores) ;
		}

		Debug.Log("PARTIDA MANAGER");
		JogadorInfo[] jogadorInf ;
		if(info.first){
			jogadorInf = base.persistencia.jogadoresInfo;
		}
		else{
			jogadorInf = info.jogadores ;
		}
		jogadores = new GameObject[jogadorInf.Length];
	
		mensagemPopUpTV.SetActive(false);
		opcoes.SetActive(false);

		string msg ="Jogadores na partida:\n";
		for(int i=0; i<jogadorInf.Length;i++ ){
			Debug.Log(jogadorInf[i].name);
			jogadores[i] = new GameObject();
			jogadores[i].AddComponent<RectTransform>();
			jogadores[i].AddComponent<Jogador>();
			jogadores[i].GetComponent<Jogador>().SetInfo(jogadorInf[i] );
			jogadores[i].transform.SetParent(jogadoresPlaceHolder.transform);
			jogadores[i].GetComponent<Jogador>().Visibilidade(true);
			msg+=jogadores[i].name+"\n";

			if(info.first){
				tabuleiro.GetComponent<Tabuleiro>().SetarPeao(0,jogadores[i].GetComponent<Jogador>());
			}else{
				Debug.Log(i+" setar pos: "+jogadores[i].GetComponent<Jogador>().PosTabuleiro+" info: "+jogadorInf[i].posTabuleiro);
				tabuleiro.GetComponent<Tabuleiro>().SetarPeao(jogadores[i].GetComponent<Jogador>().PosTabuleiro,jogadores[i].GetComponent<Jogador>());

			}

		}
	//	msgPopUp = mensagemPopUpTV.GetComponentInChildren<Text>();
		ShowMessage(false,msg,false,false);
		gQ = persistencia.gQuestao;
		info.jogSelecionado = jogadorSelecionado;
		info.jogadores = JogadorInfo.gerarInfo(jogadores) ;

	}
	#endregion

	#region Start
	void Start () {
		StartCoroutine(Controle() );
		InvokeRepeating("refreshScreen", 10.0f, 5f);
	//	questionamentos = new Thread(_questionamento);
	//	questionamentos.Start();
	}
	#endregion

	#region Update
	// Update is called once per frame
	void Update () {
//		Debug.Log("Partida Manager UPDATE: "+base.debugInfo() );
		info.jogSelecionado = jogadorSelecionado;
	//	info.jogadores = JogadorInfo.gerarInfo(jogadores) ;
		info.pergunta = this.msgPopUp.text;
		info.resposta= this.respostaCerta;
		info.posTab = new int[this.jogadores.Length];
		string pos="pos ";
		for(int i=0; i<this.jogadores.Length;i++ ){
			pos+=" "+jogadores[i].GetComponent<Jogador>().PosTabuleiro;
		}
		Debug.Log(pos);
	}
	#endregion

	#region Threads
	public void _questionamento(){

	}

	#endregion

	private IEnumerator Controle(){
		int posAtual;
		int rnd=0;
		while(estadoMaquina > -1){
			Debug.Log("jogador: "+jogadorSelecionado);
		switch(estadoMaquina){
		case INICIO:					
			if(!anim){
				Debug.Log("INICIO");
				estadoMaquina = PERGUNTANDO;

			}
			break;

		case PERGUNTANDO:
			if(!anim){

				Debug.Log("PERGUNTANDO");
					canvasCelularBackground.color = new Color(1f,1f,1f);
					dice.GetComponent<Animator>().SetInteger("numberDice",0);
					dice.transform.localPosition = new Vector3(1,1,1);
				
				Pergunta(nSen);
					this.nickJogador_tv.text="JOGADOR "+(jogadorSelecionado+1);
					this.nickJogador_cel.text="JOGADOR "+(jogadorSelecionado+1);
					this.pontosJogador.text = ""+jogadores[jogadorSelecionado].GetComponent<Jogador>().Pontuacao;
					this.jogadorAvatar.sprite = jogadores[jogadorSelecionado].GetComponent<Jogador>().Personagem.GetComponent<Image>().sprite;
					this.EnfocarCameraTV(
						jogadores[jogadorSelecionado].GetComponent<Jogador>().Peao.transform.position  );
				estadoMaquina = RESPONDENDO;
			}
			break;

		case RESPONDENDO:
			if(!anim){				
				Debug.Log("RESPONDENDO");
				if(condRespondido){
					condRespondido = false;
					ShowMessage(false,"",false,false);
					estadoMaquina = MOVENDO;
						if(TesteResposta()){
							rnd = Random.Range(1,6);
							dice.transform.localPosition = new Vector3(626f,-300f,45f);
							dice.GetComponent<Animator>().SetInteger("numberDice",rnd);
							canvasCelularBackground.color = new Color(0,1f,0);
							this.respostaCertaAudio.Play();
						}else{
							canvasCelularBackground.color = new Color(1f,0,0);
							this.respostaErradaAudio.Play();
						}
				}
			}
			break;

		case MOVENDO:
			if(!anim){				
				Debug.Log("MOVENDO");
				if(TesteResposta()){
					posAtual = jogadores[jogadorSelecionado].GetComponent<Jogador>().PosTabuleiro;
					jogadores[jogadorSelecionado].GetComponent<Jogador>().Pontuacao+=nSen;
						MoverPeao(jogadorSelecionado,posAtual+rnd);// tabuleiro.GetComponent<Tabuleiro>().NCasas);//

						//refreshScreen();
					}//else{
						
					//}
				posAtual = jogadores[jogadorSelecionado].GetComponent<Jogador>().PosTabuleiro;

					//testa se chegou ao final
					if(posAtual>= tabuleiro.GetComponent<Tabuleiro>().NCasas-1){
						estadoMaquina = TERMINO;	
					}else{
						jogadorSelecionado = (jogadorSelecionado+ 1)%jogadores.Length ;
						info.jogSelecionado = jogadorSelecionado;
						estadoMaquina = INICIO;
					}


			}
			break;
			case TERMINO:
				if(!anim){
					Debug.Log("TERMINO GG");
					ShowMessage(true, " PARABÉNS\n"+jogadores[jogadorSelecionado].GetComponent<Jogador>().Nick,true,false);
					estadoMaquina = -1;
					vitoriaAudio.Play();
					yield return new WaitForSeconds(5);
					this.persistencia.jogadoresInfo = JogadorInfo.gerarInfo(this.jogadores);
					this.persistencia.CarregarCena(TelaCarregamento.CLASSIFICACAO);
				}
			break;
		default:

			break;
		}


		yield return new WaitForSeconds(1);

		}

	}
		

	private void refreshScreen(){
		backgroundTV.SetActive(false);
		backgroundTV.SetActive(true);
		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );

		Debug.Log("desativas e ativas background");
	}
	private void Pergunta(int nivel){
		List<string> sentencas = gQ.GerarSentencas(nivel);
		string pergunta ="";
		foreach(string s in sentencas){
			pergunta += s+"\n";
		}
		ShowMessage(true,pergunta,false,true);
		opcoes.SetActive(true);

		this.respostaCerta = gQ.Validade(sentencas);
	//	yield return new WaitUntil(() => condRespondido== true);
//		UnityEditor.SceneView.RepaintAll();
	}

	public void MoverPeao(int p, int pos){
		if(ganhador) return;

		Jogador j = jogadores[p].GetComponent<Jogador>();

		tabuleiro.GetComponent<Tabuleiro>().SetarPeao(pos,j);
		Debug.Log("MOVER PEAO - jogador:"+p+" para posicao: "+pos);
		//EnfocarCameraTV(tabuleiro.GetComponent<Tabuleiro>().GetCasaTransform(pos).position);
		if(pos>= tabuleiro.GetComponent<Tabuleiro>().NCasas){
			ganhador = true;
		}


	}

	public void EnfocarCameraTV(Vector3 position){
		float z = base.tv_camera.transform.position.z;
		base.tv_camera.transform.position = new Vector3(position.x,position.y, z);
		Debug.Log("EnfocarCameraTV posicao: "+position);
		Scene scene = SceneManager.GetActiveScene();

		Debug.Log("Repaint All ");
	}


	public IEnumerator ShowPopUp(int seconds, string msg, bool coroa){
		anim = true;
		// seta a coroa
		mensagemPopUpTV.transform.GetChild(2).gameObject.SetActive(false);

		//seta a mensagem
		mensagemPopUpTV.GetComponentInChildren<Text>().text = msg;
		mensagemPopUpTV.SetActive(true);
	

		yield return new WaitForSeconds(seconds);


		mensagemPopUpTV.SetActive(false);
		mensagemPopUpTV.transform.GetChild(1).gameObject.SetActive(false);
		anim = false;
	}

	private IEnumerator Movefade( int posfinal){
		anim = true;
		Jogador jogador = jogadores[jogadorSelecionado].GetComponent<Jogador>();

		int casasCount = Mathf.Abs(posfinal - jogador.PosTabuleiro ) ;
		int a = Mathf.Abs(posfinal - jogador.PosTabuleiro )/(posfinal - jogador.PosTabuleiro);

		for(int i=0; i< casasCount; i++){
			MoverPeao(jogadorSelecionado, jogador.PosTabuleiro +a);
			Debug.Log(jogador.PosTabuleiro +" pos " );
			yield return new WaitForSeconds(0.5f);
		}
		anim = false;
	}


	public void ShowMessage (bool showMSG, string msg, bool premiacao,bool showOPC){
		msgPopUp.text = msg;
		mensagemPopUpTV.SetActive(showMSG);
		opcoes.gameObject.SetActive(showOPC);
		int nchild= mensagemPopUpTV.transform.childCount-1;
		coroa.SetActive(premiacao);
	}

		
	public void OpVerdadeira(){
		this.respostaJogador=true;
		condRespondido = true;

	}
	public void OpFalsa(){
		this.respostaJogador = false;
		condRespondido = true;

	}
	public bool TesteResposta(){
		string msg;
		bool acerto = respostaJogador == respostaCerta; 
		if(acerto){
			msg = " RESPOSTA CERTA";
		}else{
			msg = " RESPOSTA ERRADA";
		}

		Debug.Log("jogador: "+jogadorSelecionado+ " resposta dada: "+ respostaJogador +" resposta da pergunta: "+respostaCerta );
		return acerto;
	}

}
