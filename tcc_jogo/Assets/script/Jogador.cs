using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Jogador : MonoBehaviour {
	private int pontuacao=0;
	private int pos_personagem;
	private int numeroPersonagem;
	private bool sexo;
	private string nick;
	public GameObject peao;
	public GameObject personagem;
	private int nPersonagens=6; // iniciando no numero 1
	private bool visivel = true;
	private int posTabuleiro=0;
	// Use this for initialization

	void Awake(){
		iniciarPrefabs();
		//DontDestroyOnLoad(this);

	}
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {

	}

	public int PosTabuleiro {
		get {
			return posTabuleiro;
		}
		set{
			posTabuleiro= value;
		}
	}

	public GameObject Peao {
		get {
			return peao;
		}
		set{
			peao = value;
		}
	}

	public GameObject Personagem {
		get {
			return personagem;
		}
		set{
			personagem = value;
		}
	}

	private void iniciarPrefabs(){

		if(peao == null){
			peao = Instantiate(Resources.Load("prefabs/peao") ) as GameObject;	
		}
		if(personagem == null){
			personagem = Instantiate(Resources.Load("prefabs/personagem") ) as GameObject;
		}
		peao.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
		personagem.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());



		peao.name="peao";
		personagem.name = "personagem";



		this.setPersonagem(1,true);
	}
	public JogadorInfo GetInfo(){
		JogadorInfo jog = new JogadorInfo();
		jog.enabled = this.enabled;
		//jog.gameObject = this.gameObject.;
		jog.name = this.name;
		jog.nick = this.nick;
		jog.numeroPersonagem = this.numeroPersonagem;
	//	jog.peao = this.peao;
		jog.pontuacao = this.pontuacao;
		jog.pos_personagem = this.pos_personagem;
		jog.sexo = this.sexo;
		jog.visivel = this.visivel;
		return jog;
	}
	public void SetInfo(JogadorInfo info){
		this.enabled = info.enabled;
		this.nick = info.nick;
		this.nPersonagens = info.nPersonagens;

		this.setPersonagem(info.numeroPersonagem, info.sexo );
		this.Visibilidade(info.visivel);

		this.name = info.name;
		this.pontuacao = info.pontuacao;
		this.pos_personagem = info.pos_personagem;



	}

	public int Pontuacao {
		get {
			return pontuacao;
		}
		set {
			pontuacao=value;
			pontuacao = (pontuacao<0)?0:pontuacao;
		}
	}

	public int NumeroPersonagem {
		get {
			return numeroPersonagem;
		}
	}

	public bool Sexo {
		get {
			return sexo;
		}
	}

	public int NPersonagens {
		get {
			return nPersonagens;
		}
	}

	public string Nick {
		get {
			return nick;
		}
		set {
			this.nick=value;
		}
	}

	public void SetPositionPersonagem(Vector3 v){
		/*RectTransform rt = personagem.GetComponent<RectTransform>();
		rt.position = v;*/
		personagem.transform.localPosition = v;

	}
	public void SetPositionPeao(Vector3 v){
		/*RectTransform rt = peao.GetComponent<RectTransform>();
		rt.position = v;*/
		peao.transform.localPosition=v;

	}
	public void Visibilidade(bool v){
		/*peao.GetComponent<Image>().enabled= visivel;
		personagem.GetComponent<Image>().enabled=visivel;
*/
		this.visivel=v;
		peao.SetActive(this.visivel);
		personagem.SetActive(this.visivel);
	//	Debug.Log("visibilidade = "+this.visivel);
	

	}
	public void VisibilidadePeao(bool isVisi ){
		peao.SetActive(isVisi);
	}
	public void VisibilidadePersonagem(bool isVisi){
		personagem.SetActive(isVisi);
	}


	public void SetScalePersonagem(Vector3 v){
		RectTransform rt = personagem.GetComponent<RectTransform>();
			rt.localScale = v;
	}
	public void SetScalePeao(Vector3 v){
		RectTransform rt = peao.GetComponent<RectTransform>();
		rt.localScale = v;
	}
	// PERSON 1 - 6, SEX true=f - false=m		
	public void setPersonagem(int person,bool sex){

		string path = "";
		string nome = "";
		if(sex){
			nome +="F";
		}else{
			nome+="M";
		}
		if(person<0 && person>5){
			Debug.Log("FORA DO RANGE");
		}
		Color cor_peao;
		switch(person){
		case 1:
			cor_peao= new Color(0,1f,0);
			break;
		case 2:
			cor_peao= new Color(0,0,1f);			
			break;
		case 3:
			cor_peao= new Color(1.000f, 0.000f, 1.00f);
			break;
		case 4:
			cor_peao= new Color(1f,0,0);
			break;
		case 5:
			cor_peao= new Color(1.000f, 0.647f, 0.000f);
			break;
		case 6:
			cor_peao= new Color(0.000f, 0.502f, 0.502f);
			break;
		default:
			cor_peao=new Color(0,0,1);
			break;
		}
		peao.GetComponent<Image>().color = cor_peao;
		nome +=person;
		path= "img/image/"+nome+"";
		//personagem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
		Sprite sp = Resources.Load<Sprite>(path);
		personagem.GetComponent<Image>().sprite=sp ;
		numeroPersonagem= person;
		this.sexo= sex;
			
	}


}
