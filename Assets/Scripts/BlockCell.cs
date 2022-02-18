using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class BlockCell : MonoBehaviour, IDropHandler
{
    public GameObject controller;
    private CreateGameObject createGameObject;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {            
            Tilemap tilemap = this.GetComponentInChildren<Tilemap>();
            Vector3Int cellPosition = tilemap.WorldToCell(eventData.pointerDrag.transform.position);

            if (cellPosition.x >= 0 && cellPosition.x <= 7 && cellPosition.y >= 0 && cellPosition.y <= 7)
            {
                if (createGameObject != null && controller.GetComponent<CreateGameObject>().positions[cellPosition.x, cellPosition.y] == null)
                {
                    createGameObject.CreateBlockPieces();
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = tilemap.CellToWorld(cellPosition) + new Vector3(.57f, .57f, 1) * .5f;
                    controller.GetComponent<CreateGameObject>().positions[cellPosition.x, cellPosition.y] = eventData.pointerDrag;
                }
                else
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(4, -1.2f, 1);
                }
            }
            else
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(4, -1.2f, 1);
            }

            
        }
    }

    

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        createGameObject = Tree.FindObjectOfType<CreateGameObject>();
    }

}
