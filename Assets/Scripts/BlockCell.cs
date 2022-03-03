using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BlockCell : Base, IDropHandler
{
    public GameObject controller;
    public Text scoreText;
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
                            Pontuar10();
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
                        Pontuar10();
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

    private Coroutine CountingCoroutine;
    public int CountFPS = 30;
    public float Duration = 1f;
    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);

        int previousValue = int.Parse(scoreText.text);
        int stepAmount;        
        
        stepAmount = Mathf.CeilToInt((newValue - previousValue) / (CountFPS * Duration)); // newValue = 20, previousValue = 0. CountFPS = 30, and Duration = 1; (20 - 0) / (30*1) // 0.66667 (floortoint)-> 0
        
      
        while (previousValue < newValue)
        {
            previousValue += stepAmount;
            if (previousValue > newValue)
            {
                previousValue = newValue;
            }

            scoreText.text = previousValue.ToString();

            yield return Wait;
        }
       
    }

    private void Pontuar10()
    {
       
        int antes = int.Parse(scoreText.text);
        CountingCoroutine = StartCoroutine(CountText(antes + 10));

    }

    private void VerificaSePrecisaNovasPecas()
    {
        if(controller.GetComponent<CreateGameObject>().vetorPosicaoInicial.Count == 0)
            createGameObject.CreateBlockPieces(null);
    }

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        createGameObject = Tree.FindObjectOfType<CreateGameObject>();
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
    }

}
