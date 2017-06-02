using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Google.Cast.RemoteDisplay;

public class MainMenu : MonoBehaviour {
	private GameObject pers;
	private Persistencia persistencia;
	CastRemoteDisplayManager displayManager;
	Camera tv_camera;
	Camera celular_camera;

	void Awake(){
	
		/*pers = new GameObject();
		pers.AddComponent<Persistencia>();
		persistencia = pers.GetComponent<Persistencia>();
		DontDestroyOnLoad(pers);
*/

		//data = new GameObject(); 
		//data.AddComponent<Persistencia>();

		GameObject p = new GameObject();
		p.AddComponent<Persistencia>();
		persistencia = p.GetComponent<Persistencia>();


		DontDestroyOnLoad(this);
		DontDestroyOnLoad(p);

		persistencia = p.GetComponent<Persistencia>();
		tv_camera = GameObject.Find("tv_camera").GetComponent<Camera>();
		celular_camera = GameObject.Find("celular_camera").GetComponent<Camera>();

	//	displayManager = CastRemoteDisplayManager.GetInstance();
	//	displayManager.RemoteDisplayCamera = tv_camera;
	//	displayManager.RemoteAudioListener = GameObject.Find("tv_camera").GetComponent<Camera>().GetComponent<AudioListener>();

		Debug.Log("MAIN");

	

	}
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		//tv_camera.CopyFrom(celular_camera);
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			Application.Quit(); 
		}
	}

	public void OpcaoJogar(){
		
	//	persistencia.CarregarCena(TelaCarregamento.ESCOLHAPERSONAGEM);
		persistencia.ProximacenaIndex = 2;
		SceneManager.LoadScene(TelaCarregamento.CONECTAR_CHROMECAST );

	}
	public void OpcaoTutorial(){
		persistencia.CarregarCena(0);
	}
	public void Opcao_(){
		persistencia.CarregarCena(0);
	}
	public void OpcaoCredito(){
		persistencia.CarregarCena(1);
	}
}
