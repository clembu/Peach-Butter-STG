using UnityEngine;
using System.Collections;

[AddComponentMenu("Danmaku/Auto Movements/Linear")]
public class Straight : MonoBehaviour {

    public Transform lookAtTarget;

    public float distanceToGo;

    public Vector3 direction;

    public bool doReturn = false;

    public int loops = 0;

    public float speed;

    bool isReturning = false;

    int currentLoop = 0;

    Vector3 originalPosition;

    float distanceGone = 0.0f;

    Vector3 currentDir;

    void OnEnable()
    {
        originalPosition = transform.position;
        currentDir = direction.normalized;

        Debug.Log("Movement enabled. Translating over " + distanceToGo + " towards " + currentDir);
    }

    void Update()
    {
        transform.Translate(currentDir*speed*Time.deltaTime);

        if (isReturning) distanceGone -= Time.deltaTime * speed;
        else distanceGone += Time.deltaTime * speed;

        if (lookAtTarget != null) transform.LookAt(lookAtTarget);

        if (distanceGone >= distanceToGo)
        {
            if (doReturn)
            {
                isReturning = true;
                currentDir = -currentDir;
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
        else if (distanceGone <= 0)
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
        distanceGone = 0.0f;
        isReturning = false;
        currentDir = direction.normalized;
    }
    

}
