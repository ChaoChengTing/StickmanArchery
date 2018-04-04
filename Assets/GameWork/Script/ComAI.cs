using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComAI : MonoBehaviour
{
    public Transform bowPosition;

    //public GameObject cam;
    public GameObject enemy;

    public bool isBullet = true;
    public GameObject m_shootingArrow;
    public GameObject m_shootingObject;
    public int max = 10;
    public int shootingForce = 1000;
    private Animator animator;
    private List<GameObject> arrowList = new List<GameObject>();
    private List<GameObject> bulletList = new List<GameObject>();
    private bool isAttack = true;
    private List<float> timeList = new List<float>();
    //(1)

    private IEnumerator cdTime(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = true;
    }

    // Use this for initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
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
        {
            animator.SetTrigger("Attack");
            StartCoroutine(waitForShoot(0.4f));
            isAttack = false;
            StartCoroutine(cdTime(Random.Range(1f, 2f)));
        }
    }

    private IEnumerator waitForShoot(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject go = Instantiate(m_shootingObject) as GameObject;
        GameObject arrow = Instantiate(m_shootingArrow) as GameObject;
        float goTime = Time.time;
        bulletList.Add(go);
        arrowList.Add(arrow);
        timeList.Add(goTime);
        if (isBullet)
            go.AddComponent<Bullet>();
        go.transform.position = bowPosition.position;
        if (enemy.GetComponent<Hurt>())
            go.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(enemy.GetComponent<Hurt>().head.transform.position - bowPosition.position) * shootingForce);
        arrow.GetComponent<ArrowRotation>().target = go;
    }
}