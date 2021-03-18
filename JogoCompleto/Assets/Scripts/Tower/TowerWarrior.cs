using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerWarrior : MonoBehaviour
{
    [HideInInspector] public bool setPositionWarriros; 
    [HideInInspector] public int indexOFTower;
    [HideInInspector] public bool levelUpWarriors;
    [HideInInspector] public List<Vector3> positionOfwarriorsDead;
    [HideInInspector] public List<GameObject> warriors;
    public List<LevelTower> levelTower;

    private LevelTower currentLevel;
    private float timeLastSpawn;
    private Vector3 clickPosition;
    private Vector3 startPosition;
    private List<GameObject> enemyInAreaTower;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        levelUpWarriors = false;
        setPositionWarriros = false;
        startPosition = gameObject.transform.position;
        incrementLevelTower = levelTower[0]; //Inicia level torre
        warriors = new List<GameObject>();
        enemyInAreaTower = new List<GameObject>();
        positionOfwarriorsDead = new List<Vector3>();
        timeLastSpawn = Time.time;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<DrawCircleInScreen>().enabled)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit)
                {

                    if (hit.transform.gameObject == gameObject)
                    {

                        setPositionWarriros = true;
                        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }
                }
            }
        }//Testa se o player está no raio 
        if (positionOfwarriorsDead.Count>0) //Spawna os inimigos novamente quando eles morrem
        {
            
                if (Time.time - timeLastSpawn > currentLevel.intervalForSpawnWarriors)
                {                 
                    foreach(Vector3 positionWarriorDead in positionOfwarriorsDead)
                    {
                        spawnWarrior(gameObject.transform.position, positionWarriorDead);
                        positionOfwarriorsDead.Remove(positionWarriorDead);
                        break;
                    }
                    timeLastSpawn = Time.time;
                }
        }
        else
        {
            timeLastSpawn = Time.time;
        }
        
        if (setPositionWarriros)
        {
            //Move warriors pelo raio da torre, para posiciona-los em um novo local
            if (warriors.Count > 0)
            {
                newClickPosition();

            }
            else if (warriors.Count == 0 && warriors.Count <= currentLevel.countWarriors)  //Spawna warriors, posiciona a distancia entre os mesmos
            {
                putWarriorsInScene();
            }
            setPositionWarriros = false;

        }
       
        //Upa warriors para o proximo level
        if (levelUpWarriors && indexOFTower > 0 )
        {
            if (warriors.Count > 0)
            {
                OnDestroy(); //Função que limpa os warriors
                putWarriorsInScene();
                
            }
            levelUpWarriors = false;

        }


    }
    public bool checkTargetInWarriors(GameObject warriorAttack,GameObject target) //Testa se os warriors não possuem o mesmo target
    {
    
        foreach (GameObject warrior in warriors)
        {
            if (warrior != null)
            {
                if (warriorAttack != warrior)
                {
                    if (warrior.GetComponentInChildren<IndioWarriorAttack>().target == target) //Verifica se já possui um warrior lutando com aquele inimigo
                    {
                        return false;
                    }
                }



            }
        }
        return true;

    }
    private void newClickPosition()
    {
        Vector3 setClickPosition = clickPosition;
        bool isFirst = true;
        foreach (GameObject warrior in warriors)
        {
            if (warrior != null)
            {
                if (warrior.GetComponent<IndioAnimation>().stateWarrior == "Idle")
                {
                    if (isFirst)
                    {
                        setClickPosition = new Vector3(setClickPosition.x, setClickPosition.y, 0);
                        isFirst = false;
                    }
                    else
                    {
                        setClickPosition = new Vector3(setClickPosition.x + 0.5f, setClickPosition.y, 0);
                    }

                    warrior.GetComponent<IndioWarrior>().targetPosition = setClickPosition;
                    warrior.GetComponent<IndioWarrior>().lastPosition = setClickPosition;
                    warrior.GetComponent<IndioAnimation>().stateWarrior = "Run";
                }

            }
        }
    }
    private void putWarriorsInScene()
    {  
        Vector3 setClickPosition = clickPosition;   
        Vector3 setStartPosition = startPosition;   
        for (int i = 0; i < currentLevel.countWarriors; i++)  
        {      
            if(i == 0) {
                setClickPosition = new Vector3(setClickPosition.x, setClickPosition.y, 0);
                
            } else {     
                setClickPosition = new Vector3(setClickPosition.x + 0.5f, setClickPosition.y, 0);
            }            
            if (levelUpWarriors)
                        
            {
                setStartPosition = setClickPosition;
            }            
            else            
            {               
                setStartPosition = new Vector3(setStartPosition.x + 0.5f, setStartPosition.y, 0);          
            }             
            spawnWarrior(setStartPosition, setClickPosition);
        }
        



    }
    private void spawnWarrior(Vector3 setStartPosition, Vector3 setClickPosition)
    {
        setStartPosition.z = currentLevel.prefabWarrior.transform.position.z;
        setClickPosition.z = currentLevel.prefabWarrior.transform.position.z;
        GameObject warrior = Instantiate(currentLevel.prefabWarrior) as GameObject;
        IndioWarrior scriptWarrior = warrior.GetComponent<IndioWarrior>();
        scriptWarrior.lastPosition = setClickPosition;
        scriptWarrior.targetPosition = setClickPosition;
        scriptWarrior.startPosition = setStartPosition;
        scriptWarrior.towerOfWarrior = gameObject;
      
        warriors.Add(warrior);



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision = collision.GetComponent<BoxCollider2D>())
        {
            if (collision.gameObject.tag == "Enemy")
            {
                enemyInAreaTower.Add(collision.gameObject);
            }else if(collision.gameObject.tag == "Player")
            {
                player = collision.gameObject;
                player.GetComponentInChildren<PlayerAttack>().setNewAttack(currentLevel.setChangedAttack,true); //Modify the strong of player

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision == collision.GetComponent<BoxCollider2D>())
        {
                if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Gods") //Desabilita Area do Trigger
                {
                    GetComponent<DrawCircleInScreen>().enabled = false;
                    GetComponent<LineRenderer>().enabled = false;
                if (collision.gameObject.tag == "Player")
                {
                    collision.GetComponentInChildren<PlayerAttack>().setNewAttack(currentLevel.setChangedAttack, false); //Modify the strong of player
                    player = null;
                }
                }else if (collision.gameObject.tag == "Enemy")
                {
                    enemyInAreaTower.Remove(collision.gameObject);
                }
        }
    }
    public void OnDestroy() //Quando a torre é destruida ela limpa todos os guerreiros que a mesma spawnous
    {
        foreach (GameObject warrior in warriors)
        {
            if (warrior != null)
            {
                warrior.GetComponent<IndioAnimation>().stateWarrior = "Die";
                Destroy(warrior);
            
            }
        }
        
        warriors.Clear();
        positionOfwarriorsDead.Clear();
    }

    //Metodos get e set para relacionar os leveis da torre
    public LevelTower incrementLevelTower {
        get
        {
            return currentLevel ;
        }
        set
        {
            if (player != null) //Case the player already stayed in area of the tower is necessary set a new paramanter
            {
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().setNewAttack(currentLevel.setChangedAttack, false); //Return the attack standart
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().setNewAttack(value.setChangedAttack, true); //Modify the attack for the new level tower
            }
            currentLevel = value;
            indexOFTower = levelTower.IndexOf(currentLevel);
                if (indexOFTower < levelTower.Count)
                {
                    GetComponent<SpriteRenderer>().sprite = currentLevel.sprite;
                    if (indexOFTower > 0)
                    {

                        levelUpWarriors = true;
                    }



                }



        }
    }
    [System.Serializable]
    public class LevelTower
    {
  
        public int moneySell;
        public int moneyBuy;
        public Sprite sprite;

        public int countWarriors;
        public float intervalForSpawnWarriors;
        public int setChangedAttack;
        public GameObject prefabWarrior;

    }

}

