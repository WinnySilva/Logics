using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class MultiplayerManager : MonoBehaviour {
	public NetworkManager nm;
	public NetworkManagerHUD nmHUD;
	public NetworkClient nc;
	public NetworkServer ns;
	public NetworkLobbyManager nlm;
	public NetworkDiscovery nd;

	// Use this for initialization
	void Start () {
	//	nd.broadcastsReceived
	//	nc = nm.StartHost();
	//	nm.StartMatchMaker();
	//	StringMessage mb = new StringMessage("huehuehe");
		Debug.Log(" -- | --");
		//nc.Send(5,mb);
		/*nm.StartMatchMaker();
		foreach(UnityEngine.Networking.Match.MatchInfoSnapshot mis in nm.matches  ){
			Debug.Log("ID "+ mis.networkId+" "+mis.name +" "+mis.hostNodeId.ToString());
		}*/

		/*
		nm.OnStartClient = OnStartClient;
		nm.OnStartHost = OnStartHost;
		nm.OnStopClient = OnStopClient;
		*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartHost(){
		nc = nm.StartHost();
		nd.broadcastData = nc.serverIp;
		Debug.Log( nd.broadcastData );
	}
	public void FindHost(){
		nc = nm.StartClient();
		Debug.Log( nd.broadcastData );
	}


	public  void OnStartHost()
	{
		nd.Initialize();
		nd.StartAsServer();

	}

	public void OnStartClient(NetworkClient client)
	{
		nd.showGUI = false;
	}

	public void OnStopClient()
	{
		nd.StopBroadcast();
		nd.showGUI = true;
	}


}
