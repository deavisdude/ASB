using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start1PGame : MonoBehaviour
{
    public GameObject[] objectsToToggle;
    public GameObject[] playerAssets;
    public GameObject[] AIAssets;

    public GameObject[] Fleet;
    public GameObject AIBoard;
    public GameObject[] AIFleet;
    public GameObject smallSafadi;

    int[,] ship1 = {{1,1,1,1},
                    {0,0,0,0}};
    int[,] ship2 = {{1,1,1,0},
                    {0,0,1,0}};
    int[,] ship3 = {{1,1,1,0},
                    {1,0,0,0}};
    int[,] ship4 = {{1,1,0,0},
                    {1,1,0,0}};
    int[,] ship5 = {{0,1,1,0},
                    {1,1,0,0}};
    int[,] ship6 = {{1,1,1,0},
                    {0,1,0,0}};
    int[,] ship7 = {{1,1,0,0},
                    {0,1,1,0}};

    List<int[,]> shipDesigns= new List<int[,]>();

    bool placed = false;
    // Start is called before the first frame update
    public void StartGame()
    {

        shipDesigns.Add(ship1);
        shipDesigns.Add(ship2);
        shipDesigns.Add(ship3);
        shipDesigns.Add(ship4);
        shipDesigns.Add(ship5);
        shipDesigns.Add(ship6);
        shipDesigns.Add(ship7);
        Camera.main.GetComponent<CameraRotator>().Activate();
        foreach (GameObject go in objectsToToggle)
        {
            go.SetActive(!go.activeSelf);
        }

    }

    void Update()
    {
        int count = 0;
        foreach (GameObject ship in Fleet)
        {
            if (ship.GetComponent<Ship>().placed)
            {
                count++;
            }
        }

        if (count == 7 && !placed) //if all ships are placed
        {
            placed = true;
            PopulateAIBoard();
            smallSafadi.active = false;
        }
    }

    void SwitchTurns()
    {
        foreach (GameObject o in playerAssets)
        {
            o.active = !o.active;
        }
        foreach (GameObject o in AIAssets)
        {
            o.active = !o.active;
        }
    }

    void PopulateAIBoard()
    {
        SwitchTurns();
        int shipNumber = 0;
        foreach (int[,] design in shipDesigns)
        {
            bool success = false;
            while (!success) { 
                int x1 = Random.Range(0, 15);
                int y1 = Random.Range(0, 10);
                int x, y;

                int counter = 0;
                for (int i = 0; i < design.Rank; i++)
                {
                    for (int j = 0; j < design.Length / design.Rank; j++)
                    {
                        if (design[i, j] == 1)
                        {
                            x = x1 + j;
                            y = y1 + i;

                            foreach (GameObject cell in Cell.AICells)
                            {
                                Cell c = cell.GetComponent<Cell>();
                                c.Start();
                                if (c.x == x && c.y == y && !c.ship)
                                {
                                    c.ship = AIFleet[shipNumber];
                                    counter++;
                                }
                            }
                        }
                    }
                }
                if (counter == 4)
                {
                    shipNumber++;
                    success = true;
                }
            }
        }

    }
}
