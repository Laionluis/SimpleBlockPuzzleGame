using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private CanvasGroup canvasGroup;
    private Grid grid;
    private GameObject contornoAtual;
    private Vector3Int dragCellPosicaoAtual;
    private CreateGameObject createGameObject;
    private Tilemap tilemap;
    public GameObject controller;

    public void OnDrag(PointerEventData eventData) 
    {
        //Create a ray going from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        //Calculate the distance between the Camera and the GameObject, and go this distance along the ray
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
        //Move the GameObject when you drag it
        transform.position = rayPoint;       
        
        if (tilemap != null && createGameObject != null)
        {
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
            Vector3 posicaoReal = tilemap.CellToWorld(cellPosition) + new Vector3(.57f, .57f, 1) * .5f;
            GameObject auxContorno;
            if (cellPosition.x >= 0 && cellPosition.x <= 7 && cellPosition.y >= 0 && cellPosition.y <= 7)
            {
                if (cellPosition != dragCellPosicaoAtual)
                {
                    DestroyImmediate(contornoAtual);
                    auxContorno = createGameObject.Create(posicaoReal);
                    
                    //Debug.Log(cellPosition);
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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        Debug.Log(controller.GetComponent<CreateGameObject>().positions[cellPosition.x, cellPosition.y]);
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        controller = GameObject.FindGameObjectWithTag("GameController");
        tilemap = Tree.FindObjectOfType<Tilemap>();
        createGameObject = Tree.FindObjectOfType<CreateGameObject>();
    }
}
