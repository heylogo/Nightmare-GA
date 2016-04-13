using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {


	Canvas canvas;
	// public  int currentState = 1;
	// int flag = 0; //0, first time entering condition 1, already entered condition

	void Start()
	{
		canvas = GetComponent<Canvas>();
		// canvas.enabled = !canvas.enabled;
		// Pause();
	}

	void Update()
	{
		//var textArea = new Rect(0,0,Screen.width, Screen.height);
		//GUI.Label(textArea,"Debug Info:\nLevel:");
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
		// if (currentState == 0 && flag == 0) //game won or ended, trigger menu
		// {
		// 	flag = 1;
		// 	canvas.enabled = !canvas.enabled;
		// 	Pause();
		// }
		// Debug.Log ("Update Current State " + currentState + " Flag " + flag);
	}

	// public void Level()
	// {
	// 	if(Application.loadedLevelName == "Level 01")
	// 	{
	// 		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	// 		Application.LoadLevel ("Level 02");
	// 	}
	// 	else if(Application.loadedLevelName == "Level 02")
	// 	{
	// 		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	// 		Application.LoadLevel ("Level 01");
	// 	}
	// }

	public void SpeedSwitch ()
	{
		Time.timeScale = Time.timeScale == 5 ? 1 : 5;
	}
	public void Pause()
	{
		canvas.enabled = !canvas.enabled;
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	}

	public void Quit()
	{
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
