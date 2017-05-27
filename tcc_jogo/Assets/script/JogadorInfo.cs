using UnityEngine;
using System.Collections;

public class JogadorInfo{
	public int pontuacao=0;
	public int pos_personagem;
	public int numeroPersonagem;
	public bool sexo;
	public string nick;
	public int nPersonagens=6; // iniciando no numero 1
	public bool visivel = true;
	public bool enabled;
	public string name;
	// Use this for initialization

	public static JogadorInfo[] gerarInfo(GameObject[] jogadores){
		JogadorInfo[] ji = new JogadorInfo[jogadores.Length];
		for(int i=0; i<jogadores.Length;i++){
			ji[i] = jogadores[i].GetComponent<Jogador>().GetInfo();
		}
		return ji;
	}

	public static GameObject[] gerarJogadores(JogadorInfo[] jogadoresInfo){
		if(jogadoresInfo == null) return null;
		GameObject[] jogadores = new GameObject[jogadoresInfo.Length];

		for(int i=0; i<jogadoresInfo.Length;i++){
			jogadores[i] = new GameObject();
			jogadores[i].AddComponent<RectTransform>();
			jogadores[i].AddComponent<Jogador>();
			jogadores[i].GetComponent<Jogador>().SetInfo(jogadoresInfo[i] );
		//	jogadores[i].transform.SetParent(jogadoresPlaceHolder.transform);
			jogadores[i].GetComponent<Jogador>().Visibilidade(true);
		}
		return jogadores;
	}

}
