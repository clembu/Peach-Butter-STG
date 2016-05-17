using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;

    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 input = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );

        Vector2 dir = input.normalized;

        rb2d.velocity = dir * speed;        
	}
}
