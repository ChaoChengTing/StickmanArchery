using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject mainCam, playerCam; //兩個不同的攝影機 & 射擊
    private bool first = true;
    private Transform IniTransform;
    private GameObject m_controlPlayer;
    private bool m_isCamera = true;
    /* 放在Awake內，在物件執行之前就先封鎖住，避免物件的出現，直到使用者切換攝影機後再創造該物件，若是放在Start內則會造成全部的物件都已經產生且初始化後再封鎖住。 */

    private void Awake()
    {
        //預設先開啟第一部攝影機
        //一定要先暫停不使用的攝影機後，再開啟要使用的攝影機！
        playerCam.SetActive(false);
        mainCam.SetActive(true);
    }

    private void OnGUI()
    {
        if (GetComponent<Player>().m_player != -1)
        {
            if (GUI.Button(new Rect(75, 50, 195, 50), "<color=white><size=25>" + "SwitchCamera" + "</size></color>") && Input.GetMouseButtonUp(0)) //(左,上,寬,高)
            {
                if (GetComponent<Player>().m_player == 0)
                    playerCam.GetComponent<CameraCircularMove>().x = 270;
                else if (GetComponent<Player>().m_player == 1)
                    playerCam.GetComponent<CameraCircularMove>().x = 90;
                playerCam.GetComponent<CameraCircularMove>().y = 0;
                m_controlPlayer.transform.rotation = Quaternion.Euler(playerCam.GetComponent<CameraCircularMove>().y, playerCam.GetComponent<CameraCircularMove>().x, 0);
                m_isCamera = !m_isCamera;
                Cursor.visible = !Cursor.visible;
            }
        }
    }

    private void Start()
    {
        if (!playerCam.GetComponent<CameraCircularMove>())
            Debug.LogError("Player Camera Should Get CameraCircularMove Component.");
    }

    private void Update()
    {
        m_controlPlayer = GetComponent<Player>().m_controlPlayer;
        if (m_controlPlayer.GetComponent<Hurt>())
        /*if (m_controlPlayer.GetComponent<Hurt>().isDead)
        {
            playerCam.GetComponent<CameraCircularMove>().enabled = false;
            playerCam.transform.position = m_controlPlayer.GetComponent<Shoot>().m_eyesPosition.position;
        }
        else*/
        {
            if (m_controlPlayer.GetComponent<Shoot>())
                if (m_isCamera)
                {
                    mainCam.SetActive(true);
                    playerCam.SetActive(false);
                    Cursor.visible = true;
                    m_controlPlayer.GetComponent<Shoot>().m_isFirstPerson = false;
                }
                else
                {
                    playerCam.SetActive(true);
                    mainCam.SetActive(false);
                    if (Input.GetKeyUp(KeyCode.Escape))
                        Cursor.visible = !Cursor.visible;
                    m_controlPlayer.GetComponent<Shoot>().m_isFirstPerson = true;
                    m_controlPlayer.transform.rotation = playerCam.transform.rotation;
                    playerCam.transform.position = m_controlPlayer.GetComponent<Shoot>().m_eyesPosition.position;
                }
        }
    }
}