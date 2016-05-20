using UnityEngine;
using System.Collections;

[AddComponentMenu("Danmaku/Auto Movements/Rotation")]
public class Rotate : MonoBehaviour {

    public float angSpeed;

    public float angleToGo;

    public bool cw;

    public bool doReturn = false;
    
    public int loops = 0;

    Quaternion originalRotation;

    int currentLoop = 0;

    bool isReturning = false;

    float angleGone = 0.0f;

    int dir = 1;

    void OnEnable()
    {
        originalRotation = transform.rotation;
        if (cw) dir = -1;
    }

    void Update()
    {
        //frameIndependant
        transform.Rotate(transform.forward, Time.deltaTime * angSpeed * dir);
        if (isReturning) angleGone -= Time.deltaTime * angSpeed;
        else angleGone += Time.deltaTime * angSpeed;

        if(angleGone >= angleToGo)
        {
            if (doReturn)
            {
                isReturning = true;
                dir = -dir;
            } else if(currentLoop < loops)
            {
                Restart();
                currentLoop++;
            } else
            {
                enabled = false;
            }
        } else if(angleGone <= 0)
        {
            if(currentLoop < loops)
            {
                Restart();
                currentLoop++;
            } else
            {
                enabled = false;
            }
        }
    }

    void OnDisable()
    {
        Restart();
    }

    void Restart()
    {
        isReturning = false;
        transform.rotation = originalRotation;
        if (cw) dir = -1; else dir = 1;
        angleGone = 0.0f;
    }
}
