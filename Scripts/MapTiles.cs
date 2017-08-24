using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapTiles : MonoBehaviour
{
 
    public GameObject tile;
    public MapGen mapGen;
    public double f = 0;
    public int g = 0;
    public double h = 0;
    public List<MapTiles> neighbours;
    public Vector2 position;
    public MapTiles previous;
    public bool walkable = true;
    public int water;
    public int iron;

    public void Init() {
        neighbours = new List<MapTiles>() ;
    }
    public void setPosition(int x, int y)
    {
        position = new Vector2(x, y);
    }

   public void addNeighbours(MapTiles[,] grid)
    {
        if((int)position.x < mapGen.mapWidth - 1) 
        {
            neighbours.Add(grid[(int)position.x + 1, (int)position.y]);
        }
        if ((int)position.x > 0)
        {
            neighbours.Add(grid[(int)position.x - 1, (int)position.y]);
        }
        if ((int)position.y < mapGen.mapHeight - 1)
        {
            neighbours.Add(grid[(int)position.x, (int)position.y + 1]);
        }
        if ((int)position.y > 0)
        {
            neighbours.Add(grid[(int)position.x, (int)position.y - 1]);
        }

        if ((int)position.y > 0 && (int)position.x > 0)
        {
            neighbours.Add(grid[(int)position.x - 1, (int)position.y - 1]); //bottom left
        }


        if ((int)position.y > 0 && (int)position.x < mapGen.mapWidth - 1)
        {
            neighbours.Add(grid[(int)position.x + 1, (int)position.y - 1]); // bottom right
        }


        if ((int)position.y < mapGen.mapHeight - 1 && (int)position.x > 0)
        {
            neighbours.Add(grid[(int)position.x - 1, (int)position.y + 1]); //top left
        }


        if ((int)position.y < mapGen.mapHeight - 1 && (int)position.x < mapGen.mapWidth - 1     )
        {
            neighbours.Add(grid[(int)position.x + 1, (int)position.y + 1]);
        }
    }
}



    



