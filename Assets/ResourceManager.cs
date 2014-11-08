using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    private int actualPostIt = 0;
    public GameObject PostItPrefab;
    public float maxDistance = 1.0f;

    public int ActualPostIt{
        get {
            return actualPostIt;
        }
        set {
            actualPostIt = value;
            postItCountUI.text = actualPostIt + "/" + maxPostIt;
        }
    }
    private Text postItCountUI;
    public int maxPostIt;
    public int KeysToWin;
    private int actualKeys;
    private Text keyCountUI;
    private SoundManagerScript soundManager;
    private GameObject camera;
    private GameObject character; 
    ScreenTextManager stm;
    // Use this for initialization
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("soundManager").GetComponent("SoundManagerScript") as SoundManagerScript;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        character = GameObject.FindGameObjectWithTag("character");

        actualKeys = 0;
        keyCountUI = GameObject.FindGameObjectWithTag("keyCount").GetComponent<Text>();
        stm = GameObject.FindGameObjectWithTag("screenText").GetComponent<ScreenTextManager>();
        keyCountUI.text = actualKeys + "/" + KeysToWin;

        postItCountUI = GameObject.FindGameObjectWithTag("postItCount").GetComponent<Text>();
        this.actualPostIt = 10;
    }

    // Update is called once per frame
    void Update()
    {
        throwPostIt();
    }

    public void throwPostIt()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, maxDistance);
            if (isHit && (hitInfo.transform.tag == "pTarget"))
            {
                if (ActualPostIt > 0)
                {
                    ActualPostIt -= 1;
                    soundManager.throwPostIt();
                    GameObject instantiatedThrowable = Instantiate(PostItPrefab, hitInfo.point, hitInfo.transform.rotation) as GameObject;
                }
                else
                    stm.changeText("You don't have post it"); //mensaje de que faltan keys
            }
        }
    }
    public void keyFound()
    {
        actualKeys++;
        stm.changeText("You found a key");
        keyCountUI.text = actualKeys + "/" + KeysToWin;
    }

    internal void tryWin()
    {
        if (actualKeys >= KeysToWin)
            Application.LoadLevel("winGame");
        else
            stm.changeText("You need " + (KeysToWin - actualKeys) + " keys more, fool."); //mensaje de que faltan keys
    }
}
