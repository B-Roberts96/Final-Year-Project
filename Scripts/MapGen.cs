using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



public class MapGen : MonoBehaviour {
    public mineGet resourceSetter;
    public GameObject mapTileBlack;
    public GameObject mapTileRed;
    public int mapWidth;
    public int mapHeight;
    public float startX;
    public float startY;
    public string seed;
    public List<Vector2> tilePositions;
    public GameObject[,] rows;
    public MapTiles[,] tiles;
    public List<List<GameObject>> columns;
  
    bool mining;

   

    void Start() {
      
    }

    public void GenerateMap() {

        columns = new List<List<GameObject>>(mapHeight);
        rows = new GameObject[mapWidth, mapHeight];
        tiles = new MapTiles[mapWidth, mapHeight];


        float newX = 0;
        float newY = 0;
        for (int i = 0; i < mapWidth; i++)
        {
            newX = 0;
            tilePositions.Add(new Vector2(newX, newY));
            for (int j = 0; j < mapHeight; j++)
            {
                rows[i, j] = (GameObject)Instantiate(mapTileBlack, new Vector2(newX, newY), Quaternion.identity);
                tiles[i, j] = rows[i, j].GetComponent<MapTiles>();
                tiles[i, j].Init();
                tiles[i, j].setPosition(i, j);
                resourceSetter.setWater(i, j);
                resourceSetter.setIron(i, j);
                newX = startX + (j + 1);
            }
            newY = startY + (i + 1);
        }
    }


    public void addNeighbours() {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                tiles[i, j].addNeighbours(tiles);
            }
        }
    }
   public List<Vector2> getPositions()
    {
        return tilePositions;
    }

    public GameObject[,] getTiles()
    {
        return rows;
    }

    public MapTiles[,] getMapTiles()
    {
        return tiles;
    }
    public void setMining(bool inMining)
    {
        mining = inMining;
    }
    public bool getMining()
    {
        return mining;
    }
   
}
