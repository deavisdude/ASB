using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour { 

    public Sprite yellowTile, tile, greenTile;
    public int x, y;
    public static List<GameObject> cells = new List<GameObject>();
    public static List<GameObject> AICells = new List<GameObject>();
    public GameObject board;
    public GameObject AIBoard;
    public GameObject ship;



    public void Start()
    {
        if (cells.Count < 1)
        {
            for (int i = 0; i < board.transform.childCount; i++)
            {
                cells.Add(board.transform.GetChild(i).gameObject);
            }
        }
        if (AICells.Count < 1)
        {
            for (int i = 0; i < AIBoard.transform.childCount; i++)
            {
                AICells.Add(AIBoard.transform.GetChild(i).gameObject);
            }
        }
        int index;
        if (cells.Contains(gameObject))
            index = cells.IndexOf(gameObject);
        else
            index = AICells.IndexOf(gameObject);
        if (index < 10)
        {
            x = index + 1;
            y = 10;
        }
        else
        {
            y = 10 - (Mathf.FloorToInt(index / 15));
            x = (index % 15) + 1;
        }
    }


    public void SetColor(int i)
    {
        switch (i)
        {
            case 0:
                GetComponent<Image>().sprite = tile;
                break;
            case 1:
                GetComponent<Image>().sprite = yellowTile;
                break;
            case 2:
                GetComponent<Image>().sprite = greenTile;
                break;
        }

    }
}

