using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReboot : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            transform.position = new Vector2(0f, 7399998f);

        }
    }
}
