using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this);    
    }

    public bool m_IsInMenu = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene("AudioGame");
            m_IsInMenu = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && m_IsInMenu)
        {
            this.GetComponent<QuitGame>().Quit();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
