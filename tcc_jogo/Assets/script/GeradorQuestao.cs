using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
public class GeradorQuestao {
	private Dictionary<string,bool> sentencas;
	public enum operador{
		OU, E
	}
	public enum modificador{
		NAO, ABREP,FECHAP
	}
	System.Random rnd;



	public GeradorQuestao(){
		sentencas = new Dictionary<string,bool>();
		string path = "./Assets/Resources/questions/sentencas1.csv";
		TextAsset textAsset= Resources.Load("questions/sentencas1") as TextAsset;

		string text = "";
		if( textAsset !=null  ){
			Debug.Log(" CARREGANDO: "+path);
			text = textAsset.text;// System.IO.File.ReadAllText(path, Encoding.UTF8);
		}else{
			Debug.Log(" CARREGANDO: not exist "+path);
			text = "todos o azul é vermelho;0\n" +
				"todos coloridos são pretos;0\no sol é quente;1\na pedra é mole;0";

		}

		string[] splitSeparador= new string[] {";","\n"};
		string[] split = text.Split(splitSeparador,StringSplitOptions.RemoveEmptyEntries);
		rnd = new System.Random();
		string sentencaAux;
		bool validadeAux;
		for(int i=0; i< split.Length;i++ ){
			sentencaAux = split[i];
			i++;
			validadeAux =  (int.Parse(split[i]) ==1 )?true:false  ;
			sentencas.Add(sentencaAux,validadeAux);
		//	Debug.Log(sentencaAux +" "+validadeAux);
		}
	}
	public List<string> GerarSentencas(int nSentencas){
		List<string> sentenca = new List<string>();
		int aux;
		string[] op;
		for(int i=0;i<nSentencas; i++){
			// pega uma sentenca aleatoria e guarda

			aux = rnd.Next(0, sentencas.Keys.Count -1); 
			sentenca.Add(sentencas.Keys.ElementAt(aux) );
			//escolhe um operador aleatorio
			op = Enum.GetNames( typeof(operador));
			aux = rnd.Next(0, op.Length);

			sentenca.Add(op[aux]);
		}
		sentenca.RemoveAt( sentenca.Count-1 );
	/*	foreach(string s in sentenca){
			Debug.Log(s);
		}
	*/	return sentenca;
	}
	public bool Validade(List<string> premissas){
	//	bool validad = true;
		bool arg1, arg2;
		int npremissa=0, aux,op=0;

		arg1 = sentencas[premissas.ElementAt(0)];
		premissas.RemoveAt(0);
			foreach(string s in premissas){
			aux = npremissa%2;

			switch(aux){
			case 0:
				op = (int)Enum.Parse(typeof(operador), s);
				break;
	
			case 1:
				arg2 = sentencas[s];
				int E=(int)Enum.Parse(typeof(operador), operador.E.ToString() );
				int OU= (int)Enum.Parse(typeof(operador), operador.OU.ToString() );

				if( E==op)
					arg1 = arg1 && arg2;
				else if(OU==op)
					arg1 = arg1 || arg2;
				Debug.Log( arg1 +" "+arg2);
				break;
			}
			npremissa++;
		}

		return arg1;
	}




}
