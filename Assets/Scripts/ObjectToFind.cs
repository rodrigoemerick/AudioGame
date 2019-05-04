using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToFind : MonoBehaviour
{
    public bool m_PlayerIsColliding;
    [SerializeField]
    private bool m_IsYourTurn;
    public bool IsYourTurn { get { return m_IsYourTurn; } set { m_IsYourTurn = value; } }
    [SerializeField]
    private bool m_IsLast;
    public bool IsLast { get { return m_IsLast; }}

    void OnTriggerEnter(Collider other)
    {
        if (m_IsYourTurn)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                m_PlayerIsColliding = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_PlayerIsColliding = false;
        }
    }
}
