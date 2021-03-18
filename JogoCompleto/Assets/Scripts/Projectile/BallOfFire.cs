using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class BallOfFire : MonoBehaviour
{
    public TileBase tileFire;

    [HideInInspector] public Vector2 directionPlayer;
    [HideInInspector] public float timeForEndFire;
    [HideInInspector] public int ticksOfFire;
    [HideInInspector] public int damageOfFire;
    [HideInInspector] public float distance;
    [HideInInspector] public float speedForBallOfFire;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float currentTimeForBackTile;
    private float currentTimeForFire;
    private float spawnTime;
    private float timeForFire = 0.5f;

    private Tilemap tileMap;
    private Vector3Int getCurrentCell;
    private Vector3Int currentCell;

    // Start is called before the first frame update
    void Start()
    {

        currentTimeForFire = Time.time;
        spawnTime = Time.time;
        currentTimeForBackTile = Time.time;
        tileMap = GameObject.FindGameObjectWithTag("Effects").GetComponent<Tilemap>();
        startPosition = gameObject.transform.position;
        targetPosition = new Vector3(startPosition.x + (distance * directionPlayer.x), startPosition.y + (distance * directionPlayer.y), 0); // calcula a posição de chegada do projetil
    }

    // Update is called once per frame
    void Update()
    {
       
        if (gameObject.transform.position.Equals(targetPosition))
        {
             currentCell = tileMap.WorldToCell(transform.position); // pega o cell da posicao atual da bola
            if (currentCell != getCurrentCell)
            {
                transformTileMap(tileFire); // transforma o tile me fogo 4x4
                currentTimeForBackTile = Time.time;
            }
            else
            {
                if (Time.time - currentTimeForBackTile > timeForEndFire)
                {
                    transformTileMap(null); // retorna o tile ao normal
                    tileMap.GetComponent<Effects>().clearListGameObject(); // limpa a lista de inimigos de effects
                    Destroy(gameObject);
                }
            }
            //Chama a função do tile em effects para dar o efeito de queima nos inimigos
            if (Time.time - currentTimeForFire > timeForFire)
            {
                tileMap.GetComponent<Effects>().fire(damageOfFire, ticksOfFire);
                currentTimeForFire = Time.time;
            }
                
            
            
            gameObject.GetComponent<SpriteRenderer>().enabled = false;


        }
        else
        {
            float intervalTime = Time.time - spawnTime;
            gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, intervalTime * speedForBallOfFire / distance);
            gameObject.transform.Rotate(0,0,20);
        }



    }
    private void transformTileMap(TileBase tile)
    {
        getCurrentCell = currentCell;
        getCurrentCell.x -= 1;
        tileMap.SetTile(getCurrentCell, tile);
        getCurrentCell = currentCell;
        getCurrentCell.y -= 1;
        tileMap.SetTile(getCurrentCell, tile);
        getCurrentCell = currentCell;
        getCurrentCell.y -= 1;
        getCurrentCell.x -= 1;
        tileMap.SetTile(getCurrentCell, tile);
        tileMap.SetTile(currentCell, tile);
        getCurrentCell = currentCell;
    }


}

