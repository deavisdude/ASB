using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour { 

    public Sprite yellowTile, tile, greenTile;
    public int x, y;
    List<GameObject> cells = new List<GameObject>();
    public GameObject board;

    private void Start()
    {

        for (int i = 0; i < board.transform.childCount; i++)
        {
            cells.Add(board.transform.GetChild(i).gameObject);
        }

        int index = cells.IndexOf(gameObject);
        if (index < 15)
        {
            x = index + 1;
            y = 15;
        }
        else
        {
            y = 15 - (Mathf.FloorToInt(index / 15));
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

