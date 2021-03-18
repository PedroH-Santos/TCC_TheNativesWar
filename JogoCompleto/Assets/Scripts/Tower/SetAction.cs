using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAction : MonoBehaviour
{
    private GameObject tower;

    private UI ui;
    private int indexOfTower;


    // Start is called before the first frame update
    void Start()
    {
        tower = gameObject.transform.parent.transform.parent.gameObject;
        indexOfTower = 0;
        ui = GameObject.Find("UI").GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void sell(GameObject basisTower)
    {
          
            int moneySell = 0;
            if (tower.tag == "TowerShoot")
            {
                moneySell = tower.GetComponent<TowerShoot>().incrementLevelTower.moneySell;

            }
            else if (tower.tag == "TowerMusic")
            {
                moneySell = tower.GetComponent<TowerMusic>().incrementLevelTower.moneySell;
            }
            else if (tower.tag == "TowerWarrior")
            {
             
                moneySell = tower.GetComponent<TowerWarrior>().incrementLevelTower.moneySell;
            }
            ui.ChangeGold += moneySell;
            Instantiate(basisTower, new Vector3(tower.transform.position.x, tower.transform.position.y, tower.transform.position.z), Quaternion.identity);
            Destroy(tower);
    }
    public void levelUp()
    {
        int moneyBuy = 0;
            if (tower.tag == "TowerShoot")
            {
                if (indexOfTower < tower.GetComponent<TowerShoot>().levelTower.Count - 1)
                {
                if (ui.goldPlayer >= tower.GetComponent<TowerShoot>().incrementLevelTower.moneyBuy)
                    {
                        tower.GetComponent<TowerShoot>().incrementLevelTower = tower.GetComponent<TowerShoot>().levelTower[indexOfTower + 1];
                        moneyBuy = tower.GetComponent<TowerShoot>().incrementLevelTower.moneyBuy;
                    }

                }

            }
            else if (tower.tag == "TowerMusic")
            {
            if (indexOfTower < tower.GetComponent<TowerMusic>().levelTower.Count - 1)
                {
        
                
                if (ui.goldPlayer >= tower.GetComponent<TowerMusic>().incrementLevelTower.moneyBuy)
                {
                    tower.GetComponent<TowerMusic>().incrementLevelTower = tower.GetComponent<TowerMusic>().levelTower[indexOfTower + 1];
                    moneyBuy = tower.GetComponent<TowerMusic>().incrementLevelTower.moneyBuy;
                }
            }
            }
            else if (tower.tag == "TowerWarrior")
            {
                
                if (indexOfTower < tower.GetComponent<TowerWarrior>().levelTower.Count - 1)
                {

                if (ui.goldPlayer >= tower.GetComponent<TowerWarrior>().incrementLevelTower.moneyBuy)
                    {

                        tower.GetComponent<TowerWarrior>().incrementLevelTower = tower.GetComponent<TowerWarrior>().levelTower[indexOfTower + 1];
                        moneyBuy = tower.GetComponent<TowerWarrior>().incrementLevelTower.moneyBuy;
                    }

                }
               
            }
        if (moneyBuy != 0) //Verific se o player possui dinheiro para comprar a torre
        {
            ui.ChangeGold -= moneyBuy;
            indexOfTower++;
        }

    }



    

   
}
