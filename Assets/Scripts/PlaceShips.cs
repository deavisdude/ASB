using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceShips : MonoBehaviour
{
    Image image;
    public GameObject board;
    GameObject AIBoard;
    List<GameObject> AIcells = new List<GameObject>();
    List<GameObject> cells = new List<GameObject>();
    public List<GameObject> adjacentCells = new List<GameObject>();
    public int direction;
    //public GameObject carrier, battleship, cruiser, submarine, destroyer;
    //GameObject[] ships;
    public int placing, AIPlacing, size, AISize;

    public List<GameObject> toggleUs = new List<GameObject>();

    void Start()
    {
        //ships = new GameObject[]{ carrier, battleship, cruiser, submarine, destroyer};
        size = 5;
        AISize = 5;
        placing = 1;
        AIPlacing = 1;
        image = GetComponent<Image>();
        for(int i=0; i<board.transform.childCount; i++)
        {
            cells.Add(board.transform.GetChild(i).gameObject);
        }
    }

    void Update()
    { 
        if(placing == 0)
        {
            AIBoard = Instantiate(board, board.transform.parent);
            board.transform.Translate(-120f, 0f, 0f);
            AIBoard.transform.Translate(120f, 0f, 0f);

            List<GameObject> AICells = new List<GameObject>();
            for (int i = 0; i < AIBoard.transform.childCount; i++)
            {
                AIBoard.transform.GetChild(i).gameObject.GetComponent<Cell>().activated = false;
                AIBoard.transform.GetChild(i).gameObject.GetComponent<Cell>().SetColor(0);
            }

            placing = -1;

            AIPlace();
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

        switch (AIPlacing)
        {
            case 1:
                AISize = 5;
                break;
            case 2:
                AISize = 4;
                break;
            case 3:
                AISize = 3;
                break;
            case 4:
                AISize = 3;
                break;
            case 5:
                AISize = 2;
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

    void AIPlace()
    {
        AIPlacing = 1;
        for (int i = 1; i <= 5; i++)
        {
            int x = Random.Range(1, 10);
            int y = Random.Range(1, 10);

            ClearCells();
            AITryRight(x, y);
            AIClick();

            AIPlacing++;
            Update();
        }

        AIPlacing = 0;

        foreach(GameObject cell in AIcells)
        {
            if (!cell.GetComponent<Cell>().activated)
            {
                cell.GetComponent<Cell>().SetColor(0);
            }
        }

        foreach(GameObject go in toggleUs)
        {
            go.active = !go.active;
        }

    }

    public bool AITryRight(int x, int y)
    {
        int count = 0, tx = x, ty = y;
        while (count < AISize)
        {
            GameObject go = AIFindCellXY(tx, ty);
            if (go != null && !go.GetComponent<Cell>().activated)
            {
                adjacentCells.Add(go);
                go.GetComponent<Cell>().Highlight();
                tx++;
            }
            else
            {
                ClearCells();
                AITryDown(x, y);
                return false;
            }
            count++;
        }
        direction = 0;
        return true;
    }

    public bool AITryDown(int x, int y)
    {
        int count = 0, tx = x, ty = y;
        while (count < AISize)
        {
            GameObject go = AIFindCellXY(tx, ty);
            if (go != null && !go.GetComponent<Cell>().activated)
            {
                adjacentCells.Add(go);
                go.GetComponent<Cell>().Highlight();
                ty--;
            }
            else
            {
                ClearCells();
                AITryLeft(x, y);
                return false;
            }
            count++;
        }
        direction = 1;
        return true;
    }

    public bool AITryLeft(int x, int y)
    {
        int count = 0, tx = x, ty = y;
        while (count < AISize)
        {
            GameObject go = AIFindCellXY(tx, ty);
            if (go != null && !go.GetComponent<Cell>().activated)
            {
                adjacentCells.Add(go);
                go.GetComponent<Cell>().Highlight();
                tx--;
            }
            else
            {
                ClearCells();
                AITryUp(x, y);
                return false;
            }
            count++;
        }
        direction = 2;
        return true;
    }

    public bool AITryUp(int x, int y)
    {
        int count = 0, tx = x, ty = y;
        while (count < AISize)
        {
            GameObject go = AIFindCellXY(tx, ty);
            if (go != null && !go.GetComponent<Cell>().activated)
            {
                adjacentCells.Add(go);
                go.GetComponent<Cell>().Highlight();
                ty++;
            }
            else
            {
                ClearCells();
                return false;
            }
            count++;
        }
        direction = 3;
        return true;
    }



    public GameObject AIFindCellXY(int x, int y)
    {
        for (int i = 0; i < AIBoard.transform.childCount; i++)
        {
            AIcells.Add(AIBoard.transform.GetChild(i).gameObject);
        }

        foreach (GameObject cell in AIcells)
        {
            if (cell.GetComponent<Cell>().x == x && cell.GetComponent<Cell>().y == y)
                return cell;
        }
        return null;
    }

    public void AIClick()
    {
        switch (AIPlacing)
        {
            case 1:
                foreach (GameObject cell in adjacentCells)
                {
                    cell.GetComponent<Cell>().activated = true;
                }
                AIPlacing = 2;
                break;
            case 2:
                foreach (GameObject cell in adjacentCells)
                {
                    cell.GetComponent<Cell>().activated = true;
                }
                AIPlacing = 3;
                break;
            case 3:
                foreach (GameObject cell in adjacentCells)
                {
                    cell.GetComponent<Cell>().activated = true;
                }
                AIPlacing = 4;
                break;
            case 4:
                foreach (GameObject cell in adjacentCells)
                {
                    cell.GetComponent<Cell>().activated = true;
                }
                AIPlacing = 5;
                break;
            case 5:
                foreach (GameObject cell in adjacentCells)
                {
                    cell.GetComponent<Cell>().activated = true;
                }
                AIPlacing = 0;
                break;
        }
    }



}
