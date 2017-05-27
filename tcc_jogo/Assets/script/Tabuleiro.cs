using UnityEngine;
using UnityEngine.UI;
//using System.Collections;
using System;
using System.IO;


public class Tabuleiro : MonoBehaviour {	
	private GameObject[] casas;
	private int nCasas; 
//	private Vector3 PosInicial= new Vector3(40,50,0);
	//int espacamento = 50;
	private int maps;
	private Persistencia persistencia;
	public GameObject tabuleiro;
	int[][] posicoes; //linha coluna nPeoes
	void Awake () {
		tabuleiro = this.gameObject;
		persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
		nCasas = persistencia.NCasasTabuleiro;
		this.gerarTabuleiro();
	}
		
	// Update is called once per frame
	void Update () {

	}

	public int NCasas {
		get {
			return nCasas;
		}
	}

	public void SetarPeao(int destinoPos, Jogador jogador){
		int linha,coluna, posInicial = jogador.PosTabuleiro;
		if(destinoPos>= nCasas){
			destinoPos = nCasas-1;
		}

		linha = posicoes[destinoPos][0];
		coluna = posicoes[destinoPos][1];

		GameObject peao =  jogador.Peao;
		GameObject auxLinha = tabuleiro.transform.GetChild(linha).gameObject;
		GameObject casa = auxLinha.transform.GetChild(coluna).gameObject;

		peao.transform.SetParent(casa.transform);
		peao.transform.position= casa.transform.position;

		//casa destino
		if(casa.transform.childCount !=0){
			
			Rect rc = casa.GetComponent<RectTransform>().rect;
			float offset = (float)(rc.width/casa.transform.childCount+1);
	//		Debug.Log("offset"+offset+" min: "+rc.xMin+" max: "+rc.xMax);
			float x = - offset * (float)(casa.transform.childCount/2);//+casa.transform.position.x
			for(int i=0; i<casa.transform.childCount;i++ ){
				casa.transform.GetChild(i).localPosition = new Vector3(x,
					0,
					0);
//				Debug.Log(x+" -- ");
				x+=offset;
			}
		}

		//casa inicial
		linha = posicoes[posInicial][0];
		coluna = posicoes[posInicial][1];
		auxLinha = tabuleiro.transform.GetChild(linha).gameObject;
		casa = auxLinha.transform.GetChild(coluna).gameObject;
		if(casa.transform.childCount !=0){
			Rect rc = casa.GetComponent<RectTransform>().rect;
			float offset = (float)(rc.width/casa.transform.childCount+1);
			//		Debug.Log("offset"+offset+" min: "+rc.xMin+" max: "+rc.xMax);
			float x = - offset * (float)(casa.transform.childCount/2);//+casa.transform.position.x
			for(int i=0; i<casa.transform.childCount;i++ ){
				casa.transform.GetChild(i).localPosition = new Vector3(x,
					0,
					0);
				x+=offset;
			}
		}



		jogador.GetComponent<Jogador>().PosTabuleiro = destinoPos;
		peao.transform.localScale = new Vector3(0.05f,0.05f,1);

	}
	public Transform GetCasaTransform(int pos){
		int linha,coluna;
		if(pos>=nCasas){
			pos=nCasas-1;
		}
		linha = posicoes[pos][0];
		coluna = posicoes[pos][1];
		GameObject auxLinha = tabuleiro.transform.GetChild(linha).gameObject;
		GameObject casa = auxLinha.transform.GetChild(coluna).gameObject;		
	//	Debug.Log("get cood "+linha +" "+coluna+" : "+casa.GetComponent<RectTransform>().position);

		return (casa.GetComponent<RectTransform>().transform) ;


	}

	//posicoes
	//   1
	// 4 x 2
	//   3
	private void gerarTabuleiro(){
		int[][] dirCasas = persistencia.CasasTabuleiro;

		int dirCount=0;
		int linha = tabuleiro.transform.childCount-1 ;
		int coluna = 0;
		GameObject auxLinha;
		Image casa;
		posicoes = new int[nCasas][];
		for(int i=0;i<nCasas; i++){
			//posicoes
			//   1
			// 4 0 2
			//   3
			switch(dirCasas[i][0]){
			case 0:
				break;
			case 1:
				linha--;
				break;

			case 2:
				coluna++;
				break;

			case 3:
				linha++;
				break;

			case 4:
				coluna--;
				break;

			}
//			Debug.Log(linha +" "+coluna);
			posicoes[i]=new int[2]; // linha coluna nPeoes
			posicoes[i][0]= linha;
			posicoes[i][1]=coluna;

			auxLinha = tabuleiro.transform.GetChild(linha).gameObject;
			auxLinha.SetActive(true);
//			auxLinha.transform.GetChild(coluna).gameObject.AddComponent<Image>();
			casa = auxLinha.transform.GetChild(coluna).gameObject.GetComponent<Image>();
			auxLinha.transform.GetChild(coluna).gameObject.SetActive(true);
			casa.color = new Color(1,1,0);
		}
	}

}