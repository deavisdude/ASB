using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ship : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static GameObject shipBeingDragged;
    int size = 4;
    Vector3 startPos;
    public GameObject fleet;
    List<GameObject> cellsOccupying = new List<GameObject>();
    public bool placed = false;

    void Update()
    {
        if (gameObject.Equals(Ship.shipBeingDragged))
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
                transform.Rotate(0f, 0f, 90f);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.Rotate(0f, 0f, -90f);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    { 
        Ship.shipBeingDragged = gameObject;
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ship.shipBeingDragged = null;
        foreach(GameObject cell in Cell.cells)
        {
            if (!cell.GetComponent<Cell>().ship && cell.GetComponent<BoxCollider2D>().IsTouching(GetComponent<PolygonCollider2D>())){
                cellsOccupying.Add(cell);
                cell.GetComponent<Cell>().ship = gameObject;
                cell.GetComponent<Cell>().SetColor(2);
            }
        }

        if (cellsOccupying.Count < size)
        {
            transform.position = startPos;
            foreach(GameObject cell in cellsOccupying)
            {
                cell.GetComponent<Cell>().ship = null;
                cell.GetComponent<Cell>().SetColor(0);
            }
            cellsOccupying.Clear();
        }
        else
        {
            placed = true;
        }

    }

}
