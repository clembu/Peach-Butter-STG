using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;

    Rigidbody2D rb2d;

    public Transform shotSpawn1;

    public Transform shotSpawn2;

    public Transform spiralSpawn;
    
    public Animator anim;

    public CurveOfDeath rod;
        
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        
	}
	
    void Update()
    {
        bool input = Input.GetButton("Fire1");

        shotSpawn1.GetComponent<FireBullets>().Fire = input;
        shotSpawn2.GetComponent<FireBullets>().Fire = input;

        bool skillinput = Input.GetButtonDown("Fire2");

        if (skillinput)
        {
            anim.SetTrigger("SkillActivated");
        }

        bool ringinput = Input.GetButtonDown("Fire3");

        if(ringinput)
        {
            rod.Fire();
        }
    }

	// Update is called once per frame
	void FixedUpdate () {

        Vector2 input = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );

        Vector2 dir = input.normalized;

        rb2d.velocity = dir * speed;        
	}
}
