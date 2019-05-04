using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookoo : MonoBehaviour
{
    public bool m_PlayerIsColliding;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_PlayerIsColliding = true;
        }
    }
}
