using UnityEngine;
using System.Collections;

public class pickupBehavior : MonoBehaviour {

	public GameObject player;
	public float speed;

	private Transform transform;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2 (0.0f, -speed*Time.deltaTime));
	}

	void OnTriggerEnter2D(Collider2D other){
		//if the GO collided is the player
		if( other.gameObject.name == "player" ){
			//increment its power by 1
			//destroy the object
			PlayerController playerControl = other.gameObject.GetComponent<PlayerController>();
			playerControl.power = playerControl.power + 1;
			Destroy (gameObject);	
		} 
	}
}
