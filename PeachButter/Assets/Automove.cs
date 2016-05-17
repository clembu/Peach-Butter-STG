using UnityEngine;
using System.Collections;

public class Automove : MonoBehaviour
{

    public float speed;

    // Use this for initialization
    void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        rb2d.velocity = transform.up * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
