using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateGameObject : Base
{
    public GameObject[,] positions = new GameObject[8, 8];
    public Sprite sprite;
    public GameObject movePlate, peca1, peca2, peca3, peca4, peca5, peca6, peca7, peca8, peca9, peca10;
    public GameObject peca11, peca12, peca13, peca14, peca15, peca16, peca17, peca18;

    private List<GameObject> pecas;
    // Start is called before the first frame update
    void Start()
    {
        pecas = new List<GameObject> { peca1, peca2, peca3, peca4, peca5, peca6, peca7, peca8, peca9, peca10, peca11, peca12, peca13, peca14, peca15, peca16, peca17, peca18 };
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
        System.Random random = new System.Random();
        int index = random.Next(pecas.Count);
        GameObject pecaAleatoria = pecas[index];
        GameObject gameObject = Instantiate(pecaAleatoria, posicaoInicialBloco, Quaternion.identity);         
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
