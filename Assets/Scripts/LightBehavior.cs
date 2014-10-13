using UnityEngine;
using System.Collections;
using System.Timers;

public class LightBehavior : MonoBehaviour {

    private Light light;
    public float bateryEnergy;
	// Use this for initialization
	void Start () 
    {
        light = (Light)GetComponent("Light");
        light.enabled = true;
        InvokeRepeating("bateryDischarge",0.1f,0.1f);
	}
	
	// Update is called once per frame
	void Update () 
    {        
        if (Input.GetButtonDown("Light Turn On/Off"))
        {
            lightSwitch();            
        }
	}

    private void lightSwitch()
    {
        if (light.enabled)
        {
            light.enabled = false;
            CancelInvoke("bateryDischarge");
        }
        else
        {
            if (bateryEnergy > 0)
            {
                light.enabled = true;
                InvokeRepeating("bateryDischarge", 0.1f, 0.1f);
            }
        }
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void bateryDischarge()
    {
        bateryEnergy -= 0.1f;
        if (bateryEnergy > 100)
            bateryEnergy = 100;
        GameObject batteryHUD = GameObject.FindGameObjectWithTag("batteryHUD");
        ((GUIBarScript)batteryHUD.GetComponent("GUIBarScript")).SetNewValue(bateryEnergy / 100);
        ((GUIBarScript)batteryHUD.GetComponent("GUIBarScript")).ForceUpdate();
        if (bateryEnergy <= 0)
        {
            light.enabled = false;
            CancelInvoke("bateryDischarge");
        }
    }
}
