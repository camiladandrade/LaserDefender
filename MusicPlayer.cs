using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public static int musicCounter = 0;
	
	void Awake (){
		Debug.Log("Music player Awake " + GetInstanceID());
		musicCounter++;
		if(musicCounter > 1){
			GameObject.DestroyObject(gameObject);
			
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log("Music player Start " + GetInstanceID());
		GameObject.DontDestroyOnLoad(gameObject);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
