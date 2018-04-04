using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count : MonoBehaviour {

    public int count = 0;
    private string headTag = "Head";
    private string bodyTag = "Body";
    private string arrowName = "Shoot(Clone)";

    // Use this for initialization
    void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " hit " + collision.gameObject.name);
        if (collision.gameObject.name == arrowName)
            if (tag == headTag)
                count += 10;
            else if (tag == bodyTag)
                count += 1;
    }
}
