using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bricks : MonoBehaviour
{
	public AudioClip DestroyClip;
	public AudioSource Source;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}


	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "ball")
		{
			Source = GameObject.FindGameObjectWithTag("Fire").GetComponent<AudioSource>();
			Source.clip = DestroyClip;
			Destroy(gameObject);
			Source.Play();
		}

	}
}
