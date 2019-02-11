using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	Vector2 position;
	public float speed;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float moveX = Input.GetAxis("Horizontal");
		position.x += moveX * Time.deltaTime  * speed;
		position.x = Mathf.Clamp(position.x, -5.87f, 5.87f);
		transform.position = position;			
	}
}
