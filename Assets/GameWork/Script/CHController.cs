using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHController : MonoBehaviour
{
    private Animator animator;//(1)
    private bool isAttack = true;
    private bool isReady = true;

    private void Start()
    {//(3)
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttack)
            if (Input.GetMouseButtonDown(1))
                isReady = true;
            else if (Input.GetMouseButtonUp(1) && isReady)
            {
                animator.SetTrigger("Attack");
                isAttack = false;
                isReady = false;
                StartCoroutine(cdTime(1f));
            }
    }

    IEnumerator cdTime(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = true;
    }
}