using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;
using Google.Cast.RemoteDisplay;
using UnityEngine.Networking;

public class ManagerScene: MonoBehaviour{ 
	
	private GameObject castUIController;
	private Persistencia persistencia;
	CastRemoteDisplayManager displayManager;
	public ManagerSceneTopLevel mstl;

	void Awake(){
		
	}


	void Start () {
		Debug.Log(" MANAGER SCENE");

		displayManager = CastRemoteDisplayManager.GetInstance();
		displayManager.RemoteDisplaySessionStartEvent.AddListener(OnRemoteDisplaySessionStart);
		displayManager.RemoteDisplaySessionEndEvent.AddListener(OnRemoteDisplaySessionEnd);
		displayManager.RemoteDisplayErrorEvent.AddListener(OnRemoteDisplayError);
		castUIController = GameObject.Find("CastDefaultUI");
		persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();

		DontDestroyOnLoad(this);
		/*
		pausePanel.SetActive(false);
		pauseButton.SetActive(false);
		

		foreach(CastDevice cd in  displayManager.GetCastDevices()){
			Debug.Log("DEVICES ENCONTRADOS: "+cd.DeviceName+" :: "+cd.DeviceId+"  :: " +cd.Status );

		}*/



	}
	void Update(){
		
	}

	public void OnRemoteDisplaySessionStart(CastRemoteDisplayManager manager) {
	/*	castUIController.SetActive(false);
		pauseButton.SetActive(true);
*/
		Debug.Log("SESSION START");
		persistencia.isChromecast=true;
	persistencia.CarregarCena(TelaCarregamento.AGUARDANDO_JOGADORES );
	}
	public void SemCast(){
		persistencia.isChromecast= false;
		persistencia.CarregarCena(TelaCarregamento.AGUARDANDO_JOGADORES );
	}

	public void OnRemoteDisplaySessionEnd(CastRemoteDisplayManager manager) {
		Debug.Log("SESSION END");
		persistencia.CarregarCena(TelaCarregamento.CONECTAR_CHROMECAST );
	}

	public void OnRemoteDisplayError(CastRemoteDisplayManager manager) {

		Debug.Log("DISPLAY ERROR");
	}
}