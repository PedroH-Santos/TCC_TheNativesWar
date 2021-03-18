using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControllerUiTower : MonoBehaviour
{
    private UI ui;
    public GameObject chooseOfTower;
    private GameObject tower;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI").GetComponent<UI>();
        tower = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.gameObject; //Pegando a torre principal
    }

    // Update is called once per frame
    void Update()
    {

        if (tower.tag != "Tower") { //A torre básica não tem sell e levelup
            if (gameObject.tag == "Sell")
            {
    
                writeTextMoneySell();
            }else if (gameObject.tag == "LevelUp")
            {
                writeTextLevelUp();
            }
        }
        else
        {

            writeTextMoneyBuy();
           
        }

        
    }
    void writeTextMoneySell()
    {
        int moneySell = 0;
        if (chooseOfTower.tag == "TowerShoot")
        {
            moneySell = chooseOfTower.GetComponent<TowerShoot>().incrementLevelTower.moneySell;

        }
        else if (chooseOfTower.tag == "TowerMusic")
        {
            moneySell = chooseOfTower.GetComponent<TowerMusic>().incrementLevelTower.moneySell;
        }
        else if (chooseOfTower.tag == "TowerWarrior")
        {

            moneySell = chooseOfTower.GetComponent<TowerWarrior>().incrementLevelTower.moneySell;
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "" + moneySell;

    }//Condições para determinar o texto da venda
    void writeTextLevelUp()
    {
        int moneyBuy = 0;
        if (chooseOfTower.tag == "TowerShoot")
        {
            moneyBuy = chooseOfTower.GetComponent<TowerShoot>().incrementLevelTower.moneyBuy;

        }
        else if (chooseOfTower.tag == "TowerMusic")
        {
            moneyBuy = chooseOfTower.GetComponent<TowerMusic>().incrementLevelTower.moneyBuy;
        }
        else if (chooseOfTower.tag == "TowerWarrior")
        {

            moneyBuy = chooseOfTower.GetComponent<TowerWarrior>().incrementLevelTower.moneyBuy;
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "" + moneyBuy;
        setTextOfMoney(moneyBuy);
    } ////Condições para determinar o texto do levelUp
    void writeTextMoneyBuy()
    {
       
        int moneyBuy = 0;
        if (chooseOfTower.tag == "TowerShoot")
        {
            moneyBuy = chooseOfTower.GetComponent<TowerShoot>().levelTower[0].moneyBuy;

        }
        else if (chooseOfTower.tag == "TowerMusic")
        {
            moneyBuy = chooseOfTower.GetComponent<TowerMusic>().levelTower[0].moneyBuy;
        }
        else if (chooseOfTower.tag == "TowerWarrior")
        {

            moneyBuy = chooseOfTower.GetComponent<TowerWarrior>().levelTower[0].moneyBuy;
        }
        setTextOfMoney(moneyBuy);
        gameObject.GetComponent<TextMeshProUGUI>().text = "" + moneyBuy;
        

    } //Condições para determinar o texto da compra
    void setTextOfMoney(int money) //Muda a cor do texto
    {
 
        if (ui.goldPlayer >= money)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0x65, 0x06, 0x06, 0xff);

        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }
}
