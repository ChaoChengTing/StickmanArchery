using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public Transform m_bowPosition;
    public Transform m_eyesPosition;
    public bool m_isBullet = true;
    public bool m_isFirstPerson = true;
    public GameObject m_shootingArrow;
    public float m_shootingForce = 1000;
    public GameObject m_shootingObject;
    public int max = 10;
    private Animator animator;
    private List<GameObject> arrowList = new List<GameObject>();
    private List<GameObject> bulletList = new List<GameObject>();

    //(1)
    private GUIStyle currentStyle = null;

    private bool isAttack = true;
    private bool isReady = false;
    private float powerStart = 0;
    private float powerTotal = 0;
    private List<float> timeList = new List<float>();
    /*
    private void Start()
    {
        if (!m_shootingObject)
            Debug.LogError("You need to assign a shooting object.");
    }*/

    //建毅版
    /*
    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Vector3 v3 = Input.mousePosition;
            v3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            go = Instantiate(m_shootingObject) as GameObject;
            go.transform.position = start.transform.position;
            bulletList.Add(go);
            if (m_isBullet)
                go.AddComponent<Bullet>();
            if (bulletList.Count > max)
            {
                GameObject temp = bulletList[0];
                bulletList.Remove(temp);
                Destroy(temp.gameObject);
            }
            //v3.z += 5f;
            //v3.y += 2f;
            go.transform.LookAt(v3);
        }

        //  Vector2 v2 = Input.mousePosition;
        //v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //  v2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //  Debug.Log(v2);
        //  go.transform.position = v3;
        //v3.z += 5f;
        // v3.y += 2f;
        // go.transform.LookAt(v3);
        //  }
    }*/

    //老師版
    /*
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 100, 30), "ChangeTrigger")) //(左,上,寬,高)
        {
            m_isTrigger = !m_isTrigger;
        }

        GUI.Label(new Rect(140, 30, 400, 60), "<color=white><size=40>" + "Trigger:" + m_isTrigger + "</size></color>");

        GUI.Label(new Rect(400, 35, 400, 60), "<color=white><size=30>" + "Bullet Count:" + bulletList.Count + "</size></color>");
        if (bulletList.Count == max)
        {
            GUI.Label(new Rect(610, 35, 400, 60), "<color=white><size=30>" + "(MAX)" + "</size></color>");
        }
    }*/
    /*
    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Rigidbody rigidbody = m_shootingObject.GetComponent<Rigidbody>();

            if (rigidbody)
            {
                GameObject go = Instantiate(m_shootingObject) as GameObject;
                bulletList.Add(go);
                if (m_isBullet)
                    go.AddComponent<Bullet>();
                if (bulletList.Count > max)
                    GameObject temp = bulletList[0];
                    bulletList.Remove(temp);
                    Destroy(temp.gameObject);
                }
                Vector3 mousePositionOnNearPlane = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
                Vector3 v3 = go.transform.position;

                go.transform.LookAt(mousePositionOnNearPlane);
                go.transform.position = mousePositionOnNearPlane;

                go.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(mousePositionOnNearPlane - Camera.main.transform.position) * m_shootingForce);//終點位置
            }
            else
            {
                Debug.LogError("Shooting object need to assign Rigidbody component.");
            }
        }
    }*/
    //鎮瑋版

    private IEnumerator cdTime(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = true;
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    private void OnGUI()
    {
        if (currentStyle == null)
        {
            currentStyle = new GUIStyle(GUI.skin.box);
            currentStyle.normal.background = MakeTex(2, 2, new Color(0f, 1f, 0f, 0.5f));
        }
        if (isReady)
            GUI.Box(new Rect(75, 100, (powerTotal) * 100, 49), "", currentStyle);
    }

    // Use this for initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (!m_shootingObject)
            Debug.LogError("You need to assign a shooting object.");
        if (!m_shootingArrow)
            Debug.LogError("You need to assign a shooting arrow.");
    }

    // Update is called once per frame
    private void Update()
    {
        if (timeList.Count > 0)
            if ((bulletList.Count > max) || (Time.time - timeList[0] >= 10))
            {
                GameObject temp2 = arrowList[0];
                GameObject temp = bulletList[0];
                arrowList.Remove(temp2);
                bulletList.Remove(temp);
                timeList.Remove(timeList[0]);
                Destroy(temp2.gameObject);
                Destroy(temp.gameObject);
            }
        if (isAttack)
            if (Input.GetMouseButtonDown(1))
            {
                powerStart = Time.time;
                powerTotal = 0;
                isReady = true;
            }
            else if (Input.GetMouseButtonUp(1) && isReady)
            {
                animator.SetTrigger("Attack");
                StartCoroutine(waitForShoot(0.4f));
                isAttack = false;
                isReady = false;
                StartCoroutine(cdTime(1f));
            }
            else if (Input.GetMouseButton(1) && isReady)
                if (powerTotal >= 3)
                    powerTotal = 3;
                else
                    powerTotal = Time.time - powerStart;
    }

    private IEnumerator waitForShoot(float time)
    {
        yield return new WaitForSeconds(time);
        Rigidbody rigidbody = m_shootingObject.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            GameObject go = Instantiate(m_shootingObject) as GameObject;
            GameObject arrow = Instantiate(m_shootingArrow) as GameObject;
            float goTime = Time.time;
            bulletList.Add(go);
            arrowList.Add(arrow);
            timeList.Add(goTime);
            if (m_isBullet)
                go.AddComponent<Bullet>();
            go.transform.position = m_bowPosition.position;//從bow射出
            arrow.GetComponent<ArrowRotation>().target = go;
            Vector3 positionInTheAir;
            if (m_isFirstPerson)
            {
                positionInTheAir = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, Camera.main.nearClipPlane));//畫面中央
                go.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(positionInTheAir - Camera.main.transform.position) * m_shootingForce * (1 + powerTotal));//終點方向*力量
            }
            else
            {
                positionInTheAir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_bowPosition.position.z - Camera.main.transform.position.z));//鼠標位置
                go.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(positionInTheAir - m_bowPosition.position) * m_shootingForce * (1 + powerTotal));//終點方向*力量
            }
        }
        else
            Debug.LogError("Shooting object need to assign Rigidbody component.");
    }
}