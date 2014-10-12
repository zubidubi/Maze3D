using UnityEngine;
using System.Collections;

public class ButtonFunctions : MonoBehaviour {

    public void startGame()
    {
        Application.LoadLevel("maze");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
