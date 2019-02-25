using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMove : MonoBehaviour {

	public float force;
	public Transform ballChecker;
	public AudioClip shoot;
	public AudioSource source;

	public float velX = 0;
	public float velY = 200;
	Vector2 position;
	public float speed;
	public LineRenderer line;

	// Use this for initialization
	void Start () {
		transform.position = new Vector2(0,-2.34f);
		line = GetComponent<LineRenderer>();
		line.SetPosition(1, new Vector2(0, -2.34f));
		position = line.transform.position;
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space))
		{
			source.clip = shoot;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 0.5f) * Time.deltaTime * force);
			source.Play();
			line.enabled = false;
		}

		if (transform.position.y < ballChecker.transform.position.y)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
