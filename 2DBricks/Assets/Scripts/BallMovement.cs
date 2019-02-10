using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float force;
    public Rigidbody2D rb;
    bool startGame = false;
    float xRotation = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log(rb.rotation.ToString());
            rb.rotation += xRotation;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            xRotation -= Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.right);
        }
        if ( Input.GetKeyUp(KeyCode.Space) && startGame == false )
        {            
            rb.AddForce(new Vector2(force, force));
            startGame = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            transform.position = new Vector2(0f, -4.26f);
            rb.velocity = new Vector2(0, 0);
            startGame = false;
        }
    }
}
