using UnityEngine;
using System.Collections;

[AddComponentMenu("Danmaku/Bullet Types/Straight Shot")]
public class StraightShot : MonoBehaviour {

    Bullet bullet;
    void Start()
    {
        bullet = GetComponent<Bullet>();
    }
        
	// Update is called once per frame
	void Update ()
    {

        bullet.rb2d.velocity = transform.up * bullet.speed;

	}
}
