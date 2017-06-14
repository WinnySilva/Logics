package geradorcartaslogics;


import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileReader;
import java.io.InputStreamReader;
import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.Dictionary;
import java.util.Random;


public class GeradorQuestao {
	private ArrayList<String> sentencas;
        
	public enum Operador{
		OU("OU",1), E("E",2);
                private final String valor;
                private final int intValue;
                Operador(String valorOpcao, int n){
                    valor = valorOpcao;
                    intValue =n;
                }
                public String getValor(){
                    return valor;
                }
                public int getCod(){
                    return intValue;
                }
	}
	public enum modificador{
		NAO, ABREP,FECHAP
	}
	private Random rnd;
        
        

	public GeradorQuestao(String path){
		sentencas = new ArrayList<String>();
		
                try {
                File f = new File(path);
                
                if(!f.canRead()){
                    System.out.println("Impossivel Ler") ;
                }
                
                BufferedReader br = new BufferedReader( 
                        new InputStreamReader(new FileInputStream(f), "CP1252")  );
                String line;
                //ArrayList<String> sentencas = new ArrayList<>();
                String[] splitted;
                String auxUpper;
                rnd = new Random();
		
                while ( ( line = br.readLine() ) != null )  {
                    
                    System.out.println(line );
                    splitted = line.split(";");
                    auxUpper = splitted[0];
                    //if(auxUpper.length()>2){
                    sentencas.add(auxUpper.substring(0, 1).toUpperCase()+auxUpper.substring(1) );
                    sentencas.add(splitted[1]);
                    //System.out.println(line+" "+splitted[0]+" : " + splitted[1] );
                   // }
                    
                }
                
                 } catch (Exception e) {
                e.printStackTrace();
                }	
	}
        
        public int listSize(){
            return (this.sentencas.size()/2);
        }
        
	public String[] GerarSentencas(int nSentencas){
		String sentenca[] = new String[2];
		int aux;
                sentenca[0]="";
		Operador[] op;
                Boolean guardado=null,novo=true;
                Integer opAux=null;
                op = Operador.values();
		for(int i=0;i<nSentencas; i++){
			// pega uma sentenca aleatoria e guarda
			aux = rnd.nextInt(sentencas.size()-2);
                        aux = ((aux%2)==0 )?aux:aux+1;
			sentenca[0] +=(sentencas.get(aux))+" ";
			//escolhe um operador aleatorio
                        novo = (sentencas.get(aux+1)).equals("1")  ;
                        if(( guardado != null) && (opAux!= null) ) {
                 //           System.out.println(novo+" : "+guardado+" "+opAux);
                            switch(opAux){
                                case 1:
                                    guardado = novo || guardado ;
                                    break;
                                case 2:
                                    guardado = novo && guardado;
                                break;    
                            }
                        }else{
                            guardado = novo;
                        }
                        
                        if(i<nSentencas-1 ){
                            aux = rnd.nextInt(op.length );
                            sentenca[0]+=(op[aux].getValor()+" ");                            
                            opAux = op[aux].getCod();
                           

                        }
               //         System.out.println(" <<< "+sentenca[0] );
                        
		}
                sentenca[1]= (guardado)?"VERDADEIRO":"FALSO" ;
		return sentenca;
	}
/*	public bool Validade(List<string> premissas){
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

*/


}
