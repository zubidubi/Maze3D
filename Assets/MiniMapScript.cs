using UnityEngine;
using System.Collections;

public class MiniMapScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

        //For placing the image of the mini map.
    public GUIStyle miniMap;
    //Two transform variables, one for the player's and the enemy's,
    public Transform player;
    public Transform demon;
    public Transform key;
    public Transform battery;
    public Transform treasure;
    //Icon images for the player and enemy(s) on the map.
    public GUIStyle demonIcon;
    public GUIStyle playerIcon;
    public GUIStyle keyIcon;
    public GUIStyle treasureIcon;
    public GUIStyle batteryIcon;
    //Offset variables (X and Y) - where you want to place your map on screen.
    public int mapOffSetX = 762;
    public int mapOffSetY = 510;
    //The width and height of your map as it'll appear on screen,
    public int mapWidth = 200;
    public int mapHeight = 200;
    //Width and Height of your scene, or the resolution of your terrain.
    public int sceneWidth = 1920;
    public int sceneHeight = 1080;
    //The size of your player's and enemy's icon on the map.
    int iconSize = 50;
    private int iconHalfSize;

    void Update () {
        
        iconHalfSize = iconSize/2;
    }

    float GetMapPos(float pos, float mapSize, float sceneSize)
    {
        return pos * mapSize / sceneSize;
    }


    void OnGUI()
    {
        GUI.BeginGroup(new Rect(mapOffSetX, mapOffSetY, mapWidth, mapHeight), miniMap);
        float pX = GetMapPos(transform.position.x, mapWidth, sceneWidth);
        float pZ = GetMapPos(transform.position.z, mapHeight, sceneHeight);
        float playerMapX = pX - iconHalfSize;
        float playerMapZ = ((pZ * -1) - iconHalfSize) + mapHeight;
        GUI.Box(new Rect(playerMapX, playerMapZ, iconSize, iconSize), "", playerIcon);

        float sX = GetMapPos(demon.transform.position.x, mapWidth, sceneWidth);
        float sZ = GetMapPos(demon.transform.position.z, mapHeight, sceneHeight);
        float enemyMapX = sX - iconHalfSize;
        float enemyMapZ = ((sZ * -1) - iconHalfSize) + mapHeight;
        GUI.Box(new Rect(enemyMapX, enemyMapZ, iconSize, iconSize), "", demonIcon);
        GUI.EndGroup();
    }


}
