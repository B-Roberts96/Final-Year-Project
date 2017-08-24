using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinder : MonoBehaviour
{
   public List<MapTiles> openSet;
   public List<MapTiles> closedSet;
    public MapGen mapGen;
    Vector2 start;
    Vector2 end;
    MapTiles[,] tiles;
    public List<MapTiles> path;
    public ControlClass control;
    bool noSolution;

    

    public void Init() {
       tiles = new MapTiles[mapGen.mapWidth, mapGen.mapHeight];
        tiles = mapGen.tiles;
        openSet = new List<MapTiles>();
        closedSet = new List<MapTiles>();
        noSolution = false;


    }

    public List<MapTiles> Pathfind(Vector2 startPos, Vector2 endPos) {
        
        try
        {
            start = startPos;
            end = endPos;

            openSet.Add(tiles[(int)start.x, (int)start.y]);

            while (openSet.Count > 0)
            {
                if (openSet.Count > 0)
                {
                    int lowestIndex = 0;
                    for (int i = 0; i < openSet.Count; i++)
                    {
                        if (openSet[i].f < openSet[lowestIndex].f)
                        {
                            lowestIndex = i;
                        }
                    }

                    MapTiles current = openSet[lowestIndex];

                    if (current.position == end)
                    {
                        if (!noSolution)
                        {
                            path = new List<MapTiles>();
                            MapTiles temp = current;

                            path.Add(temp);
                            while (temp.previous && temp.previous != null)
                            {
                                path.Add(temp.previous);
                                temp = temp.previous;
                            }
                            
                            return path;
                        }

                    }
                    removeFromArray(openSet, current);
                    closedSet.Add(current);

                    List<MapTiles> neighbours = current.neighbours;
                    for (int i = 0; i < neighbours.Count; i++)
                    {
                        MapTiles neighbour = neighbours[i];
                        if (!closedSet.Contains(neighbour) && neighbour.walkable)
                        {
                            int tempG = current.g + 1;

                            bool newPath = false;
                            if (openSet.Contains(neighbour))
                            {
                                if (tempG < neighbour.g)
                                {
                                    neighbour.g = tempG;
                                    newPath = true;
                                }
                            }
                            else
                            {
                                neighbour.g = tempG;
                                newPath = true;
                                openSet.Add(neighbour);
                            }
                            if (newPath)
                            {
                                neighbour.h = heuristic(neighbour, tiles[(int)end.x, (int)end.y]);
                                neighbour.f = neighbour.g + neighbour.h;
                                neighbour.previous = current;
                            }
                        }
                    }
                }
                else
                {
                    noSolution = true;
                    break;
                }

            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return path;
        }
        return path;
    }

    void removeFromArray(List<MapTiles> inTiles, MapTiles inCurrent) {
        for (int i = inTiles.Count - 1; i >= 0 ; i--) {
            if (inTiles[i] == inCurrent) {
                inTiles.RemoveAt(i);
            }
        }
    }

    float heuristic(MapTiles a, MapTiles b) {

        float dist = System.Math.Abs(a.position.x - b.position.x) + System.Math.Abs(a.position.y - b.position.y);
        return dist;
    }

    public void ResetValues() {
        for (int i = 0; i < tiles.GetLength(0); i++) {
            for (int j = 0; j < tiles.GetLength(1); j++) {
                tiles[i,j].f = 0;
                tiles[i, j].g = 0;
                tiles[i, j].h = 0;
                tiles[i, j].previous = null;
            }
        }
        noSolution = false;
        openSet.Clear();
        closedSet.Clear();
    }
}
