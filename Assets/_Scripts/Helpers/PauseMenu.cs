using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Button PauseButton,Main,LevelSelect;
    public Pauser p;
    //Settings
    public Button InputType;

	// Use this for initialization
    public void Start()
    {
		p = FindObjectOfType<Pauser>();
        gameObject.SetActive(false);
    }
	public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void UnPause()
    {
        p.UnPauseGame();
    }
    public void GoToSettings()
    {
        AbleButtons(false,PauseButton, LevelSelect);
    }
    public void GoBackFromSettings()
    {

    }
    public void ChangeInput()
    {
        
    }

    public void AbleButtons( bool Enable, params Button[] Buttons)
    {
        foreach(Button b in Buttons)
        {
            b.gameObject.SetActive(Enable);
        }
    }
}
