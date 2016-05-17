using UnityEngine;
using System.Collections;

public class BulletDestroyer : MonoBehaviour {

	void OnEnable()
    {
        Invoke("Destroy", 1f);
    }
	
	// Update is called once per frame
	void Destroy () {
        gameObject.SetActive(false);
	}

    void OnDisable()
    {
        CancelInvoke();
    }
}
