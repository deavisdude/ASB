using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceShips : MonoBehaviour
{
    Image image;
    public GameObject board;
    List<GameObject> cells = new List<GameObject>();
    public List<GameObject> adjacentCells = new List<GameObject>();
    public int direction;
    //public GameObject carrier, battleship, cruiser, submarine, destroyer;
    //GameObject[] ships;
    public int placing, size;

    void Start()
    {
        //ships = new GameObject[]{ carrier, battleship, cruiser, submarine, destroyer};
        size = 5;
        placing = 1;
        image = GetComponent<Image>();
        for(int i=0; i<board.transform.GetChildCount(); i++)
        {
            cells.Add(board.transform.GetChild(i).gameObject);
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

    public void ClearCells()
    {
        foreach (GameObject cell in adjacentCells)
        {
            if (cell != null)
                cell.GetComponent<Cell>().Highlight();
        }
        adjacentCells = new List<GameObject>();
    }

    public bool TryRight(int x, int y)
    {
        int count = 0, tx = x, ty = y;
        while (count < size)
        {
            GameObject go = FindCellXY(tx, ty);
            if (go != null && !go.GetComponent<Cell>().activated)
            {
                adjacentCells.Add(go);
                go.GetComponent<Cell>().Highlight();
                tx++;
            }
            else
            {
                ClearCells();
                TryDown(x,y);
                return false;
            }
            count++;
        }
        direction = 0;
        return true;
    }

    public bool TryDown(int x, int y)
    {
        int count = 0, tx = x, ty = y;
        while (count < size)
        {
            GameObject go = FindCellXY(tx, ty);
            if (go != null && !go.GetComponent<Cell>().activated)
            {
                adjacentCells.Add(go);
                go.GetComponent<Cell>().Highlight();
                ty--;
            }
            else
            {
                ClearCells();
                TryLeft(x,y);
                return false;
            }
            count++;
        }
        direction = 1;
        return true;
    }

    public bool TryLeft(int x, int y)
    {
        int count = 0, tx = x, ty = y;
        while (count < size)
        {
            GameObject go = FindCellXY(tx, ty);
            if (go != null && !go.GetComponent<Cell>().activated)
            {
                adjacentCells.Add(go);
                go.GetComponent<Cell>().Highlight();
                tx--;
            }
            else
            {
                ClearCells();
                TryUp(x,y);
                return false;
            }
            count++;
        }
        direction = 2;
        return true;
    }

    public bool TryUp(int x, int y)
    {
        int count = 0, tx = x, ty = y;
        while (count < size)
        {
            GameObject go = FindCellXY(tx, ty);
            if (go != null && !go.GetComponent<Cell>().activated)
            {
                adjacentCells.Add(go);
                go.GetComponent<Cell>().Highlight();
                ty++;
            }
            else
            {
                ClearCells();
                TryRight(x,y);
                return false;
            }
            count++;
        }
        direction = 3;
        return true;
    }



    public GameObject FindCellXY(int x, int y)
    {
        foreach(GameObject cell in cells)
        {
            if (cell.GetComponent<Cell>().x == x && cell.GetComponent<Cell>().y == y)
                return cell;
        }
        return null;
    }

    public void LClick()
    {
        switch (placing)
        {
            case 1:
                foreach (GameObject cell in adjacentCells)
                {
                    cell.GetComponent<Cell>().activated = true;
                }
                placing = 2;
                break;
            case 2:
                foreach (GameObject cell in adjacentCells)
                {
                    cell.GetComponent<Cell>().activated = true;
                }
                placing = 3;
                break;
            case 3:
                foreach (GameObject cell in adjacentCells)
                {
                    cell.GetComponent<Cell>().activated = true;
                }
                placing = 4;
                break;
            case 4:
                foreach (GameObject cell in adjacentCells)
                {
                    cell.GetComponent<Cell>().activated = true;
                }
                placing = 5;
                break;
            case 5:
                foreach (GameObject cell in adjacentCells)
                {
                    cell.GetComponent<Cell>().activated = true;
                }
                placing = 0;
                break;
        }
    }

}
