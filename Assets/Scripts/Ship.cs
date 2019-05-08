using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ship : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static GameObject shipBeingDragged;
    Vector3 startPos;
    public List<GameObject> cells = new List<GameObject>();
    public GameObject fleet;

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
        transform.position = startPos;
    }
}
