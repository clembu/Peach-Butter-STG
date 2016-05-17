using UnityEngine;
using System.Collections;

public class FireBullets : MonoBehaviour {

    public bool Fire = false;

    public float bulletSpeed = 20f;

    public GameObject bullet;

    public BulletsPool pool;

    public float fireRate = 0.5f;

    float nextShot = 0.0f;

    	
	// Update is called once per frame
	void Update () {

        if(Fire && Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;

            GameObject b = pool.GetBullet();
            if (b == null) return;
            
            b.transform.position = transform.position;
            b.transform.rotation = transform.rotation;
            b.GetComponent<Automove>().speed = bulletSpeed;
            b.SetActive(true);
        }
	
	}
}
