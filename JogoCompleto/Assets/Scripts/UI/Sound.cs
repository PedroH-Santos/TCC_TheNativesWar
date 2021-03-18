using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    
     
    public List<AudioCondition> audios;
    private int currentAudio;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentAudio = -1;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.tag == "Player")
        {
            if(GetComponent<PlayerAnimation>().stateOfPlayer == "Transformation")
            {
                if (!audioSource.isPlaying)
                {
                    Time.timeScale = 1;
                }
            }
        }


        
    }
    public void playAudio(string condition)
    {
        if (!GameObject.FindGameObjectWithTag("Director")) //Se não encontrar o diretor é por que as cutscenes já foram passadas
        {
            List<AudioClip> audiosUsed;
            audiosUsed = null;
            foreach (AudioCondition audio in audios)
            {
                if (audio.condition == condition)
                {
                    audiosUsed = audio.clip;
                }
            }
            if (gameObject.TryGetComponent<AudioSource>(out AudioSource component))
            {
                if (!audioSource.isPlaying) //Die tem prioridade para interromper qualquer som
                {
                    if (audiosUsed.Count > 1 && currentAudio < audiosUsed.Count)
                    {
                        currentAudio = Random.Range(0, audiosUsed.Count);
                    }
                    else
                    {
                        currentAudio = audiosUsed.Count - 1;
                    }
                    if (audioSource.clip == audiosUsed[currentAudio])
                    {
                        return;
                    }
                    else
                    {
                        audioSource.clip = audiosUsed[currentAudio];
                    }

                    audioSource.Play();

                }
            }
        }
    }
}
[System.Serializable]
public class AudioCondition
{
    public List<AudioClip> clip;
    public string condition;
}
