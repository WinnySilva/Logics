using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ExplicacaoManager : ManagerSceneTopLevel {
	public VideoPlayer videoVideo;
	public AudioSource videoAudio;
	void Awake(){
		base.setCommom();
	}

	void Start(){
		videoVideo.Play();
		videoAudio.Play();
	}
	void Update(){
		if(!videoVideo.isPlaying){
			base.persistencia.CarregarCena(TelaCarregamento.PARTIDA);	
		}
	}

	public void PularExplicacao(){
		videoVideo.Stop();
		videoAudio.Stop();
	}
}
