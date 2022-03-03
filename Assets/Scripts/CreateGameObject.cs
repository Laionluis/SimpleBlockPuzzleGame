using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateGameObject : Base
{
    public GameObject[,] positions = new GameObject[8, 8];
    public Sprite sprite;
    public GameObject movePlate, peca1, peca2, peca3, peca4, peca5, peca6, peca7, peca8, peca9, peca10;
    public GameObject peca11, peca12, peca13, peca14, peca15, peca16, peca17, peca18;
    public IDictionary<Vector3, GameObject> vetorPosicaoInicial = new Dictionary<Vector3, GameObject>();

    public int Score = 0;

    private List<GameObject> pecas;
    // Start is called before the first frame update
    void Start()
    {
        pecas = new List<GameObject> { peca1, peca2, peca3, peca4, peca5, peca6, peca7, peca8, peca9, peca10, peca11, peca12, peca13, peca14, peca15, peca16, peca17, peca18 };       
        CreateBlockPieces(null);
    }

    public GameObject Create(Vector3 positionCell, string tag)
    {
        GameObject aux = Instantiate(movePlate, positionCell, Quaternion.identity);
        aux.name = "ContornoObject" + tag;
        return aux;
    }

  
    public void CreateBlockPieces(string aux)
    {        
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, pecas.Count);                
            GameObject pecaAleatoria = pecas[index];

            Vector3 pos = i == 0 ? posicaoInicialBloco1 : i == 1 ? posicaoInicialBloco2 : posicaoInicialBloco3;
            Vector3 posaux = pos;
            pos.x = pos.x + 6;
            if (vetorPosicaoInicial.Any(x => x.Key == pos))
                continue;
             
            GameObject gameObject = Instantiate(pecaAleatoria, pos, pecaAleatoria.GetComponent<RectTransform>().rotation);         
            gameObject.name = "pecaTabInicial" + i;
            if (gameObject.tag != "Untagged")
                gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            else
                gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);

            gameObject.GetComponent<DragAndDrop>().animarIrPosicaoInicial = true;
            gameObject.GetComponent<DragAndDrop>().posicaoInicialSetada = posaux;

            vetorPosicaoInicial.Add(posaux, gameObject);
        }
        
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
