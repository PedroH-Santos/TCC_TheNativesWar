using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ControllerStartPhases : MonoBehaviour
{
    static public int phase;
    static bool changedPhase = true; //Variavel que controla quando pode ser chamado a função para alterar o mapa
    public List<Phase> phasesOfGame; 
    void Start()
    {
   
        changedPhase = true;
        testIfHaveMoreOfOneMapManager();
        DontDestroyOnLoad(transform.gameObject);

    }
    // Start is called before the first frame update
    void Awake()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        if (changedPhase)
        {
            testHowPhaseIsOpen();
        }

    }
    void testIfHaveMoreOfOneMapManager() //Destroy o outro UI_Controller que é criado
    {
        GameObject[] maps = GameObject.FindGameObjectsWithTag("ManagerPhase");
        if (maps.Length > 1)
        {
            Destroy(gameObject);
        }
    }
    void resetListOfPhases() //Reseta a lista das frases
    {
        foreach (Phase phas in phasesOfGame)
        {
            //phas.isReady = false;
            phas.objectPhase.transform.GetChild(1).GetComponent<Image>().enabled = true;
            phas.objectPhase.transform.GetChild(0).transform.GetChild(0).GetComponent<RawImage>().enabled = false;

        }
    }
    public void testHowPhaseIsOpen() //Testa qual phase está pronta para ser jogada e desabila o background dessa fase
    {  
        foreach (Phase phas in phasesOfGame)
        {
            if (phas.isReady == false || !phas.phaseIsFinished) //Somente as fases que podem ser habilitadas passam por aqui
            {
               
                if(phas.phase == phase) //A cara do pajé só estará na última fase
                {
                    phas.isReady = true;
                    phas.objectPhase.transform.GetChild(1).GetComponent<Image>().enabled = false; //Desabilita o fundo preto
                    phas.objectPhase.transform.GetChild(0).transform.GetChild(0).GetComponent<RawImage>().enabled = true; //Habilita o fundo preto
                }
                else if (phas.phase > phase) //As fases que o player não passou tem o fundo preto
                {
                    phas.objectPhase.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
            }
            else
            {

                if (phas.phase <= phase) // As fazes que o player já passou podem ser habilitadas 
                {
                    phas.isReady = true;

                    phas.objectPhase.transform.GetChild(1).GetComponent<Image>().enabled = false; //Desabilita o fundo preto
                }
            }
               

        }


        }
    public void addsPhase() //Função que incrementa a fase
    {
        if (phase + 1 <= phasesOfGame.Count)
        {
            foreach (Phase phas in phasesOfGame)
            {
                if (phas.scene == SceneManager.GetActiveScene().name)
                {
                    if (!phas.phaseIsFinished)
                    {
                        phas.phaseIsFinished = true;
                        phase++;
                    }
                }
            }
        }
        disableAllObjects(true);
    }
    public void isFreePhase(GameObject phase)
    {
        if (phasesOfGame!=null)
        {
            foreach (Phase phas in phasesOfGame)
            {
                if (phase == phas.objectPhase) //Testa se o objeto clickado pelo player é uma fase
                {
                    if (phas.isReady == true)
                    {
                    
                        changedPhase = false;
                        disableAllObjects(false); //Desabilita todas os outros objetos a não ser o gameobject do script
                        resetListOfPhases(); //Reseta a lista para que quando alterar de fase refazer o mapa
                        SceneManager.LoadScene(phas.scene);//Instancia a nova cena
                        



                    }
                }
            }

        }

    }
    public void disableAllObjects(bool disable) //Desabilita os outros objetos da scene
    {
        for (int i = 0; i < gameObject.transform.childCount; i++) { 
            gameObject.transform.GetChild(i).gameObject.SetActive(disable);
            
           
        }
                
            
        
 

    }
    [System.Serializable]
    public class Phase //Classe adaptada para as caracteristicas de cada fase
    {
        public int phase;
        public GameObject objectPhase;
        [HideInInspector] public bool isReady = false; //Variavel para testar se a fase está habilitada
        public string scene;
        [HideInInspector] public bool phaseIsFinished = false;
    }

}
