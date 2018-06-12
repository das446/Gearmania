using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void StartGame(){
		this.PlaySound("Select");
		SceneManager.LoadScene(2);
	}

	public void GoToHelp(){
		this.PlaySound("Select");
		SceneManager.LoadScene(1);
	}
}
