using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletsPool : MonoBehaviour {

    public GameObject bulletPrefab;
    
    public int pooledAmount = 50;
    List<GameObject> pool;

	// Use this for initialization
	void Start () {
        pool = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject go = (GameObject)Instantiate(bulletPrefab);
            go.SetActive(false);
            pool.Add(go);
        }
	}

    public GameObject Bullet
    {
        get
        {
            if (pool == null) return null;

            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy) return pool[i];
            }

            return null;
        }
    }
	
	public GameObject GetBullet()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }
}
