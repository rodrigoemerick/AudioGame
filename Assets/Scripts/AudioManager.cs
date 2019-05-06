using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Storyteller")]
    public AudioSource m_Storyteller;
    public AudioClip[] m_StorytellerClips = new AudioClip[10];
    private int m_StorytellerIndx = 0;

    [Header("SoundFX")]
    public AudioSource m_Bomb;
    public AudioClip m_BombCounter;
    public AudioClip m_BombExploded;

    
    [Header ("Objects")]
    [SerializeField]
    private GameObject[] m_ObjectsToGet;
    private int m_ObjectsIndx = 0;

    private bool m_PlayerWin = false;

    public float m_TimeToLose;
    
    void Start()
    {
        m_Storyteller.clip = m_StorytellerClips[m_StorytellerIndx];
        m_Storyteller.Play();
        StartCoroutine(WaitForFirstHint());
        m_ObjectsToGet[m_ObjectsIndx].GetComponent<ObjectToFind>().IsYourTurn = true;
    }
    
    void Update()
    {
        if (m_ObjectsToGet[m_ObjectsIndx].GetComponent<ObjectToFind>().m_PlayerIsColliding && Input.GetKeyDown(KeyCode.E))
        {
            if (!m_ObjectsToGet[m_ObjectsIndx].GetComponent<ObjectToFind>().IsLast) {
                ChangeObjectToFind();
                m_StorytellerIndx++;
                m_Storyteller.clip = m_StorytellerClips[m_StorytellerIndx];
                m_Storyteller.Play();
                
            }else if (m_ObjectsToGet[m_ObjectsIndx].GetComponent<ObjectToFind>().IsLast)
            {
                Debug.Log("Defused");
                StopCoroutine(ExplodeBombTime());
                m_Bomb.Pause();
                m_ObjectsToGet[m_ObjectsIndx].GetComponent<AudioSource>().Pause();
                m_Storyteller.clip = m_StorytellerClips[7];
                m_Storyteller.Play();
                StartCoroutine(ReloadScene());
            }
        }
    }

    private IEnumerator WaitForFirstHint()
    {
        yield return new WaitForSeconds(20.0f);
        StartCoroutine(ExplodeBombTime());
        m_StorytellerIndx++;
        m_Storyteller.clip = m_StorytellerClips[m_StorytellerIndx];
        m_Storyteller.Play();
        m_Bomb.clip = m_BombCounter;
        m_Bomb.Play();
        m_ObjectsToGet[m_ObjectsIndx].GetComponent<AudioSource>().Play();
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(5.0f);
        GameObject temp =  GameObject.Find("Menu");
        temp.GetComponent<Menu>().m_IsInMenu = true;
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator ExplodeBombTime()
    {
        yield return new WaitForSeconds(m_TimeToLose);

        if (!m_PlayerWin)
        {
            m_ObjectsToGet[m_ObjectsIndx].GetComponent<AudioSource>().Pause();
            m_Bomb.clip = m_BombExploded;
            m_Bomb.Play();
            m_Bomb.loop = false;
            StartCoroutine(WaitForLoseTrashTalk());
        }
    }

    private IEnumerator WaitForLoseTrashTalk()
    {
        yield return new WaitForSeconds(10.0f);
        m_Storyteller.clip = m_StorytellerClips[8];
        m_Storyteller.Play();
        StartCoroutine(ReloadScene());
    }

    private void ChangeObjectToFind()
    {
        m_ObjectsToGet[m_ObjectsIndx].GetComponent<ObjectToFind>().IsYourTurn = false;
        m_ObjectsToGet[m_ObjectsIndx].GetComponent<AudioSource>().Pause();
        m_ObjectsIndx++;
        if (m_ObjectsIndx <= m_ObjectsToGet.Length)
        {
            m_ObjectsToGet[m_ObjectsIndx].GetComponent<AudioSource>().Play();
            m_ObjectsToGet[m_ObjectsIndx].GetComponent<ObjectToFind>().IsYourTurn = true;
        }
    }
}