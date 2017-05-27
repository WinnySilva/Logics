using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading;

public class PartidaManager : ManagerSceneTopLevel {
	#region Public data
	public GameObject tabuleiro;
	//public Persistencia persistencia;
	public GameObject[] jogadores;
	public GameObject mensagemPopUpTV;
	public GameObject opcoes;
	public GameObject jogadoresPlaceHolder;
	//bool showpopup = false;
	public int showpopupsecs=0;
	#endregion

	#region Private data
	private bool ganhador = false;
	private GeradorQuestao gQ;
	private bool respostaJogador = false;
	private bool respostaCerta ;
	private bool condRespondido = false;
	private bool anim = false;
	private int fade =0;
	private Thread questionamentos;
	private Text msgPopUp;
	private const int	PERGUNTANDO=0, RESPONDENDO=1, MOVENDO=2, INICIO=3,TERMINO=4 ;

	int jogadorSelecionado=0;
	int nSen = 2;
	int estadoMaquina = 0;
	#endregion

	#region Awake
	// Use this for initialization
	void Awake(){
		base.setCommom();
		Debug.Log("PARTIDA MANAGER");
		//persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
		if(base.persistencia==null){
			Debug.Log("persistencia é null ");

		}
		JogadorInfo[] jogadorInf = base.persistencia.jogadoresInfo;
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
			tabuleiro.GetComponent<Tabuleiro>().SetarPeao(0,jogadores[i].GetComponent<Jogador>());
		}
		msgPopUp = mensagemPopUpTV.GetComponentInChildren<Text>();
		ShowMessage(false,msg,false,false);
		gQ = persistencia.gQuestao;

	}
	#endregion

	#region Start
	void Start () {
		StartCoroutine(Controle() );

	//	questionamentos = new Thread(_questionamento);
	//	questionamentos.Start();
	}
	#endregion

	#region Update
	// Update is called once per frame
	void Update () {
		

	}
	#endregion

	#region Threads
	public void _questionamento(){

	}

	#endregion

	private IEnumerator Controle(){
		int posAtual;
		while(estadoMaquina > -1){
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
				Pergunta(nSen);

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
				}
			}
			break;

		case MOVENDO:
			if(!anim){				
				Debug.Log("MOVENDO");
				if(TesteResposta()){
					posAtual = jogadores[jogadorSelecionado].GetComponent<Jogador>().PosTabuleiro;
					//int rnd = Random.Range(1, );
					jogadores[jogadorSelecionado].GetComponent<Jogador>().Pontuacao+=nSen;
						MoverPeao(jogadorSelecionado,nSen);// tabuleiro.GetComponent<Tabuleiro>().NCasas);//

				}
				posAtual = jogadores[jogadorSelecionado].GetComponent<Jogador>().PosTabuleiro;
				//testa se chegou ao final
					if(posAtual>= tabuleiro.GetComponent<Tabuleiro>().NCasas-1){
						estadoMaquina = TERMINO;	
					}else{
						jogadorSelecionado = (jogadorSelecionado+ 1)%jogadores.Length ;
						estadoMaquina = INICIO;
					}
				

			}
			break;
			case TERMINO:
				if(!anim){
					Debug.Log("TERMINO GG");
					ShowMessage(true, " PARABÉNS\n"+jogadores[jogadorSelecionado].GetComponent<Jogador>().Nick,true,false);
					estadoMaquina = -1;
					yield return new WaitForSeconds(3);
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
	}

	public void MoverPeao(int p, int pos){
		if(ganhador) return;

		Jogador j = jogadores[p].GetComponent<Jogador>();

		tabuleiro.GetComponent<Tabuleiro>().SetarPeao(pos,j);

		EnfocarCameraTV(tabuleiro.GetComponent<Tabuleiro>().GetCasaTransform(pos).position);
		if(pos>= tabuleiro.GetComponent<Tabuleiro>().NCasas){
			ganhador = true;
		}

	}
	public void EnfocarCameraTV(Vector3 position){
		base.tv_camera.transform.position = new Vector3(position.x,position.y,0);
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
		mensagemPopUpTV.transform.GetChild(1).gameObject.SetActive(premiacao);
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

		Debug.Log(msg + " "+ respostaJogador +" "+respostaCerta );
		return acerto;
	}

}
