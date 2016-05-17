using UnityEngine;
using System.Collections;

public class CurveOfDeath : MonoBehaviour {

    public int amount;

    public float bulletSpeed;

    public GameObject bullet;

    public BulletsPool pool;
    
    public void Fire()
    {
        Vector3[] path = iTweenPath.GetPath("ROD");
        for (int i = 0; i < amount; i++)
        {
            GameObject b = pool.GetBullet();
            if (b == null) return;

            float prc = i / (float)amount;
            Vector3 p = iTween.PointOnPath(path, prc);
            b.transform.position = p;
            Vector3 outw = p*2; outw.z = p.z;

            b.transform.up = outw.normalized;

            b.SetActive(true);
        }
    }
}
