using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetTower : MonoBehaviour
{

    private GameObject tower;
    private UI ui;
    // Start is called before the first frame update
    void Start()
    {
        tower = gameObject.transform.parent.transform.parent.gameObject; //pegando o gameobject da torre
        ui = GameObject.Find("UI").GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void levelUp(GameObject choseOfTower)
    {

        if (canLevelUp(choseOfTower))
        {
            Instantiate(choseOfTower, new Vector3(tower.transform.position.x, tower.transform.position.y, tower.transform.position.z), Quaternion.identity);
            Destroy(tower);
        }
    }
    private bool canLevelUp(GameObject choseOfTower)
    {

        int moneyBuy = 0;
        if (choseOfTower.tag == "TowerShoot")
        {
            moneyBuy = choseOfTower.GetComponent<TowerShoot>().levelTower[0].moneyBuy;

        }
        else if (choseOfTower.tag == "TowerMusic")
        {
            moneyBuy = choseOfTower.GetComponent<TowerMusic>().levelTower[0].moneyBuy;
        }
        else if (choseOfTower.tag == "TowerWarrior")
        {

            moneyBuy = choseOfTower.GetComponent<TowerWarrior>().levelTower[0].moneyBuy;
        }
        if (ui.goldPlayer >= moneyBuy)
        {
            ui.ChangeGold -= moneyBuy;
            return true;
            
        }
        return false;
    } //Testa se o player possui dinheiro para comprar a torre

    

}
