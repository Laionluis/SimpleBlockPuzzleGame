using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class BlockCell : Base, IDropHandler
{
    public GameObject controller;
    private CreateGameObject createGameObject;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Tilemap tilemap = this.GetComponentInChildren<Tilemap>();
            if (eventData.pointerDrag.tag != "Untagged")
            {
                for (int i = 0; i < eventData.pointerDrag.transform.childCount; i++) //loop para identificar se local que foi dropado ja esta em uso
                {
                    Vector3Int cellPosition = tilemap.WorldToCell(eventData.pointerDrag.transform.GetChild(i).transform.position);
                    if (cellPosition.x >= 0 && cellPosition.x <= 7 && cellPosition.y >= 0 && cellPosition.y <= 7)
                    {
                        if (controller.GetComponent<CreateGameObject>().positions[cellPosition.x, cellPosition.y] != null)
                        {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = posicaoInicialBloco;
                            eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(0.5f, 0.5f, 1);
                            return;
                        }
                    }
                    else
                    {
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = posicaoInicialBloco;
                        eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(0.5f, 0.5f, 1);
                        return;
                    }
                }
                
                for (int i = 0; i < eventData.pointerDrag.transform.childCount; i++)
                {
                    Vector3Int cellPosition = tilemap.WorldToCell(eventData.pointerDrag.transform.GetChild(i).transform.position);
                    
                    if (cellPosition.x >= 0 && cellPosition.x <= 7 && cellPosition.y >= 0 && cellPosition.y <= 7)
                    {
                        if (createGameObject != null && controller.GetComponent<CreateGameObject>().positions[cellPosition.x, cellPosition.y] == null)
                        {
                            GameObject objetoQueFicaNoTabuleiro = createGameObject.CreateBlockPiecesAux();
                            objetoQueFicaNoTabuleiro.GetComponent<RectTransform>().anchoredPosition = tilemap.CellToWorld(cellPosition) + new Vector3(.57f, .57f, 1) * .5f;                            
                            controller.GetComponent<CreateGameObject>().positions[cellPosition.x, cellPosition.y] = objetoQueFicaNoTabuleiro;                            
                        }
                        else
                        {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = posicaoInicialBloco;
                            eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(0.5f, 0.5f, 1);
                            return;
                        }
                    }
                    else
                    {
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = posicaoInicialBloco;
                        eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(0.5f, 0.5f, 1);
                        return;
                    }
                }
                createGameObject.CreateBlockPieces();
            }
            else
            {
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
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = posicaoInicialBloco;
                        eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(0.5f, 0.5f, 1);
                    }
                }
                else
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = posicaoInicialBloco;
                    eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(0.5f, 0.5f, 1);
                }
            }           
        }
    }

    

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        createGameObject = Tree.FindObjectOfType<CreateGameObject>();
    }

}
