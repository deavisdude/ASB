using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceShips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite yellowTile, tile, greenTile;
    int x, y;
    Image image;
    public GameObject board;
    List<GameObject> cells = new List<GameObject>();
    List<GameObject> adjacentCells = new List<GameObject>();
    int direction;
    //public GameObject carrier, battleship, cruiser, submarine, destroyer;
    //GameObject[] ships;
    static int placing, size;
    bool activated = false;

    void Start()
    {
        //ships = new GameObject[]{ carrier, battleship, cruiser, submarine, destroyer};
        size = 5;
        placing = 1;
        image = GetComponent<Image>();
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

    void Update()
    { 
        if(placing == 0)
        {
            GameObject AIBoard = Instantiate(board, board.transform.parent);
            board.transform.Translate(-120f, 0f, 0f);
            AIBoard.transform.Translate(120f, 0f, 0f);
            placing = -1;
        }

        foreach (GameObject cell in cells)
        {
            if (activated)
            {
                image.sprite = greenTile;
            }
        }
        switch (placing)
        {
            case 1:
                size = 5;
                break;
            case 2:
                size = 4;
                break;
            case 3:
                size = 3;
                break;
            case 4:
                size = 3;
                break;
            case 5:
                size = 2;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)//If we right click, let's try rotating again
        {
            OnPointerExit(null);
            if (direction == 3)
            {
                TryRight();
            }
            else
            {
                int i = direction + 1;
                switch (i)
                {
                    case 1:
                        TryDown();
                        break;
                    case 2:
                        TryLeft();
                        break;
                    case 3:
                        TryUp();
                        break;
                }
            }
        }else if(eventData.button == PointerEventData.InputButton.Left)
        {
            switch (placing)
            {
                case 1:
                    foreach(GameObject cell in adjacentCells)
                    {
                        cell.GetComponent<PlaceShips>().activated = true;
                    }
                    placing = 2;
                    break;
                case 2:
                    foreach (GameObject cell in adjacentCells)
                    {
                        cell.GetComponent<PlaceShips>().activated = true;
                    }
                    placing = 3;
                    break;
                case 3:
                    foreach (GameObject cell in adjacentCells)
                    {
                        cell.GetComponent<PlaceShips>().activated = true;
                    }
                    placing = 4;
                    break;
                case 4:
                    foreach (GameObject cell in adjacentCells)
                    {
                        cell.GetComponent<PlaceShips>().activated = true;
                    }
                    placing = 5;
                    break;
                case 5:
                    foreach (GameObject cell in adjacentCells)
                    {
                        cell.GetComponent<PlaceShips>().activated = true;
                    }
                    placing = 0;
                    break;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(placing != 0)
            TryRight();
    }


    bool TryRight()
    {
        int count = 0, tx = x, ty = y;
        while (count < size)
        {
            GameObject go = FindCellXY(tx, ty);
            if (go != null)
            {
                adjacentCells.Add(go);
                go.GetComponent<PlaceShips>().Highlight();
                tx++;
            }
            else
            {
                OnPointerExit(null);
                TryDown();
                return false;
            }
            count++;
        }
        direction = 0;
        return true;
    }

    bool TryDown()
    {
        int count = 0, tx = x, ty = y;
        while (count < size)
        {
            GameObject go = FindCellXY(tx, ty);
            if (go != null)
            {
                adjacentCells.Add(go);
                go.GetComponent<PlaceShips>().Highlight();
                ty--;
            }
            else
            {
                OnPointerExit(null);
                TryLeft();
                return false;
            }
            count++;
        }
        direction = 1;
        return true;
    }

    bool TryLeft()
    {
        int count = 0, tx = x, ty = y;
        while (count < size)
        {
            GameObject go = FindCellXY(tx, ty);
            if (go != null)
            {
                adjacentCells.Add(go);
                go.GetComponent<PlaceShips>().Highlight();
                tx--;
            }
            else
            {
                OnPointerExit(null);
                TryUp();
                return false;
            }
            count++;
        }
        direction = 2;
        return true;
    }

    bool TryUp()
    {
        int count = 0, tx = x, ty = y;
        while (count < size)
        {
            GameObject go = FindCellXY(tx, ty);
            if (go != null)
            {
                adjacentCells.Add(go);
                go.GetComponent<PlaceShips>().Highlight();
                ty++;
            }
            else
            {
                OnPointerExit(null);
                TryRight();
                return false;
            }
            count++;
        }
        direction = 3;
        return true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach(GameObject cell in adjacentCells)
        {
            if(cell != null)
                cell.GetComponent<PlaceShips>().Highlight();
        }
        adjacentCells = new List<GameObject>();
    }

    public GameObject FindCellXY(int x, int y)
    {
        foreach(GameObject cell in cells)
        {
            if (cell.GetComponent<PlaceShips>().x == x && cell.GetComponent<PlaceShips>().y == y)
                return cell;
        }
        return null;
    }

    public void Highlight()
    {
            if (image.sprite == tile)
                image.sprite = yellowTile;
            else
            {
                image.sprite = tile;
            }
        }

    }

}
