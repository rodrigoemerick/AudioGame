using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Storyteller")]
    public AudioSource m_Storyteller;
    public AudioClip[] m_StorytellerClips = new AudioClip[10];

    [Header("SoundFX")]
    public AudioSource m_Bomb;
    public AudioSource m_TV;
    public AudioSource m_Blender;
    public AudioSource m_Alarm;
    public AudioSource m_Cookoo;
    public AudioSource m_Electricity;
    public AudioSource m_Cellphone;
    public AudioClip m_BombCounter;
    public AudioClip m_BombExploded;

    [Header("Check Bools")]
    public bool m_IsTV = false;
    public bool m_IsBlender = false;
    public bool m_IsAlarm = false;
    public bool m_IsCookoo = false;
    public bool m_IsElectricity = false;
    public bool m_IsCellPhone = false;
    public bool m_BombDifused = false;
    
    TV m_TVCheck;
    Electricity m_ElectricityCheck;
    Blender m_BlenderCheck;
    Cellphone m_CellphoneCheck;
    Cookoo m_CookooCheck;
    Alarm m_AlarmCheck;
    
    void Start()
    {
        m_Storyteller.clip = m_StorytellerClips[0];
        m_Storyteller.Play();
        StartCoroutine(WaitForFirstHint());
        m_TVCheck = GameObject.Find("TV").GetComponent<TV>();
        m_ElectricityCheck = GameObject.Find("Electricity").GetComponent<Electricity>();
        m_BlenderCheck = GameObject.Find("Blender").GetComponent<Blender>();
        m_CellphoneCheck = GameObject.Find("Cellphone").GetComponent<Cellphone>();
        m_CookooCheck = GameObject.Find("Cookoo").GetComponent<Cookoo>();
        m_AlarmCheck = GameObject.Find("Alarm").GetComponent<Alarm>();
    }
    
    void Update()
    {
        if (m_IsTV)
        {
            m_TV.Play();
        }
        if (m_TVCheck.m_PlayerIsColliding && m_IsTV && Input.GetKeyDown(KeyCode.E))
        {
            m_TV.Pause();
            m_Storyteller.clip = m_StorytellerClips[2];
            m_Storyteller.Play();
            m_IsTV = false;
            m_IsElectricity = true;
        }

        if (m_IsElectricity)
        {
            m_Electricity.Play();
        }
        if(m_ElectricityCheck.m_PlayerIsColliding && m_IsElectricity && Input.GetKeyDown(KeyCode.E))
        {
            m_Electricity.Pause();
            m_Storyteller.clip = m_StorytellerClips[3];
            m_Storyteller.Play();
            m_IsElectricity = false;
            m_IsBlender = true;
        }

        if (m_IsBlender)
        {
            m_Blender.Play();
        }
        if(m_BlenderCheck.m_PlayerIsColliding && m_IsBlender && Input.GetKeyDown(KeyCode.E))
        {
            m_Blender.Pause();
            m_Storyteller.clip = m_StorytellerClips[4];
            m_Storyteller.Play();
            m_IsBlender = false;
            m_IsCellPhone = true;
        }

        if (m_IsCellPhone)
        {
            m_Cellphone.Play();
        }
        if(m_CellphoneCheck.m_PlayerIsColliding && m_IsCellPhone && Input.GetKeyDown(KeyCode.E))
        {
            m_Cellphone.Pause();
            m_Storyteller.clip = m_StorytellerClips[5];
            m_Storyteller.Play();
            m_IsCellPhone = false;
            m_IsCookoo = true;
        }

        if (m_IsCookoo)
        {
            m_Cookoo.Play();
        }
        if(m_CookooCheck.m_PlayerIsColliding && m_IsCookoo && Input.GetKeyDown(KeyCode.E))
        {
            m_Cookoo.Pause();
            m_Storyteller.clip = m_StorytellerClips[6];
            m_Storyteller.Play();
            m_IsCookoo = false;
            m_IsAlarm = true;
        }

        if (m_IsAlarm)
        {
            m_Alarm.Play();
        }
        if(m_AlarmCheck.m_PlayerIsColliding && m_IsAlarm && Input.GetKeyDown(KeyCode.E))
        {
            m_Alarm.Pause();
            m_Storyteller.clip = m_StorytellerClips[7];
            m_Storyteller.Play();
            m_IsAlarm = false;
            m_BombDifused = true;
        }

        //if (m_BombDifused)
        //{
        //    m_Storyteller.clip = m_StorytellerClips[7];
        //    m_Storyteller.Play();
        //    StartCoroutine(ReloadScene());
        //}
    }

    private IEnumerator WaitForFirstHint()
    {
        yield return new WaitForSeconds(17.0f);
        m_Storyteller.clip = m_StorytellerClips[1];
        m_Storyteller.Play();
        m_Bomb.clip = m_BombCounter;
        m_Bomb.Play();
        m_IsTV = true;
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("AudioGame");
    }

    private IEnumerator ExplodeBombTime()
    {
        yield return new WaitForSeconds(120.0f);
        m_Bomb.clip = m_BombExploded;
        m_Bomb.Play();
        StartCoroutine(WaitForLoseTrashTalk());
    }

    private IEnumerator WaitForLoseTrashTalk()
    {
        yield return new WaitForSeconds(10.0f);
        m_Storyteller.clip = m_StorytellerClips[8];
        m_Storyteller.Play();
        StartCoroutine(ReloadScene());
    }
}