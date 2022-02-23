using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DragAndDrop : Base, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private CanvasGroup canvasGroup;
    private Grid grid;
    private GameObject contornoAtual;
    private Vector3Int dragCellPosicaoAtual;
    private CreateGameObject createGameObject;
    private Tilemap tilemap;
    public GameObject controller;
    private Dictionary<int, Vector3Int> teste = new Dictionary<int, Vector3Int>();
    private Dictionary<int, GameObject> contornos = new Dictionary<int, GameObject>();
    public void OnDrag(PointerEventData eventData) 
    {
        //Create a ray going from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        //Calculate the distance between the Camera and the GameObject, and go this distance along the ray
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position)) + new Vector3(0, 1, 0);
        //Move the GameObject when you drag it
        rayPoint.z = 50;
        transform.position = rayPoint;
        if (tilemap != null && createGameObject != null)
        {
            if (this.tag != "Untagged")
            {
                for (int i = 0; i < this.gameObject.transform.childCount; i++)
                {
                    Vector3Int cellPosition = tilemap.WorldToCell(transform.GetChild(i).transform.position);
                    Vector3 posicaoReal = tilemap.CellToWorld(cellPosition) + new Vector3(.57f, .57f, 1) * .5f;
                    
                    if (cellPosition.x >= 0 && cellPosition.x <= 7 && cellPosition.y >= 0 && cellPosition.y <= 7)
                    {
                        var aux1 = teste.Where(x => x.Key == i).FirstOrDefault();
                        if (aux1.Value != cellPosition)
                        {
                            teste.Remove(i);
                            DestroyImmediate(contornos.Where(x => x.Key == i).Select(x => x.Value).LastOrDefault());
                            contornos.Remove(i);
                            GameObject auxContorno = createGameObject.Create(posicaoReal, "");
                            contornos.Add(i,auxContorno);
                            teste.Add(i, cellPosition);
                        }
                    }
                    else
                    {
                        DestruirContornos();
                    }
                }               
            }
            else
            {
                Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
                Vector3 posicaoReal = tilemap.CellToWorld(cellPosition) + new Vector3(.57f, .57f, 1) * .5f;
                GameObject auxContorno;
                if (cellPosition.x >= 0 && cellPosition.x <= 7 && cellPosition.y >= 0 && cellPosition.y <= 7)
                {
                    if (cellPosition != dragCellPosicaoAtual)
                    {
                        DestroyImmediate(contornoAtual);
                        auxContorno = createGameObject.Create(posicaoReal, this.tag);
                        contornoAtual = auxContorno;
                    }
                }
                else
                {
                    DestroyImmediate(contornoAtual);
                }

                dragCellPosicaoAtual = cellPosition;
            }          
        }        
    }

    private void DestruirContornos()
    {
        foreach (var item in contornos.Select(x => x.Value))
        {
            DestroyImmediate(item);
        }
        contornos.Clear();
        teste.Clear();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        DestroyImmediate(contornoAtual);
        DestruirContornos();
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        if (cellPosition.x >= 0 && cellPosition.x <= 7 && cellPosition.y >= 0 && cellPosition.y <= 7)
        {
            if (eventData.pointerDrag.tag != "Untagged")
                Destroy(eventData.pointerDrag);
        }
        else
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = posicaoInicialBloco;
        }
        VerificaMatriz();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        if (cellPosition.x >= 0 && cellPosition.x <= 7 && cellPosition.y >= 0 && cellPosition.y <= 7)
        {
            
        }
        else
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = posicaoInicialBloco;
        }
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        controller = GameObject.FindGameObjectWithTag("GameController");
        tilemap = Tree.FindObjectOfType<Tilemap>();
        createGameObject = Tree.FindObjectOfType<CreateGameObject>();
    }

    private void VerificaMatriz()
    {
        GameObject[,] positions = controller.GetComponent<CreateGameObject>().positions;
        int countCol;
        int countRow;
        for (int col = 0; col < positions.GetLength(0); col++)
        {
            countCol = 0;
            countRow = 0;
            for (int row = 0; row < positions.GetLength(1); row++)
            {
                if (positions[col, row] != null)
                    countCol++;
                if (positions[row, col] != null)
                    countRow++;
            }
            if (countCol == 8)
                DestruirColuna(positions, col);
            if (countRow == 8)
                DestruirLinha(positions, col);
        }

    }

    private void DestruirLinha(GameObject[,] positions, int col)
    {
        GameObject[] gameObjectsLinha = GetRow(positions, col);
        foreach (var item in gameObjectsLinha)
        {
            Destroy(item);
        }
    }

    private GameObject[] GetRow(GameObject[,] matrix, int col)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[x, col])
                .ToArray();
    }

    private void DestruirColuna(GameObject[,] matrix, int col)
    {
        GameObject[] gameObjectsColuna = GetColumn(matrix, col);
        foreach (var item in gameObjectsColuna)
        {
            Destroy(item);
        }
    }

    public GameObject[] GetColumn(GameObject[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[columnNumber, x])
                .ToArray();
    }
}
