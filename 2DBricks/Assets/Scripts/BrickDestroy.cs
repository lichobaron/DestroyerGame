using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D c2d)
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }
}
