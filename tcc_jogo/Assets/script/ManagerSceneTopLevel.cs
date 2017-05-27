using UnityEngine;
using System.Collections;
using Google.Cast.RemoteDisplay;

public class ManagerSceneTopLevel : MonoBehaviour {

	public Persistencia persistencia;
	public GameObject tv_camera;
	public GameObject celular_camera;
	private CastRemoteDisplayManager displayManager;

	void Awake(){
	//	persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
	/*	displayManager = CastRemoteDisplayManager.GetInstance();
		displayManager.RemoteDisplayCamera = tv_camera.GetComponent<Camera>() ;
		displayManager.RemoteAudioListener = tv_camera.GetComponent<AudioListener>();
		persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
		Debug.Log("TOP LEVEL");
	*/	setCommom();
	}

	public void setCommom(){
		displayManager = CastRemoteDisplayManager.GetInstance();
		displayManager.RemoteDisplayCamera = tv_camera.GetComponent<Camera>() ;
		displayManager.RemoteAudioListener = tv_camera.GetComponent<AudioListener>();
		persistencia = GameObject.Find("persistencia").GetComponent<Persistencia>();
		Debug.Log("TOP LEVEL");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
