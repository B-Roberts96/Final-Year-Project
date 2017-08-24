using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ControlClass : MonoBehaviour
{
    List<Vector2> tilePositions;
    public List<BedScript> beds;
    GameObject[,] tiles;
    List<CrewScript> crewList = new List<CrewScript>();
    int[,] waterList;
    int[,] ironList;
    public MapTiles[,] mapTilesArray;
    public Pathfinder pathfinder;
    public CrewScript player;
    public MapGen mapGenerator;
    public Camera gameCam;
    public CrewScript crew1;
    public CrewScript crew2;
    public CrewScript crew3;
    public GameObject wall1;
    public GameObject wallGhost;
    public GameObject bed;
    public GameObject bedGhost;
    public GameObject solarPanelGhost;
    public GameObject solarPanel;
    public GameObject smokeGhost;
    public GameObject smokeProducer;
    public GameObject commandPanel;
    public GameObject structPanel;
    public SendHere mineWater;
    public SendHere mineIron;
    public GameObject crewButton;
    public CrewUI crewUI;
    public mineGet getMine;
    public GameObject inventoryButton;
    public Inventory inventory;
    public mineUIText waterUIValue;
    public mineUIText ironUIValue;
    public Text terraformingText;
    public Text timeText;
    public Text tempText;
    public Text powerText;
    public CrewPanelText crewPanelText;
    public int globalTerraformingProgression;
    public int power;
    buttonClick buttonInitialiser;
    public Timer timer;
    float timestep;
    Vector2 destination;
    int dayNo;
    int minutes;
    public int hours;
    CrewScript crew;
    Vector2 clickedPos;

    bool mining = false;

   

    void Start()
    {
        populateCrew();
        getMine.Init();
        globalTerraformingProgression = 0;
        power = 0;
        hours = 7;
        mapGenerator.GenerateMap(); //generates a map of specified width and height
        mapTilesArray = mapGenerator.tiles;
        pathfinder.Init();
        tilePositions = mapGenerator.getPositions(); //loads in the positions of each tile
        tiles = mapGenerator.getTiles(); //loads in the tile gameobjects
        crewUI.Start();
        buttonInitialiser = crewButton.GetComponent<buttonClick>();
        buttonInitialiser.panelInit();
        buttonInitialiser = inventoryButton.GetComponent<buttonClick>();
        buttonInitialiser.panelInit();
        System.Random tempRND = new System.Random();


        mapGenerator.addNeighbours();

    }

    void Update()
    {
        timestep += timer.GetElapsedSeconds();
        Clock();

        terraformingText.text = "Terraforming progression: " + globalTerraformingProgression.ToString() + " / 100";
        powerText.text = power.ToString() + " J";

        Vector3 mousePos = gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1));

        for (int j = 0; j < tiles.GetLength(0); j++)
        {
            for (int i = 0; i < tiles.GetLength(1); i++)
            {
                if (!commandPanel.activeSelf && tiles[j, i].activeSelf && mousePos.x <= tiles[j, i].transform.position.x + 0.5f && mousePos.x >= tiles[j, i].transform.position.x - 0.5f && mousePos.y <= tiles[j, i].transform.position.y + 0.5f && mousePos.y >= tiles[j, i].transform.position.y - 0.5f)
                {
                    waterUIValue.textChange(mapTilesArray[j, i].water);
                    ironUIValue.textChange(mapTilesArray[j, i].iron);
                    break;
                }
                else if (!tiles[j, i].activeSelf)
                {
                    //  waterUIValue.textChange(0);
                    //ironUIValue.textChange(0);
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && !commandPanel.activeSelf && !bedGhost.activeSelf && !wallGhost.activeSelf && !solarPanelGhost.activeSelf && !smokeGhost.activeSelf && !structPanel.activeSelf && Input.mousePosition.y > 20)
        {
            updateClickedPos();
            try
            {
                player = getCrew();

            }
            catch
            {

            }

            tilePositionsCheck();
        }

        else if (wallGhost.activeSelf)
        {
            for (int j = 0; j < mapTilesArray.GetLength(0); j++)
            {
                for (int i = 0; i < mapTilesArray.GetLength(1); i++)
                {
                    if ((gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).x <= mapTilesArray[i, j].position.x + 0.5f && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).x >= mapTilesArray[i, j].position.x - 0.5f
                                      && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).y <= mapTilesArray[i, j].position.y + 0.5f && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).y >= mapTilesArray[i, j].position.y - 0.5f) && tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y].activeSelf)
                    {


                        wallGhost.transform.position = new Vector3(mapTilesArray[i, j].position.x, mapTilesArray[i, j].position.y, -0.03f); ;
                        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && tiles[(int)mapTilesArray[i, j].position.x, (int)mapTilesArray[i, j].position.y].activeSelf)
                        {
                            for (int k = 0; k < inventory.inventoryCount.Length; k++)
                            {
                                if (inventory.inventoryString[k] == "iron" && inventory.inventoryCount[k] >= 1)
                                {
                                    wall1.SetActive(true);
                                    Instantiate(wall1, mapTilesArray[i, j].position, Quaternion.identity);
                                    mapTilesArray[(int)mapTilesArray[i, j].position.x, (int)mapTilesArray[i, j].position.y].walkable = false;
                                    tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y].SetActive(false);
                                    inventory.TakeItems("iron", 1);
                                    inventory.UpdateText(k);

                                    break;
                                }
                            }

                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                wallGhost.SetActive(false);
            }
        }
        else if (solarPanelGhost.activeSelf)
        {
            for (int j = 0; j < mapTilesArray.GetLength(0); j++)
            {
                for (int i = 0; i < mapTilesArray.GetLength(1); i++)
                {
                    if (j != 0 && (gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).x <= mapTilesArray[i, j].position.x + 0.5f && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).x >= mapTilesArray[i, j].position.x - 0.5f
                                      && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).y <= mapTilesArray[i, j].position.y + 0.5f && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).y >= mapTilesArray[i, j].position.y - 0.5f) && tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y].activeSelf && tiles[(int)mapTilesArray[j, i].position.x - 1, (int)mapTilesArray[j, i].position.y].activeSelf)
                    {
                        solarPanelGhost.transform.position = new Vector3(mapTilesArray[i, j].position.x, mapTilesArray[i, j].position.y - .5f, -0.03f);
                        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && tiles[(int)mapTilesArray[i, j].position.x, (int)mapTilesArray[i, j].position.y].activeSelf)
                        {

                            for (int k = 0; k < inventory.inventoryCount.Length; k++)
                            {
                                if (inventory.inventoryString[k] == "iron" && inventory.inventoryCount[k] >= 2)
                                {
                                    solarPanel.SetActive(true);
                                    Instantiate(solarPanel, new Vector3(mapTilesArray[i, j].position.x, mapTilesArray[i, j].position.y - .5f, 0), Quaternion.identity);
                                    mapTilesArray[(int)mapTilesArray[i, j].position.x, (int)mapTilesArray[i, j].position.y].walkable = false;
                                    mapTilesArray[(int)mapTilesArray[i, j].position.x, (int)mapTilesArray[i, j].position.y - 1].walkable = false;

                                    tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y].SetActive(false);
                                    tiles[(int)mapTilesArray[j, i].position.x - 1, (int)mapTilesArray[j, i].position.y].SetActive(false);

                                    inventory.TakeItems("iron", 2);
                                    inventory.UpdateText(k);
                                }
                            }
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                solarPanelGhost.SetActive(false);
            }
        }
        else if (bedGhost.activeSelf)
        {
            for (int j = 0; j < mapTilesArray.GetLength(0); j++)
            {
                for (int i = 0; i < mapTilesArray.GetLength(1); i++)
                {
                    if (j != 0 && (gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).x <= mapTilesArray[i, j].position.x + 0.5f && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).x >= mapTilesArray[i, j].position.x - 0.5f
                                      && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).y <= mapTilesArray[i, j].position.y + 0.5f && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).y >= mapTilesArray[i, j].position.y - 0.5f) && tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y].activeSelf && tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y + 1].activeSelf)
                    {
                        bedGhost.transform.position = new Vector3(mapTilesArray[i, j].position.x + .5f, mapTilesArray[i, j].position.y, -0.03f);
                        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && tiles[(int)mapTilesArray[i, j].position.x, (int)mapTilesArray[i, j].position.y].activeSelf)
                        {

                            for (int k = 0; k < inventory.inventoryCount.Length; k++)
                            {
                                if (inventory.inventoryString[k] == "iron" && inventory.inventoryCount[k] >= 2)
                                {
                                    bed.SetActive(true);
                                    GameObject hold = (GameObject)Instantiate(bed, new Vector3(mapTilesArray[i, j].position.x + .5f, mapTilesArray[i, j].position.y, 0), Quaternion.Euler(0, 0, 90));
                                    beds.Add(hold.GetComponent<BedScript>());
                                    //mapTilesArray[(int)mapTilesArray[i, j].position.x, (int)mapTilesArray[i, j].position.y].walkable = false;
                                    mapTilesArray[(int)mapTilesArray[i, j].position.x + 1, (int)mapTilesArray[i, j].position.y].walkable = false;

                                    tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y].SetActive(false);
                                    tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y + 1].SetActive(false);

                                    inventory.TakeItems("iron", 2); ;
                                    inventory.UpdateText(k);
                                }
                            }
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                bedGhost.SetActive(false);
            }
        }
        else if (smokeGhost.activeSelf)
        {


            for (int j = 0; j < mapTilesArray.GetLength(0); j++)
            {
                for (int i = 0; i < mapTilesArray.GetLength(1); i++)
                {
                    if (j != 0 && i != mapTilesArray.GetLength(1) - 1 && (gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).x <= mapTilesArray[i, j].position.x + 0.5f && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).x >= mapTilesArray[i, j].position.x - 0.5f
                                      && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).y <= mapTilesArray[i, j].position.y + 0.5f && gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1)).y >= mapTilesArray[i, j].position.y - 0.5f) && tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y].activeSelf && tiles[(int)mapTilesArray[j, i].position.x - 1, (int)mapTilesArray[j, i].position.y].activeSelf && tiles[(int)mapTilesArray[j, i].position.x - 1, (int)mapTilesArray[j, i].position.y + 1].activeSelf && tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y + 1].activeSelf)
                    {

                        smokeGhost.transform.position = new Vector3(mapTilesArray[i, j].position.x + .5f, mapTilesArray[i, j].position.y - .5f, -0.03f);
                        if (Input.GetMouseButton(0) && tiles[(int)mapTilesArray[i, j].position.x, (int)mapTilesArray[i, j].position.y].activeSelf)
                        {
                            for (int k = 0; k < inventory.inventoryCount.Length; k++)
                            {
                                if (inventory.inventoryString[k] == "iron" && inventory.inventoryCount[k] >= 5)
                                {
                                    smokeProducer.SetActive(true);
                                    Instantiate(smokeProducer, new Vector3(mapTilesArray[i, j].position.x + .5f, mapTilesArray[i, j].position.y - .5f, 0), Quaternion.identity);
                                    mapTilesArray[(int)mapTilesArray[i, j].position.x, (int)mapTilesArray[i, j].position.y].walkable = false;
                                    mapTilesArray[(int)mapTilesArray[i, j].position.x + 1, (int)mapTilesArray[i, j].position.y].walkable = false;
                                    mapTilesArray[(int)mapTilesArray[i, j].position.x + 1, (int)mapTilesArray[i, j].position.y - 1].walkable = false;
                                    mapTilesArray[(int)mapTilesArray[i, j].position.x, (int)mapTilesArray[i, j].position.y - 1].walkable = false;

                                    tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y].SetActive(false);
                                    tiles[(int)mapTilesArray[j, i].position.x - 1, (int)mapTilesArray[j, i].position.y].SetActive(false);
                                    tiles[(int)mapTilesArray[j, i].position.x - 1, (int)mapTilesArray[j, i].position.y + 1].SetActive(false);
                                    tiles[(int)mapTilesArray[j, i].position.x, (int)mapTilesArray[j, i].position.y + 1].SetActive(false);
                                    inventory.TakeItems("iron", 5);
                                    inventory.UpdateText(k);
                                }
                            }
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                smokeGhost.SetActive(false);
            }
        }
        bool allSleep = false;
        for (int i = 0; i < crewList.Count; i++)
        {
            if (crewList[i].actions.Count != 0)
            {
                if (crewList[i].actions[0] != "sleep")
                {
                    allSleep = false;
                    break;
                }
                else if (crewList[i].actions[0] == "sleep")
                {
                    allSleep = true;
                }
            }
        }
        int prevDayNo = 0;
        if (allSleep)
        {
            if (hours != 6)
            {
                prevDayNo = dayNo;
                timestep = timestep * 8;
            }
            else if (dayNo != prevDayNo)
            {
                for (int i = 0; i < crewList.Count; i++)
                {
                    if (crewList[i].actions.Count != 0)
                    {
                        crewList[i].sleep = false;
                        crewList[i].RemoveAction();

                    }

                }
            }
        }
        if (player != null)
        {
            if (Input.GetMouseButtonDown(0) && !bedGhost.activeSelf)
            {
                for (int i = 0; i < beds.Count; i++)
                {
                    if (mousePos.x < beds[i].transform.position.x + 1 && mousePos.x > beds[i].transform.position.x - 1 && mousePos.y < beds[i].transform.position.y + .5f && mousePos.y > beds[i].transform.position.y - .5f && player.transform.position.x != beds[i].transform.position.x - .5f)
                    {
                        player.sleep = true;
                    }
                }
            }
            if (mineWater.clicked || mineIron.clicked || player.sleep == true)
            {
                setDestination();
                player.SetDestinationPlayer(destination);
                player.AddAction("moving");
            }
            if (player.mining)
            {
                crewPanelText.DisplayCancelButton();
            }
            else
            {
                crewPanelText.HideCancelButton();
            }
            if (player.sleep == true)
            {
                player.AddAction("sleep");
                player.sleep = false;
            }
            if (mining && mineWater.clicked)
            {

                mapGenerator.setMining(mining);
                for (int j = 0; j < tiles.GetLength(0); j++)
                {
                    for (int i = 0; i < tiles.GetLength(1); i++)
                    {

                        if (destination == new Vector2(tiles[i, j].transform.position.x, tiles[i, j].transform.position.y))
                        {
                            player.AddAction("water");
                            player.SetTileReference(i, j);
                            mineWater.SetClicked(false);
                            break;

                        }

                    }
                }

            }

            if (mining && mineIron.clicked)
            {
                mapGenerator.setMining(mining);
                for (int j = 0; j < tiles.GetLength(0); j++)
                {
                    for (int i = 0; i < tiles.GetLength(1); i++)
                    {
                        if (destination == new Vector2(tiles[i, j].transform.position.x, tiles[i, j].transform.position.y))
                        {
                            player.AddAction("iron");
                            player.SetTileReference(i, j);
                            mineIron.SetClicked(false);
                            break;
                        }
                    }
                }

            }

            crewUI.UpdatePanel();
            crewPanelText.UpdateText(player);

        }
    }

    CrewScript getCrew()
    {



        for (int i = 0; i < crewList.Count; i++)
        {
            if (clickedPos.x >= crewList[i].transform.position.x - 0.5f && clickedPos.x <= crewList[i].transform.position.x + 0.5f &&
                        clickedPos.y >= crewList[i].transform.position.y - 0.5f && clickedPos.y <= crewList[i].transform.position.y + 0.5f)
            {
                crew = crewList[i].GetComponent<CrewScript>();
            }
        }
        return crew;
    }

    public void populateCrew()
    {
        if (crewList.Count == 0)
        {
            crew = crew1.GetComponent<CrewScript>();
            crewList.Add(crew1);
            crewList.Add(crew2);
            crewList.Add(crew3);

        }
    }

    public List<CrewScript> GetCrew()
    {
        return crewList;
    }

    void tilePositionsCheck()
    {
        for (int j = 0; j < tiles.GetLength(0); j++)
        {
            for (int i = 0; i < tiles.GetLength(1); i++)
            {

                if (clickedPos.x < tiles[i, j].transform.position.x + 0.5f && clickedPos.x > tiles[i, j].transform.position.x - 0.5f && clickedPos.y > tiles[i, j].transform.position.y - 0.5f && clickedPos.y < tiles[i, j].transform.position.y + 0.5f)
                {

                    if (!wallGhost.activeSelf && !bedGhost.activeSelf && new Vector2(player.transform.position.x, player.transform.position.y) != new Vector2(tiles[i, j].transform.position.x, tiles[i, j].transform.position.y) && tiles[i, j].activeSelf)
                    {
                        if (!(Input.mousePosition.x > crewPanelText.transform.position.x - 50 && Input.mousePosition.x < crewPanelText.transform.position.x + 50 && Input.mousePosition.y > crewPanelText.transform.position.y - 60f && Input.mousePosition.y < crewPanelText.transform.position.y + 60f))
                        {
                            commandPanel.transform.position = new Vector3(Input.mousePosition.x + 30, Input.mousePosition.y + 20, gameCam.transform.position.z * -1);
                            commandPanel.SetActive(true);
                        }
                    }
                }


            }
        }
    }

    void updateClickedPos()
    {
        clickedPos = gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCam.transform.position.z * -1));
    }

    public void Mine(Vector2 tileRef, string toMine, CrewScript miningCrew)
    {
        if (toMine == "water")
        {
            if (mapTilesArray[(int)tileRef.x, (int)tileRef.y].water != 0)
            {
                mapTilesArray[(int)tileRef.x, (int)tileRef.y].water -= 1;
                inventory.StoreInInventory(1, "water");
                mapGenerator.setMining(mining);
                waterUIValue.textChange(mapTilesArray[(int)tileRef.x, (int)tileRef.y].water);   //only for debugging so that I can see what's happening in the tile at runtime
            }
            else
            {
                miningCrew.RemoveAction();
                mining = false;
                mineWater.SetClicked(false);
                mapGenerator.setMining(mining);
            }
        }

        else if (toMine == "iron")
        {
            if (mapTilesArray[(int)tileRef.x, (int)tileRef.y].iron != 0)
            {
                mapTilesArray[(int)tileRef.x, (int)tileRef.y].iron -= 1;
                inventory.StoreInInventory(1, "iron");
                mapGenerator.setMining(mining);
                ironUIValue.textChange(mapTilesArray[(int)tileRef.x, (int)tileRef.y].iron);
            }
            else
            {
                miningCrew.RemoveAction();
                mining = false;
                mineIron.SetClicked(false);
                mapGenerator.setMining(mining);
            }
        }



    }


    void Clock()
    {
        if (timestep > 1)
        {
            timestep = 0;
            minutes++;
            if (minutes > 59)
            {
                hours++;
                minutes = 0;
                if (hours > 25)
                {
                    dayNo++;
                    if (globalTerraformingProgression >= 100)
                    {
                        for (int i = 0; i < mapTilesArray.GetLength(0); i++)
                        {
                            for (int j = 0; j < mapTilesArray.GetLength(1); j++)
                            {
                                getMine.setWater(i, j);
                            }
                        }

                        //   waterList = getMine.GetWaterList();

                    }
                    hours = 0;
                }
            }
        }
        if (hours < 10 && minutes < 10)
        {
            timeText.text = "0" + hours.ToString() + "::" + "0" + minutes.ToString();
        }
        else if (hours < 10)
        {
            timeText.text = "0" + hours.ToString() + "::" + minutes.ToString();
        }
        else
        {
            timeText.text = hours.ToString() + "::" + minutes.ToString();

        }


    }

    void setDestination()
    {
        for (int j = 0; j < tiles.GetLength(0); j++)
        {
            for (int i = 0; i < tiles.GetLength(1); i++)
            {
                if (clickedPos.x < tiles[i, j].transform.position.x + 0.5f && clickedPos.x > tiles[i, j].transform.position.x - 0.5f && clickedPos.y > tiles[i, j].transform.position.y - 0.5f && clickedPos.y < tiles[i, j].transform.position.y + 0.5f)
                {
                    destination = tiles[i, j].transform.position;

                }
            }
        }

        if (new Vector2(player.transform.position.x, player.transform.position.y) != destination)
        {

            mining = true;
        }
    }

}