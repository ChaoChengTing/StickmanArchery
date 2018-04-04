using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject m_controlPlayer;
    public int m_player = -1;
    public GameObject[] player = new GameObject[2];
    private bool m_isFirst = true;

    private void OnGUI()
    {
        if (m_isFirst)
        {
            GUI.Label(new Rect(1500, 960, 300, 100), "<color=white><size=50>" + "Player1" + "</size></color>");
            GUI.Label(new Rect(75, 960, 300, 100), "<color=white><size=50>" + "Player2" + "</size></color>");
            if (GUI.Button(new Rect(200, 50, 120, 50), "<color=white><size=25>" + "Player1" + "</size></color>") && Input.GetMouseButtonUp(0))
            {
                m_isFirst = false;
                m_player = 0;
                player[1].GetComponent<ComAI>().enemy = player[0];
                player[0].GetComponent<Shoot>().enabled = true;
                player[1].GetComponent<ComAI>().enabled = true;
            }
            if (GUI.Button(new Rect(70, 50, 120, 50), "<color=white><size=25>" + "Player2" + "</size></color>") && Input.GetMouseButtonUp(0))
            {
                m_isFirst = false;
                m_player = 1;
                player[0].GetComponent<ComAI>().enemy = player[1];
                player[0].GetComponent<ComAI>().enabled = true;
                player[1].GetComponent<Shoot>().enabled = true;
            }
        }
    }

    // Use this for initialization
    private void Start()
    {
        m_controlPlayer = gameObject;
        if (player[0].GetComponent<Shoot>())
            player[0].GetComponent<Shoot>().enabled = false;
        else
            Debug.LogError("Player1 Should Get Shoot Component.");
        if (player[1].GetComponent<Shoot>())
            player[1].GetComponent<Shoot>().enabled = false;
        else
            Debug.LogError("Player2 Should Get Shoot Component.");
        if (player[0].GetComponent<ComAI>())
            player[0].GetComponent<ComAI>().enabled = false;
        else
            Debug.LogError("Player1 Should Get ComAI Component.");
        if (player[1].GetComponent<ComAI>())
            player[1].GetComponent<ComAI>().enabled = false;
        else
            Debug.LogError("Player2 Should Get ComAI Component.");
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_player != -1)
            m_controlPlayer = player[m_player];
    }
}