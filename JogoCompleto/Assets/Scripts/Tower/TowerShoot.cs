using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{

    public List<LevelTower> levelTower;
    public Transform positionOfNativeShoot;
    private float timeLastDamage;
    private LevelTower currentLevel;
    private List<GameObject> targetEnemy;
    private GameObject target;
    private GameObject nativeShoot;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        targetEnemy = new List<GameObject>();
        timeLastDamage = Time.time;
    }
    private void Awake()
    {
        incrementLevelTower = levelTower[0];
    }
    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
            if (gameObject.transform.position.x < target.transform.position.x) //Determina posicao para o arqueiro atirar
            {
                nativeShoot.GetComponent<IndioAnimation>().direction = "East";
            }
            else
            {
                nativeShoot.GetComponent<IndioAnimation>().direction = "West";
               
            }
            if (Time.time - timeLastDamage > currentLevel.intervalForDamage)
            {
                nativeShoot.GetComponent<IndioAnimation>().stateWarrior = "Attack";
                timeLastDamage = Time.time;

            }
            if (nativeShoot.GetComponent<IndioAnimation>().alertFinishedAnimation)
            {
                nativeShoot.GetComponent<IndioAnimation>().alertFinishedAnimation = false;
                shoot();

            }
        }
        else
        {
            target = getNewEnemy();
        }

            

    }
    private GameObject getNewEnemy()
    {
        if (targetEnemy.Count > 0)
        {
            foreach (GameObject getTarget in targetEnemy)
            {
                if (getTarget != null)
                {
                    return getTarget;
                }
            }
        }
        return null;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == collision.GetComponent<BoxCollider2D>())
        {
            if (collision.gameObject.tag == "Enemy")
            {
                targetEnemy.Add(collision.gameObject);
            }
            else if (collision.gameObject.tag == "Player")
            {
                player = collision.gameObject;
                player.GetComponent<Player>().setNewSpeed(currentLevel.setChangedSpeed, true); //Modify the speed of player
            }


        }
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == collision.GetComponent<BoxCollider2D>())
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (collision.gameObject == target)
                { //Se o inimigo sair da colisao o arqueiro nao ira atirar
                    target = null;
                }
                targetEnemy.Remove(collision.gameObject);
            }
            else if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Gods") //Habilita area do trigger
            {
                GetComponent<DrawCircleInScreen>().enabled = false;
                GetComponent<LineRenderer>().enabled = false;
                if (collision.gameObject.tag == "Player")
                {
                    player.GetComponent<Player>().setNewSpeed(currentLevel.setChangedSpeed, false); //Modify the speed of player
                    player = null;
                    
                }
            }
        }
    }
    private void shoot()
    {

            
        Vector3 startPosition = nativeShoot.transform.GetChild(0).transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = currentLevel.prefabArrow.transform.position.z;
        targetPosition.z = currentLevel.prefabArrow.transform.position.z;

        GameObject bullet = (GameObject)Instantiate(currentLevel.prefabArrow);
        if (nativeShoot.GetComponent<IndioAnimation>().direction == "West")
        {
            bullet.transform.Rotate(0, 0, 180); //Rotaciona a bala para 180 
        }
        bullet.transform.position = startPosition;
        Projectile scriptBullet = bullet.GetComponent<Projectile>();
        scriptBullet.target = target;
        scriptBullet.speedBullet = currentLevel.speedForArrow;
        scriptBullet.damage = currentLevel.damage;
    }

    public LevelTower incrementLevelTower
    {
        get
        {
            return currentLevel;

        }
        set
        {
            if (player != null) { //Case the player already stayed in area of the tower is necessary set a new paramanter
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setNewSpeed(currentLevel.setChangedSpeed, false); //Return the speed standart
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setNewSpeed(value.setChangedSpeed, true); //Modify the speed for the new level tower
            }
           
            currentLevel = value;
            int indexOFTower = levelTower.IndexOf(currentLevel);

            if (indexOFTower < levelTower.Count )
            {
                Destroy(nativeShoot);
                nativeShoot = Instantiate(currentLevel.nativeShoot);
                nativeShoot.transform.parent = gameObject.transform;
                nativeShoot.transform.position = positionOfNativeShoot.position;

                GetComponent<SpriteRenderer>().sprite = currentLevel.sprite;
                


            }
        }
    }
    [System.Serializable]
    public class LevelTower
    {
        public float setChangedSpeed;
        
        public int moneySell;
        public int moneyBuy;

        public int damage;
        public float intervalForDamage;
        public float speedForArrow;

        public GameObject prefabArrow;
        public GameObject nativeShoot;

        public Sprite sprite;


    }

}
