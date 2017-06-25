using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AguardandoJogadoresManager : ManagerSceneTopLevel {
	private Jogador jogadores;
	//private Persistencia persistencia;
	public int count_jogadores=1;
	public Text number_show;
	private GameObject infoReload;

	void Awake(){
		//jogadores = JogadorInfo.gerarJogadores(base.persistencia.jogadoresInfo) ; // GameObject.FindGameObjectsWithTag("Player");
		//bool jogadoresAtivos = jogadores !=null;

		base.setCommom();
	

	}
	// Use this for initialization
	void Start () {
		InvokeRepeating("refreshScreen", 2.0f, 2f);
		number_show.text= ""+count_jogadores;
//		persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
	}
		
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Add(){
		count_jogadores = (count_jogadores>=5)?5:count_jogadores+1;
		number_show.text= ""+count_jogadores;
	}
	public void Sub(){
		count_jogadores = (count_jogadores<=1)?1:count_jogadores-1;
		number_show.text= ""+count_jogadores;
	}
	public void Confirmar(){
		number_show.text= ""+count_jogadores;

		base.persistencia.jogadoresInfo = new JogadorInfo[count_jogadores];
		JogadorInfo info; 
		for (int i=0; i<count_jogadores;i++){
			info = new JogadorInfo();
			info.nick = "JOGADOR "+(i+1);
			persistencia.jogadoresInfo[i] = info;			
		}
		Destroy(infoReload);
		persistencia.CarregarCena(TelaCarregamento.ESCOLHAPERSONAGEM);
	}
}
