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
    public Score scoreText;
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
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Where(x => x.Value == eventData.pointerDrag).Select(y => y.Key).FirstOrDefault(); ;
                            eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(0.5f, 0.5f, 1);
                            return;
                        }
                    }
                    else
                    {
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Where(x => x.Value == eventData.pointerDrag).Select(y => y.Key).FirstOrDefault(); ;
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
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Where(x => x.Value == eventData.pointerDrag).Select(y => y.Key).FirstOrDefault(); ;
                            eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(0.5f, 0.5f, 1);
                            return;
                        }
                    }
                    else
                    {
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Where(x => x.Value == eventData.pointerDrag).Select(y => y.Key).FirstOrDefault(); ;
                        eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(0.5f, 0.5f, 1);
                        return;
                    }
                }
                scoreText.Pontuar(eventData.pointerDrag.transform.childCount, eventData.pointerDrag.transform.GetChild(0).transform.position);
                var key = controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Where(x => x.Value.name == eventData.pointerDrag.name).Select(y => y.Key).FirstOrDefault();
                controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Remove(key);
                VerificaSePrecisaNovasPecas();
            }
            else
            {
                Vector3Int cellPosition = tilemap.WorldToCell(eventData.pointerDrag.transform.position);

                if (cellPosition.x >= 0 && cellPosition.x <= 7 && cellPosition.y >= 0 && cellPosition.y <= 7)
                {
                    if (createGameObject != null && controller.GetComponent<CreateGameObject>().positions[cellPosition.x, cellPosition.y] == null)
                    {
                        var key = controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Where(x => x.Value == eventData.pointerDrag).Select(y => y.Key).FirstOrDefault();
                        controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Remove(key);
                        VerificaSePrecisaNovasPecas();
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = tilemap.CellToWorld(cellPosition) + new Vector3(.57f, .57f, 1) * .5f;
                        controller.GetComponent<CreateGameObject>().positions[cellPosition.x, cellPosition.y] = eventData.pointerDrag;
                        scoreText.Pontuar(1, eventData.pointerDrag.transform.position);
                    }
                    else
                    {
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Where(x => x.Value == eventData.pointerDrag).Select(y => y.Key).FirstOrDefault(); ;
                        eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(1.5f, 1.5f, 1);
                    }
                }
                else
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Where(x => x.Value == eventData.pointerDrag).Select(y => y.Key).FirstOrDefault(); ;
                    eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(1.5f, 1.5f, 1);
                }
            }
        }
    }

    private void VerificaSePrecisaNovasPecas()
    {
        if (controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Count == 0)
            createGameObject.CreateBlockPieces(null);        
    }

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        createGameObject = Tree.FindObjectOfType<CreateGameObject>();
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Score>();
    }

    void Update()
    {

    }
}
