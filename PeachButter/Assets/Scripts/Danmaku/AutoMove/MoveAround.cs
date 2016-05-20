using UnityEngine;
using System.Collections;

public class MoveAround : MonoBehaviour {

    public Transform lookAtTarget;

    public Transform moveAroundTarget;

    public float speed;

    public int loops;

    public bool doReturn;

    public bool cw;

    public float angleToGo;

    Quaternion originalRotation;
    Vector3 originalPosition;

    int currentLoop = 0;

    bool isReturning = false;

    float angleGone = 0.0f;

    int dir = 1;

    void OnEnable()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        if (cw) dir = -1;
    }

    void Update()
    {
        Debug.Log("Moving "+ dir * Time.deltaTime * speed);
        //frameIndependant
        transform.RotateAround(moveAroundTarget.transform.position, -transform.forward, dir * Time.deltaTime * speed);
        if (isReturning) angleGone -= Time.deltaTime * speed;
        else angleGone += Time.deltaTime * speed;

        if (lookAtTarget != null) transform.LookAt(lookAtTarget);
        else transform.rotation = originalRotation;

        if (angleGone >= angleToGo)
        {
            if (doReturn)
            {
                isReturning = true;
                dir = -dir;
            }
            else if (currentLoop < loops)
            {
                Restart();
                currentLoop++;
            }
            else
            {
                enabled = false;
            }
        }
        else if (angleGone <= 0)
        {
            if (currentLoop < loops)
            {
                Restart();
                currentLoop++;
            }
            else
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
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        isReturning = false;
        angleGone = 0.0f;
    }
}
