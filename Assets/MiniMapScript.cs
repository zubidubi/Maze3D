using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Timers;
using System;
public class MiniMapScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

        //For placing the image of the mini map.
    public GUIStyle miniMap;
    //Two transform variables, one for the player's and the enemy's,
    public Transform player;
    public List<Transform> demons;
    public List<Transform> keys;
    public List<Transform> batteries;
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
    public int sceneWidth = 60;
    public int sceneHeight = 51;
    //The size of your player's and enemy's icon on the map.
    int iconSize = 20;
    private int iconHalfSize;

    void Update () {
        
        iconHalfSize = iconSize/2;
    }

    float GetMapPosX(float pos, float mapSize, float sceneSize)
    {
        return (((pos * mapSize / sceneSize) * -1) - iconHalfSize) + (mapWidth / 2);
    }

    float GetMapPosY(float pos, float mapSize, float sceneSize)
    {
        return (((pos * mapSize / sceneSize) + iconHalfSize) * -1) + (mapHeight);
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(mapOffSetX, mapOffSetY, mapWidth, mapHeight), miniMap);
        float playerMapX = GetMapPosX(transform.position.z, mapWidth, sceneWidth);
        float playerMapY = GetMapPosY(transform.position.x + 20, mapHeight, sceneHeight);
        GUI.Box(new Rect(playerMapX, playerMapY, iconSize, iconSize), "", playerIcon);

        /*foreach(Transform demon in this.demons)
        {
            float enemyMapX = GetMapPosX(demon.transform.position.z, mapWidth, sceneWidth);
            float enemyMapY = GetMapPosY(demon.transform.position.x + 20, mapHeight, sceneHeight);
            GUI.Box(new Rect(enemyMapX, enemyMapY, iconSize, iconSize), "", demonIcon);
        }*/

        foreach (Transform key in this.keys)
        {
            float keyMapX = GetMapPosX(key.transform.position.z, mapWidth, sceneWidth);
            float keyMapY = GetMapPosY(key.transform.position.x + 20, mapHeight, sceneHeight);
            GUI.Box(new Rect(keyMapX, keyMapY, iconSize, iconSize), "", keyIcon);
        }

        float treasureMapX = GetMapPosX(treasure.transform.position.z, mapWidth, sceneWidth);
        float treasureMapY = GetMapPosY(treasure.transform.position.x + 20, mapHeight, sceneHeight);
        GUI.Box(new Rect(treasureMapX, treasureMapY, iconSize, iconSize), "", treasureIcon);
        
        GUI.EndGroup();
    }


}
