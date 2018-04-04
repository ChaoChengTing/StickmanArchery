using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartBtn : MonoBehaviour
{
    //要移除的canvas
    public GameObject canvas;

    private void ClickEvent()
    {
        //刪掉canvas
        Destroy(canvas);
    }

    private void Start()
    {
        //按下按鈕後，呼叫ClickEvent()
        GetComponent<Button>().onClick.AddListener(() =>
        {
            ClickEvent();
        });
    }
}