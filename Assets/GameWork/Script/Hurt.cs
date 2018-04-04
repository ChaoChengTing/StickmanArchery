using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hurt : MonoBehaviour
{
    public GameObject ChName;
    public GameObject head, body;
    public bool isDead = false;
    private Animator animator;//(1)
    private GUIStyle currentStyle = null;
    private int hitCount = 0;

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
            currentStyle.normal.background = MakeTex(2, 2, new Color(1f, 0f, 0f, 1f));
        }
        int blood = 100 - hitCount;
        if (blood <= 0) blood = 0;
        string name = ChName.transform.name + " blood " + blood;
        string win;
        if (ChName.name == "Player1")
        {
            GUI.Label(new Rect(1500, 830, 1000, 100), "<size=45>" + name + "</size>");
            GUI.Box(new Rect(1500, 900, (100 - hitCount) * 3.6f, 50), "", currentStyle);
        }

        if (ChName.name == "Player2")
        {
            GUI.Label(new Rect(75, 830, 1000, 100), "<size=45>" + name + "</size>");
            GUI.Box(new Rect(75, 900, blood * 3.6f, 50), "", currentStyle);
        }

        if (blood <= 0)
        {
            GUI.Box(new Rect(0, 150, 1920, 800), "");
            GUI.Label(new Rect(200, 240, 2000, 500), "<size=250>" + "GAME OVER" + "</size>");
            if (ChName.name == "Player2")
            {
                win = "Player1 is Winner";
                GUI.Label(new Rect(203, 520, 2000, 500), "<size=192>" + win + "</size>");
            }
            if (ChName.name == "Player1")
            {
                win = "Player2 is Winner";
                GUI.Label(new Rect(203, 520, 2000, 500), "<size=192>" + win + "</size>");
            }
            if (GUI.Button(new Rect(810, 760, 300, 120), "<color=white><size=50>" + "MENU" + "</size></color>"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    // Use this for initialization
    private void Start()
    {
        hitCount = 0;
        animator = GetComponent<Animator>();
        if (!head.GetComponent<Count>())
            head.AddComponent<Count>();
        if (!body.GetComponent<Count>())
            body.AddComponent<Count>();
        if (!head.GetComponent<Collider>())
        {
            SphereCollider headTemp = head.AddComponent<SphereCollider>();
            headTemp.center = new Vector3(0.03f, 0f, -0.4f);
            headTemp.radius = 0.5f;
        }
        if (!body.GetComponent<Collider>())
        {
            CapsuleCollider bodyTemp = body.AddComponent<CapsuleCollider>();
            bodyTemp.center = new Vector3(0f, -1.25f, -0.5f);
            bodyTemp.radius = 0.4f;
            bodyTemp.height = 3.75f;
            bodyTemp.direction = 1;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (hitCount != head.GetComponent<Count>().count + body.GetComponent<Count>().count)
        {
            hitCount = head.GetComponent<Count>().count + body.GetComponent<Count>().count;
            Debug.Log(ChName.transform.name + " blood " + (100 - hitCount));
        }
        if (hitCount >= 100)
        {
            isDead = true;
            animator.SetTrigger("Dead");
            GetComponent<Shoot>().enabled = false;
            GetComponent<ComAI>().enabled = false;
        }
    }
}