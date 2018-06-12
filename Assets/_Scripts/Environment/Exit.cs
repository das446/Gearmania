using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour, IContact<Player> {
	public void OnCollision(Player t) {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void OnTrigger(Player t) {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}

}