using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ManagerTutorialText : MonoBehaviour
{
    private int currentText = 0;
    private int maxText = 12;
    private int currentArrow = -1;
    public GameObject[] arrows;
    public GameObject buttonOfBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        if (currentText != 0)
        {
            buttonOfBack.SetActive(true);
        }
        chooseText();
        if (currentText == maxText)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
    public void newText(bool next)
    {
        if (next)
        {
            currentText++;
        }
        else
        {

            currentText--;
        }
        
    }
    void chooseText()
    {
        string contentOfText = "";
        switch (currentText)
        {
            case 0:
                contentOfText = "Bom dia Jogador!! Meu nome é Pedro sou um guerreiro dessa bela ilha denominada pindorama!! Bom, preciso de sua ajuda, os portuguêses estão chegando para nos destruir! Você pode nos ajudar?";
                break;
            case 1:
                contentOfText = "Que ótimo!! Para que você consiga movimentar o Moeté é necessário apertar as teclas W,S,A,D. Com isso o pajé irá se movimentar para frente, atrás, esquerda e direita respectivamente";
                break;
            case 2:
                contentOfText = "Para que Moeté destrua os inimigos é necessário clicar com a tecla J";
                break;
            case 3:
                contentOfText = "Para que Moeté consiga receber as bençãos dos seres mitológicos é necessário clicar na seta indicada e escolher qual poder desejado";
                currentArrow = 0;
                break;
            case 4:
                contentOfText = "Há três beçãos que podem ser concebidas. O pai do mato fornece o jogador a habilidade de arremesar uma pedra atinge os inimigos em área. A benção da Mãe do Ouro paralisa os inimigos e dobra a quantidad de ouro recebida pelo jogador quando abate um inimigo. Por fim, o Boitatá arremessa uma bola de fogo que quando atinge os inimigos queimam esses."; 
                break;
            case 5:
                contentOfText = "Para utilizar os poderes especiais das bençãos é necessário clicar na tecla K. Caso queira sair da benção clica na tecla L.";

                break;
            case 6:
                contentOfText = "Para defender a tribo é necessário utilizar o sistema de torres. Para que isso seja realizado, clique em cima de uma torre e escolha qual tipo você deseja. Cada torre pode ser melhorada três vezes, para melhorar clique novamente em cima da torre clique na seta verde.";

                break;
            case 7:
                contentOfText = "Caso você queira vender a torre, apenas clique na imagem com um $. O valor da venda das torres é menor do que o de compra";
                break;
            case 8:
                contentOfText = "Para melhorar as torres é necessário agrupar recursos, cada torre gasta certa quantidade de recursos. Cada inimigo derrotado irá fornecer uma certa quantia de recursos, sendo que é recebido uma quantia inicial para estruturar o ataque.";
                break;
            case 9:
                contentOfText = "Lembre-se há três tipos de torre: Arco e flecha (Maior DPS e area de ataque), Guerreiros (posiciona três guerreiros que lutam contra os inimigos) e Musical (O ritmo da música deixa os inimigos lentos e fornece um dano em área). Para posicionar os guerreiros é necessário clicar com o botão esquerdo do mouse dentro da área indicada.";
                break;
            case 10:
                contentOfText = "Para iniciar a fase, é necessário clicar na posição indicada. Essa irá mostrar a você quais inimigos virão na próxima rodada";
                currentArrow = 1;
                break;
            case 11:
                contentOfText = "Cada inimigo que conseguir chegar na torre do pajé você irá perder 1 vida. Caso você perca todas as vidas a aldeia é derrotada. Porém, se conseguir derrotar todos os inimigos a aldeia vence.";
                break;
            case 12:
                contentOfText = "Espero que você tenha entendido, caso não tenha, apenas volte para as instruções anteriores. Vamos lutar por nossa tribo !!";
                break;

        }

        gameObject.GetComponent<TextMeshProUGUI>().text = contentOfText;
        if (currentArrow != -1)
        {
            arrows[currentArrow].SetActive(true);
            currentArrow = -1;
        }
        else
        {
            foreach (GameObject imageArrow in arrows)
            {
                imageArrow.SetActive(false);
            }
            
        }
    }
}
