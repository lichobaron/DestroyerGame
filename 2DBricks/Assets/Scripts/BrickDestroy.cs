using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDestroy : MonoBehaviour
{
    public AudioSource aS;
    public AudioClip destroy;
    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D c2d)
    {
        if (c2d.gameObject.tag == "Ball")
        {
            aS.clip = destroy;
            aS.Play();
            Destroy(gameObject);
        }
    }
}
