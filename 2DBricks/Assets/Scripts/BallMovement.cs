using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
	public AudioClip audioclip;
	public AudioSource audiosource;
    public float velX = 0;
    public float velY = 200;
    public Rigidbody2D rb;
    bool startGame = false;
    float xRotation = 5.0f;
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
		audiosource = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();
        line.SetPosition(1, new Vector3(2, 0));
        line.transform.Rotate(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        int zAngle = (int )transform.localEulerAngles.z;        
        if (Input.GetKey(KeyCode.RightArrow))
        {   
            line.transform.Rotate(0, 0, -xRotation);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            line.transform.Rotate(0, 0, xRotation);
        }
        if (Input.GetKeyUp(KeyCode.Space) && startGame == false)
        {
			audiosource.Play();
            if ( zAngle > 180)
            {
                velY = -300;
            }
            switch (zAngle)
            {
                case 0:
                    velX = 300;
                    velY = 0;
                    break;
                case 90:
                    velX = 0;
                    break;
                case 180:
                    velY = 0;
                    break;
                case 270:
                    velX = 0;
                    break;
                default:
                    velX = velY / Mathf.Tan(zAngle * Mathf.Deg2Rad);
                    break;
            }
            rb.AddForce(new Vector2(velX, velY));
            velY = 400;
            startGame = true;
            line.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            transform.position = new Vector2(0f, 0f);
            rb.velocity = new Vector2(0, 0);
            startGame = false;
            line.enabled = true;
        }
    }
}
