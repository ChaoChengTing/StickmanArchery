using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<arrow>().speed = 0;
            // GetComponent<Rigidbody>().velocity = Vector3.zero;    //AddForce用
            // GetComponent<Rigidbody>().
            // GetComponent<arrow>().enabled = false;
            Debug.Log(collision.gameObject.name);
        }
    }

    // Use this for initialization
    private void Start()
    {
        speed = 5f;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}