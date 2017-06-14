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
	public Text pontuacao;
	public GameObject placeholderPersonagem;

	// Use this for initialization
	void Start () {
		iniciarJogadores(2);
		SelecionarJogador(0);
		//nick.gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3());
	}


	public void iniciarJogadores(int nJogadores){
			
		jogadores = JogadorInfo.gerarJogadores(base.persistencia.jogadoresInfo) ; // GameObject.FindGameObjectsWithTag("Player");
		bool jogadoresAtivos = jogadores !=null; 

		if( jogadoresAtivos){
			nJog = jogadores.Length;

			Debug.Log(" ENCONTRADOS PLAYERS "+jogadores.Length+" "+nJogadores);
		}
		else{
			nJog = nJogadores;
			jogadores = new GameObject[nJog];

			Debug.Log(" NÃO ENCONTRADOS JOGADORES ");
		}

		Jogador jog =null;
		for(int i=0; i<nJog; i++ ){
			Debug.Log(" jogadoresAtivos: "+jogadoresAtivos);
			jog = jogadores[i].GetComponent<Jogador>();
			if( !jogadoresAtivos || (jogadores[i] == null) ){
				jogadores[i] = Instantiate(Resources.Load("prefabs/jogador") ) as GameObject;
			//	jogadores[i].AddComponent<RectTransform>();
			//	jogadores[i].AddComponent<Jogador>();
				jogadores[i].name="JOGADOR"+i;
				jog.setPersonagem(0);
				//	jogadores[i].GetComponent<RectTransform>().localPosition;
				jog.Nick="JOGADOR "+i;
				jog.SetScalePersonagem(new Vector3(0.007f,0.0047f,0));
			}
			else{
				jogadores[i].GetComponent<Jogador>().SetScalePersonagem(new Vector3(1f,1f,1f));
			}

			jogadores[i].GetComponent<RectTransform>().SetParent(placeholderPersonagem.transform); //(this.GetComponent<RectTransform>());
			jogadores[i].transform.localPosition=new Vector3(10,0,0);
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
		/*
			GameObject persistencia = new GameObject();
			persistencia.AddComponent<Persistencia>();
			Persistencia p=persistencia.GetComponent<Persistencia>();
			persistencia.name = "persistencia";
*/
			//Persistencia persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();

			persistencia.nJogadores = jogadores.Length;
			persistencia.jogadores = new Jogador[jogadores.Length];
			persistencia.jogadoresInfo = new JogadorInfo[jogadores.Length];

			for(int i=0; i<jogadores.Length;i++){
				persistencia.jogadoresInfo[i]= jogadores[i].GetComponent<Jogador>().GetInfo();
			
			}
			/*
			DontDestroyOnLoad(persistencia);
*/

			persistencia.CarregarCena(TelaCarregamento.EXPLICACAO);
			return;
		}
		SelecionarJogador(jogSelecionado);

		return;
	}

	private GameObject SelecionarJogador(int selec){
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
		pontuacao.text =
			""+jogadores[jogSelecionado].GetComponent<Jogador>().Pontuacao+" "+(jogadores[jogSelecionado].GetComponent<Jogador>().Pontuacao==1?"ponto":"pontos" ) ;
		
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

		j.setPersonagem( ( (j.NumeroPersonagem)%j.NPersonagens+1 ));

	}
	public void SelecionarAnteriorPersonagem(){
		if(allSelected){
			return;
		}
		Jogador j = jogadores[jogSelecionado].GetComponent<Jogador>() ;

		int p = ((j.NumeroPersonagem-1%j.NPersonagens));
		p= p<0? 8:p;
		Debug.Log(((j.NumeroPersonagem%j.NPersonagens)-1)+" "+p);
		j.setPersonagem(p);


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
