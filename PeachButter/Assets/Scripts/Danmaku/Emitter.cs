using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour {

    public BulletsPool bulletPool;

    public float speed;
    public float lifeTime;

    public float fireRate = 0.0f;

    public bool isEmitting = false;

    float nextEmit = 0.0f;

    void Update()
    {
        if(isEmitting && Time.time > nextEmit)
        {
            nextEmit = Time.time + fireRate;
            Emit();
        }
    }

    public void Emit()
    {
        GameObject go = bulletPool.Bullet;
        if (go == null) return;
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        go.GetComponent<Bullet>().speed = speed;
        go.GetComponent<Bullet>().lifeTime = lifeTime;
        go.SetActive(true);
    }
    
}
