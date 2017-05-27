using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Casa : MonoBehaviour {
	private int nivel;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void setNivel(int n){
		nivel = n;
		Image im = this.GetComponent<Image>();
		switch (nivel){
		case 0:
			im.color = new Color(0.2f,1.0f,0.0f);
			break;
			case 1:
			im.color = new Color(0.2f,1.0f,1f);
			break;
			case 2:
			im.color = new Color(1f,0.0f,0.0f);
			break;
		}
	}
	public int getNivel(){
		return this.nivel;
	}


}
