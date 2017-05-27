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

			Debug.Log(" NÃO ENCONTRADOS");
		}


		for(int i=0; i<nJog; i++ ){

			if( !jogadoresAtivos ){
				jogadores[i] = Instantiate(Resources.Load("prefabs/jogador") ) as GameObject;
			//	jogadores[i].AddComponent<RectTransform>();
			//	jogadores[i].AddComponent<Jogador>();
				jogadores[i].name="JOGADOR"+i;
				jogadores[i].GetComponent<Jogador>().setPersonagem(1 ,true);
				//	jogadores[i].GetComponent<RectTransform>().localPosition;
				jogadores[i].GetComponent<Jogador>().Nick="JOGADOR "+i;
				jogadores[i].GetComponent<Jogador>().SetScalePersonagem(new Vector3(0.007f,0.0047f,0));
			}
			else{
				jogadores[i].GetComponent<Jogador>().SetScalePersonagem(new Vector3(0.4f,0.37f,0));
			}

			jogadores[i].GetComponent<RectTransform>().SetParent(placeholderPersonagem.transform); //(this.GetComponent<RectTransform>());
			jogadores[i].transform.localPosition=new Vector3(0,-296,0);
			jogadores[i].GetComponent<Jogador>().SetPositionPeao( new Vector3(4.11f,-0.42f,0) );
			jogadores[i].GetComponent<Jogador>().SetScalePeao( new Vector3(0.001f,0.001f,0) );

						//Debug.Log(jogadores[i].GetComponent.<RectTransform>().localPosition );
			//Debug.Log(jogadores[i].GetComponent<RectTransform>().position );
			jogadores[i].GetComponent<Jogador>().Visibilidade(false);

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

			persistencia.CarregarCena(TelaCarregamento.PARTIDA);
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

		j.setPersonagem( ( (j.NumeroPersonagem)%j.NPersonagens+1 ) ,this.sexo);

	}
	public void SelecionarAnteriorPersonagem(){
		if(allSelected){
			return;
		}
		Jogador j = jogadores[jogSelecionado].GetComponent<Jogador>() ;

		int p = ((j.NumeroPersonagem-1%j.NPersonagens));
		p= p<1? 6:p;
		Debug.Log(((j.NumeroPersonagem%j.NPersonagens)-1)+" "+p);
		j.setPersonagem(p,this.sexo);


	}
	public void ToggleSexo(){
		if(allSelected){
			return;
		}
		this.sexo = !sexo;
		Jogador j = jogadores[jogSelecionado].GetComponent<Jogador>() ;
		j.setPersonagem( j.NumeroPersonagem,this.sexo);
	}
}
