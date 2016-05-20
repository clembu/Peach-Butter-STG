using UnityEngine;
using System.Collections;

[AddComponentMenu("Danmaku/Bullet")]
public class Bullet : MonoBehaviour {

    public float speed;

    [HideInInspector]
    public Rigidbody2D rb2d;

    public float lifeTime = 0.0f;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void OnEnable()
    {
	    if(lifeTime > 0.0f)
        {
            Invoke("Destroy", lifeTime);
        }
	}
	
    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
