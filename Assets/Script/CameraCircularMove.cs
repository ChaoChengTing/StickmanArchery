using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCircularMove : MonoBehaviour
{
    public float x = 0, y = 0;
    public float xSpeed = 200;
    public float ySpeed = 200;
    private float x0 = 0;

    //當前攝影機與主角距離
    //private Quaternion rotatonEuler;
    private void Start()
    {
        x0 = x;
    }

    private void Update()
    {
        //讀取滑鼠的XY
        x += Input.GetAxis("Mouse X") * Time.deltaTime * xSpeed;
        y -= Input.GetAxis("Mouse Y") * Time.deltaTime * ySpeed;
        if (x0 - x > 90) x = x0 - 90;
        else if (x0 - x < -90) x = x0 + 90;
        if (y > 45) y = 45;
        else if (y < -45) y = -45;

        //運算攝影機座標、旋轉
        //rotatonEuler = Quaternion.Euler(x, y, 0);

        //應用
        //transform.rotation = rotatonEuler;
        transform.rotation = Quaternion.Euler(y, x, 0);
    }
}