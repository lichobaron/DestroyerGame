using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float velX = 0;
    public float velY = 200;
    public Rigidbody2D rb;
    bool startGame = false;
    float xRotation = 5.0f;
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(1, new Vector3(0, 2));
    }

    // Update is called once per frame
    void Update()
    {
        float zAngle = transform.localEulerAngles.z;
        if ((zAngle != 90) && (zAngle != 270))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                line.transform.Rotate(0, 0, -xRotation);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (zAngle + xRotation == 360)
                {
                    line.transform.eulerAngles = new Vector3(0,0,0);
                }
                else
                {
                    line.transform.Rotate(0, 0, xRotation);
                }
            }
        }
        else
        {
            if (zAngle == 270)
            {
                line.transform.eulerAngles = new Vector3(0, 0, 275);
            }
            if (zAngle == 90)
            {
                line.transform.eulerAngles = new Vector3(0, 0, 85);
            }
        }        
        if (Input.GetKeyUp(KeyCode.Space) && startGame == false)
        {
            GetComponent<AudioSource>().Play();
            if (zAngle > 270)
            {
                zAngle -= 270;
            }
            else if (zAngle > 5)
            {
                zAngle += 90;
            }
            else if (zAngle == 0 || zAngle == 360)
            {
                zAngle = 0;
            }
            if (Mathf.Tan(zAngle) == 0)
            {
                velX = 0;
            }
            else
            {                
                velX = velY / Mathf.Tan(zAngle * Mathf.Deg2Rad);
            }             
            rb.AddForce(new Vector2(velX, velY));
            startGame = true;
            line.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            transform.position = new Vector2(0f, -9f);
            rb.velocity = new Vector2(0, 0);
            startGame = false;
            line.enabled = true;
        }
    }
}
