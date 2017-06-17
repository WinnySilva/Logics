using System.Collections;
using Google.Cast.RemoteDisplay;
using UnityEngine;

public class ManagerSceneTopLevel : MonoBehaviour {

	public Persistencia persistencia;
	public GameObject tv_camera;
	public GameObject celular_camera;
	public CastRemoteDisplayManager displayManager;
	private CastDevice dv;

	void Awake(){
	//	persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
		displayManager = CastRemoteDisplayManager.GetInstance();
		displayManager.RemoteDisplayCamera = tv_camera.GetComponent<Camera>() ;
		displayManager.RemoteAudioListener = tv_camera.GetComponent<AudioListener>();
		persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
		Debug.Log("TOP LEVEL");
		setCommom();
	}

	public void setCommom(){
		displayManager = CastRemoteDisplayManager.GetInstance();
		displayManager.RemoteDisplayCamera = tv_camera.GetComponent<Camera>() ;
		displayManager.RemoteAudioListener = tv_camera.GetComponent<AudioListener>();
		persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
		Debug.Log("MANAGER SCENE TOP LEVEL");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//Debug.Log(""+debugInfo() );
	}

	public string debugInfo(){
		string msg="";
		dv = displayManager.GetSelectedCastDevice();
		Debug.Log("displayManager.enabled: "+displayManager.enabled);
		if(dv != null){
			msg=(" DeviceId: "+dv.DeviceId+" DeviceName: "+dv.DeviceName+" Status:"+dv.Status+" displayManager.IsCasting: "+displayManager.IsCasting());
		}else{
			msg=(" chromecast null");
		}
		return msg;
	}

	public void FlushChromecast(){
		
	}
	public void OnRemoteDisplaySessionStart(CastRemoteDisplayManager manager) {
		/*	castUIController.SetActive(false);
		pauseButton.SetActive(true);
*/
		Debug.Log("SESSION START");
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
