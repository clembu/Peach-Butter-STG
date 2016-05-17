using UnityEngine;
using System.Collections;

public class Automove : MonoBehaviour
{

    public float speed;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Update()
    {
        rb2d.velocity = transform.up * speed;
    }
    
}
