using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;

    Rigidbody2D rb2d;

	public float power;

    public Emitter emitter;

    public Pattern Skill1;
    
    public Animator Skill2;
        
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            emitter.enabled = true;
        }
        if(Input.GetButtonUp("Fire1"))
        {
            emitter.enabled = false;
        }

        bool ringInput = Input.GetButtonDown("Fire2");

        if(ringInput)
        {
            Skill1.Activate();
        }

        bool spiralInput = Input.GetButtonDown("Fire3");

        if(spiralInput)
        {
            //rod.Fire();
            Skill2.SetTrigger("Activate");
        }
    }

	// Update is called once per frame
	void FixedUpdate () {

        Vector2 input = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );

        Vector2 dir = input.normalized;

        rb2d.velocity = dir * speed;
	}
}
