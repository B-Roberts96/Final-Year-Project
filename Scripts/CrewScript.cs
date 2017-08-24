using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CrewScript : MonoBehaviour
{
    public Camera gameCam;
    public CrewScript player;
    public GameObject commandPanel;
    public GameObject structPanel;
    public ControlClass control;
    public Pathfinder pathFinder;
    public string name;
    public Inventory inventory;
    public bool sleep = false;
    bool timerStarted = false;
    public Timer inTimer;
    Timer timer;
    Timer statTimer;
   
    public float timestep;
    float statTimestep;
    public List<string> actions;
    private List<Vector2> destinations;
    private List<Vector2> pathDestinations;
    private List<Vector2> tileRef;
    private List<MapTiles> path;
    public int mineSkill;
    public bool mining = false;
   public int health;
    public int hydration;
    System.Random rnd;

    public Pathfinder Pathfinder
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
        }
    }

    void Start()
    {
        rnd = new System.Random();
        actions = new List<string>();
        tileRef = new List<Vector2>();
        destinations = new List<Vector2>();
        pathDestinations = new List<Vector2>();
        path = new List<MapTiles>();
        timer = Instantiate(inTimer);
        statTimer = Instantiate(inTimer);
        statTimer.Start();
        mineSkill = 11 - rnd.Next(1, 10);
        hydration = 100;
        health = 100;
        
    }

    void Update()
    {
        statTimestep += statTimer.GetElapsedSeconds();
        if (hydration < 20)
        {
            if (inventory.Contains("water") && (int)statTimestep == 1)
            {
                Drink();
            }
            else {
                CheckActions();
            }
        }
        else
        {
            CheckActions();
        }
        if (mining == true && (int)statTimestep == 2 && hydration != 0)
        {
            hydration -= 1;
            statTimestep = 0;
        }
        else if ((int)statTimestep == 5 && hydration != 0)
        {
            hydration -= 1;
            statTimestep = 0;
        }
        if (hydration == 0 && (int)statTimestep == 1)
        {
            health--;
            statTimestep = 0;
        }

        if (health == 0) {
            Destroy(player.gameObject);
        }
    }

    public void AddAction(string inAction)
    {
        actions.Add(inAction);
    }

    public void SetDestinationPlayer(Vector2 inDestination)
    {
        pathDestinations.Add(inDestination);
    }

    public void SetTileReference(int inTileRef1, int inTileRef2)
    {
       tileRef.Add(new Vector2(inTileRef1, inTileRef2));
    }

    public void RemoveAction()
    {
        actions.Remove(actions[0]);
        pathDestinations.Remove(pathDestinations[0]);
        if(tileRef.Count != 0)
        tileRef.Remove(tileRef[0]);
        mining = false;
        timerStarted = false;
    }

    public void Drink() {

        inventory.TakeItems("water", 1);
        hydration += 10;
        statTimestep = 0;
    }

    public void RemoveAllActions() {
        actions.Clear();
        pathDestinations.Clear();
        tileRef.Clear();
        mining = false;
        timerStarted = false;
    }
    void CheckActions()
    {
        if(actions.Count == 0)
        {
            return;
        }
        else
        {
            if (actions[0] == "moving")
            {
                if (destinations.Count == 0 && path.Count == 0)
                {

                    pathFinder.Init();
                    path = pathFinder.Pathfind(player.transform.position, pathDestinations[0]);
                    pathFinder.ResetValues();

                    for (int i = path.Count - 1; i >= 0; i--)
                    {
                        destinations.Add(path[i].position);
                    }
                    if (path.Count != 0)
                    {
                        Moving(destinations[0], true);
                    }
                    else
                    {
                        pathDestinations.Clear();
                        actions.Clear();
                    }
                }
                else
                {
                    Moving(destinations[0], true);
                }
            }
            else if (actions[0] == "water")
            {
                if (!timerStarted)
                {
                    timer.Start();
                    timerStarted = true;
                }
                mining = true;
                MineWater();
            }
            else if (actions[0] == "iron")
            {

                if (!timerStarted)
                {
                    timer.Start();
                    timerStarted = true;
                }
                mining = true;
                MineIron();
            }
            else if (actions[0] == "sleep") {
                Sleep();
            }
        }
    }
    void Sleep() {

    }
    void Moving(Vector2 destination, bool moving)
    {
        if (moving && destination != null)
        {

            if (destination.x > player.transform.position.x)
            {
                if (player.transform.position.x + 0.1f > destination.x)
                {
                    player.transform.position = new Vector3((float)System.Math.Round(player.transform.position.x, 0), player.transform.position.y, -0.01f);
                    //  collider.setPlayerPos(player.transform.position);
                }
                else
                {
                    player.transform.position = new Vector3(player.transform.position.x + 0.1f, player.transform.position.y, -0.01f);
                    // collider.setPlayerPos(player.transform.position);
                }
            }
            else if (destination.x < player.transform.position.x)
            {
                if (player.transform.position.x - 0.1f < destination.x)
                {
                    player.transform.position = new Vector3((float)System.Math.Round(player.transform.position.x, 0), player.transform.position.y, -0.01f);
                    //  collider.setPlayerPos(player.transform.position);
                }
                else
                {
                    player.transform.position = new Vector3(player.transform.position.x - 0.1f, player.transform.position.y, -1f);
                    //  collider.setPlayerPos(player.transform.position);
                }
            }
            if (destination.y < player.transform.position.y)
            {
                if (player.transform.position.y - 0.1f < destination.y)
                {
                    player.transform.position = new Vector3(player.transform.position.x, (float)System.Math.Round(player.transform.position.y,0), -1f);
                    //    collider.setPlayerPos(player.transform.position);
                }
                else
                {
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.1f, -1f);
                    //    collider.setPlayerPos(player.transform.position);
                }
            }
            else if (destination.y > player.transform.position.y)
            {

                if (player.transform.position.y + 0.1f > destination.y)
                {
                    player.transform.position = new Vector3(player.transform.position.x, (float)System.Math.Round(player.transform.position.y, 0), -1f);
                    //   collider.setPlayerPos(player.transform.position);
                }
                else
                {
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.1f, -1f);
                    //   collider.setPlayerPos(player.transform.position);
                }
            }
            if (new Vector2((float)System.Math.Round(player.transform.position.x, 1), (float)System.Math.Round(player.transform.position.y)) == destination && destinations.Count > 0)
            {
             
                player.transform.position = destination;
                destinations.RemoveAt(0);

            }
            if (new Vector2((float)System.Math.Round(player.transform.position.x, 1), (float)System.Math.Round(player.transform.position.y)) == destination && destinations.Count == 0)
            {
                moving = false;
                player.transform.position = new Vector3(destination.x,destination.y, -0.01f);
                path.Clear();
                actions.Remove(actions[0]);

            }
        }

    }

    void MineWater()
    {
        
        timestep += timer.GetElapsedSeconds();
       
       // text.text = timestep.ToString();
        if(pathDestinations[0] == new Vector2(player.transform.position.x, player.transform.position.y) && (int)timestep >= 11 - mineSkill)
        {
            
            control.Mine(tileRef[0], "water", player);
            timestep = 0;
        }

        if (timestep > 11 - mineSkill)
        {
            timestep = 0;
        }
    }

    void MineIron()
    {
        timestep += timer.GetElapsedSeconds();
      
        if (pathDestinations[0] == new Vector2(player.transform.position.x, player.transform.position.y) && (int)timestep >= 11 - mineSkill)
        {
            control.Mine(tileRef[0], "iron", player);
            timestep = 0;
        }
        if (timestep > 11 - mineSkill)
        {
            timestep = 0;
        }
    }

   
  
}

