using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot_ThirdPerson : MonoBehaviour
{
    public bool m_isBullet = false;
    public GameObject m_shootingArrow;
    public float m_shootingForce = 1000;
    public GameObject m_shootingObject;
    public Vector3 m_shootPosition = new Vector3();
    public int max = 10;
    private List<GameObject> arrowList = new List<GameObject>();
    private List<GameObject> bulletList = new List<GameObject>();
    private bool isAttack = true;
    private Vector3 mousePositionOnSamePlane = new Vector3();
    private float power = 0;
    private List<float> timeList = new List<float>();

    private IEnumerator cdTime(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = true;
    }

    // Use this for initialization
    private void Start()
    {
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
        if (Input.GetMouseButtonDown(1) && isAttack)
            power = Time.time;
        else if (Input.GetMouseButtonUp(1) && isAttack)
        {
            power = 1 + Time.time - power;
            StartCoroutine(waitForShoot(0.4f));
            isAttack = false;
            StartCoroutine(cdTime(1f));
        }
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
            mousePositionOnSamePlane = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_shootPosition.z - Camera.main.transform.position.z));//鼠標位置
            go.transform.position = m_shootPosition;//從start
            go.transform.LookAt(mousePositionOnSamePlane);//往射出方向看
            arrow.GetComponent<ArrowRotation>().target = go;
            go.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(mousePositionOnSamePlane - m_shootPosition) * m_shootingForce * power);//終點方向*力量
        }
        else
            Debug.LogError("Shooting object need to assign Rigidbody component.");
    }
}