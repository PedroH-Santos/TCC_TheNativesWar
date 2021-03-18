using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMusic : MonoBehaviour
{
    private LevelTower currentLevel;
    private float currentTimeForDamage;
    private float currentTimeForSlow;
    private List<GameObject> targetForDamage;
    private List<GameObject> enemySlow;
    private GameObject player;
    public List<LevelTower> levelTower;
    private GameObject nativeMusic;
    public Transform positionOfIndioMusic;
    // Start is called before the first frame update
    void Start()
    {
        enemySlow = new List<GameObject>();
        incrementLevelTower = levelTower[0];
        currentTimeForSlow = Time.time;
        currentTimeForDamage = Time.time;
        targetForDamage = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetForDamage.Count > 0)
        {
            if (gameObject.transform.position.x < targetForDamage[0].transform.position.x) //Determina posicao para o arqueiro atirar
            {
                nativeMusic.GetComponent<IndioAnimation>().direction = "East";
            }
            else
            {
                nativeMusic.GetComponent<IndioAnimation>().direction = "West";

            }
            if (Time.time - currentTimeForDamage > currentLevel.intervalForDamage )        
            {
                nativeMusic.GetComponent<IndioAnimation>().stateWarrior = "Attack";
                currentTimeForDamage = Time.time;

            }
            if (nativeMusic.GetComponent<IndioAnimation>().alertFinishedAnimation)
            {
                nativeMusic.GetComponent<IndioAnimation>().alertFinishedAnimation = false;
                currentTimeForSlow = Time.time;
                attackForMusic();
            }    
            if (enemySlow.Count > 0)
            {
                if (Time.time - currentTimeForSlow > currentLevel.intervalTimeSlow)
                {
                    removeSlow();
                    currentTimeForSlow = Time.time;
                }
            }
        }
    }
    private void removeSlow() {
        foreach (GameObject target in targetForDamage)
        {
            if (target != null)
            {
                 
                if ( enemySlow.Contains(target))
                {
                    target.GetComponent<Enemy>().speed += currentLevel.slow;
                    enemySlow.Remove(target);
                    target.GetComponent<Enemy>().startPosition = target.transform.position;
                    target.GetComponent<Enemy>().lastTimeSwithWaypoints = Time.time;
                }

            }
        }

    }
    private void attackForMusic()
    {
        
        foreach (GameObject target in targetForDamage)
        {
            if (target != null)
            {
                if(target.GetComponentInChildren<Life>().life - currentLevel.damage > 0)
                {
                    target.GetComponentInChildren<Life>().life -= currentLevel.damage;
                    if (enemySlow.Contains(target) == false && target.GetComponent<Enemy>().speed - currentLevel.slow > 0) // Impede que o mesmo enemy sofra duas vezes slow
                    {
                        target.GetComponent<Enemy>().speed -=  currentLevel.slow;
                        enemySlow.Add(target);
                        target.GetComponent<Enemy>().startPosition = target.transform.position;
                        target.GetComponent<Enemy>().lastTimeSwithWaypoints = Time.time;
                    }
                    
                    
                }
                else
                {
                    target.GetComponentInChildren<Life>().life = 0;
                }

            }
        }
    }
    public LevelTower incrementLevelTower
    {
        get
        {
            return currentLevel;
        }
        set
        {
            if (player != null) //Case the player already stayed in area of the tower is necessary set a new paramanter
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRest>().setNewLifeRegen(currentLevel.setChangedLifeRegen, false); //Return the attack standart
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRest>().setNewLifeRegen(value.setChangedLifeRegen, true); //Modify the attack for the new level tower
            }
            currentLevel = value;
            int indexOFTower = levelTower.IndexOf(currentLevel);
            if (indexOFTower < levelTower.Count)
            {
                Destroy(nativeMusic);
                nativeMusic = Instantiate(currentLevel.nativeMusic);
                nativeMusic.transform.parent = gameObject.transform;
                nativeMusic.transform.position = positionOfIndioMusic.position;
                GetComponent<SpriteRenderer>().sprite = currentLevel.sprite;



            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == collision.gameObject.GetComponent<BoxCollider2D>())
        {
            if(collision.gameObject.tag == "Enemy")
            {
                targetForDamage.Add(collision.gameObject);
            }
            else if (collision.gameObject.tag == "Player")
            {
                player = collision.gameObject;
                player.GetComponent<PlayerRest>().setNewLifeRegen(currentLevel.setChangedLifeRegen, true); //Modify the life regen of player
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == collision.gameObject.GetComponent<BoxCollider2D>())
        {
            if (collision.gameObject.tag == "Enemy")
            {
                removeSlow();
                targetForDamage.Remove(collision.gameObject);
            }
            else if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Gods") //Modify the life regen of player
            {
                GetComponent<DrawCircleInScreen>().enabled = false;
                GetComponent<LineRenderer>().enabled = false;
                if (collision.gameObject.tag == "Player")
                {
                    collision.GetComponent<PlayerRest>().setNewLifeRegen(currentLevel.setChangedLifeRegen, false);
                    player = null;
                }

            }
        }
    }
    [System.Serializable]
    public class LevelTower
    {
        public float setChangedLifeRegen;
        public int moneySell;
        public int moneyBuy;
        public float slow;
        public float intervalTimeSlow;
        public int damage;
        public float intervalForDamage;
        public GameObject nativeMusic;
        public Sprite sprite;

    }
}

