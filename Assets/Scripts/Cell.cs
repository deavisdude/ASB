using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool activated = false;
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
        if (index < 10)
        {
            x = index + 1;
            y = 10;
        }
        else
        {
            y = 10 - (Mathf.FloorToInt(index / 10));
            x = (index % 10) + 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        board.GetComponent<PlaceShips>().ClearCells();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (board.GetComponent<PlaceShips>().placing > 0 && board.name == "Board" && eventData.button == PointerEventData.InputButton.Right)//If we right click, let's try rotating again
        {
            OnPointerExit(null);
            if (board.GetComponent<PlaceShips>().direction == 3)
            {
                board.GetComponent<PlaceShips>().TryRight(x,y);
            }
            else
            {
                int i = board.GetComponent<PlaceShips>().direction + 1;
                switch (i)
                {
                    case 1:
                        board.GetComponent<PlaceShips>().TryDown(x,y);
                        break;
                    case 2:
                        board.GetComponent<PlaceShips>().TryLeft(x,y);
                        break;
                    case 3:
                        board.GetComponent<PlaceShips>().TryUp(x,y);
                        break;
                }
            }
        }
        else if (board.GetComponent<PlaceShips>().placing > 0 && board.name == "Board" && eventData.button == PointerEventData.InputButton.Left)
        {
            board.GetComponent<PlaceShips>().LClick();
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if(board.name == "Board" && board.GetComponent<PlaceShips>().placing > 0)
            board.GetComponent<PlaceShips>().TryRight(x,y);
    }

    public void Highlight()
    {
        if (GetComponent<Image>().sprite == tile)
            GetComponent<Image>().sprite = yellowTile;
        else
        {
            GetComponent<Image>().sprite = tile;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (activated) {
            GetComponent<Image>().sprite = greenTile;
        }
    }
}

