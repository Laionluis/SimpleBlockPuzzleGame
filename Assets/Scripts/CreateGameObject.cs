using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateGameObject : Base
{
    public GameObject[,] positions = new GameObject[8, 8];
    public Sprite sprite;
    public GameObject movePlate;
    // Start is called before the first frame update
    void Start()
    {
        CreateBlockPieces();
    }

    public GameObject Create(Vector3 positionCell, string tag)
    {
        GameObject aux = Instantiate(movePlate, positionCell, Quaternion.identity);
        aux.name = "ContornoObject" + tag;
        return aux;
    }

  
    public void CreateBlockPieces()
    {
        GameObject gameObject = new GameObject("ConcretePiece");
        gameObject.transform.position = posicaoInicialBloco;
        gameObject.transform.localScale = new Vector3(2.63f, 2.63f, 1);
        SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        gameObject.AddComponent<CanvasGroup>();
        gameObject.AddComponent<RectTransform>();
        gameObject.AddComponent<DragAndDrop>();
        Destroy(gameObject.GetComponent<Collider2D>());
        gameObject.AddComponent<BoxCollider2D>();
    }

    public GameObject CreateBlockPiecesAux()
    {
        GameObject gameObject = new GameObject("ConcretePiece");
        gameObject.transform.position = posicaoInicialBloco;
        gameObject.transform.localScale = new Vector3(2.63f, 2.63f, 1);
        SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        gameObject.AddComponent<CanvasGroup>();
        gameObject.AddComponent<RectTransform>();
        gameObject.AddComponent<DragAndDrop>();
        Destroy(gameObject.GetComponent<Collider2D>());
        gameObject.AddComponent<BoxCollider2D>();
        return gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
