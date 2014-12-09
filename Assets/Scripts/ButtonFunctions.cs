using UnityEngine;
using System.Collections;

public class ButtonFunctions : MonoBehaviour {

    public void startGame()
    {
        Application.LoadLevel("maze");
    }

    public void credits()
    {
        Application.LoadLevel("credits");
    }

    public void mainMenu()
    {
        Application.LoadLevel("start");
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
