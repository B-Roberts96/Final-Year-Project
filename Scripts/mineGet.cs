using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mineGet : MonoBehaviour {
    int mine = 0;
    public MapGen mapGen;
   int[,] waterList;
    int[,] ironList;


    public void Init() {
        waterList = new int[mapGen.mapWidth, mapGen.mapHeight];
        ironList = new int[mapGen.mapWidth, mapGen.mapHeight];
    }
    public void setWater(int i, int j)
    {
        System.Random seed = new System.Random();
        System.Random rnd = new System.Random(seed.GetHashCode());
        mine = rnd.Next(0, 5);
        mapGen.tiles[i, j].water = mine;
      //  waterList[i, j] = mine;
       
    }

    public void setIron(int i, int j)
    {
        System.Random seed = new System.Random();
        System.Random rnd = new System.Random(seed.GetHashCode());
        mine = rnd.Next(0, 30);
        mapGen.tiles[i, j].iron = mine;
      //  ironList[i, j] = mine;

    }

    public int[,] GetWaterList()
    {
        return waterList;
    }
    public int[,] GetIronList()
    {
        return ironList;
    }
}
