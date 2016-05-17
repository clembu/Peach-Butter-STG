using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;

    Rigidbody2D rb2d;

    public Transform shotSpawn1;

    public Transform shotSpawn2;

    public GameObject shot;

    public float fireRate = 0.5f;

    float nextFire = 0.0f;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
    void Update()
    {
        bool input = Input.GetButton("Fire1");

        if (input && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation);
            Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
        }
    }

	// Update is called once per frame
	void FixedUpdate () {

        Vector2 input = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );

        Vector2 dir = input.normalized;

        rb2d.velocity = dir * speed;        
	}
}
