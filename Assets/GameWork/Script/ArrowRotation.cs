using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    public GameObject target;

    // LateUpdate is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            if (target.GetComponent<Bullet>())
                if (target.GetComponent<Bullet>().m_isMoving)
                    transform.LookAt(target.transform);
            transform.position = target.transform.position;
        }
    }
}
