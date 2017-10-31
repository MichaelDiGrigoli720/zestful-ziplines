using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void newGameButton(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void exitGameButton()
    {
        Application.Quit();
    }
}
