﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pauser : MonoBehaviour {

	float BaseTimeScale;
	private static bool paused = false;
	public PauseMenu PauseMenu;

	public static bool Paused {
		get {
			return paused;
		}

		private set {
			paused = value;
			Time.timeScale = paused ? 0 : 1;
		}
	}

	// Use this for initialization
	void Start() {
		if (PauseMenu == null) {
			Debug.Log("PauseMenu==null");
			PauseMenu = Resources.FindObjectsOfTypeAll<PauseMenu>()[0];
		}
		BaseTimeScale = Time.timeScale;
		//PauseMenu.gameObject.SetActive(false);
		SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
		//UnPauseGame();
	}

	void SceneManager_activeSceneChanged(Scene arg0, Scene arg1) {
		Debug.Log("Scene Change");
		Paused = false;

	}

	// Update is called once per frame
	void Update() {
		if (Input.GetButtonDown("Pause")) {
			PausePressed();
		}
		if (Paused && !PauseMenu.gameObject.activeSelf) {
			PauseMenu.gameObject.SetActive(true);
		}
//		Debug.Log("Pause Menu Active=" + PauseMenu.gameObject.activeSelf);
	}

	public void PausePressed() {
		//play sound?
		if (!Paused) {
			PauseGame();
		} else {
			UnPauseGame();
		}
		Debug.Log("Pause Menu Active=" + PauseMenu.gameObject.activeSelf);
	}
	void PauseGame() {
		Debug.Log("Paused");
		Paused = true;
		PauseMenu.gameObject.SetActive(true);
		//GameObject.Find("Level").GetComponent<Text>().text = SceneManager.GetActiveScene().name;
		// PauseMenu.PauseButton.gameObject.GetComponentInChildren<Text>().text="Resume";
	}
	public void UnPauseGame() {
		Debug.Log("Unpause");
		Paused = false;
		PauseMenu.gameObject.SetActive(false);
		//  PauseMenu.PauseButton.gameObject.GetComponentInChildren<Text>().text = "Pause";
	}
}