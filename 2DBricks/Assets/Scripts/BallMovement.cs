using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

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
    StreamWriter sw;
    // Start is called before the first frame update
    void Start()
    {
		audiosource = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();
        line.SetPosition(1, new Vector3(2, 0));
        line.transform.Rotate(0, 0, 90);
        sw = new StreamWriter("C:\\Users\\anfec\\Documents\\GitHub\\KMeansHand\\muestreo.txt");

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
        double sen1 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131",0);
        double sen2 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 1);
        double sen3 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 2);
        double sen4 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 3);
        double sen5 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 4);
        double sen6 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 5);
        double sen7 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 6);
        double sen8 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 7);
        double sen9 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 8);
        double sen10 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 9);
        double sen11 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 10);
        double sen12 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 11);
        double sen13 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 12);
        double sen14 = VRPN.vrpnAnalog("Glove14Right@10.3.136.131", 13);

        try
        {
            sw.WriteLine(Math.Round(sen1,3)+", "+ Math.Round(sen2, 3)+", "+Math.Round(sen3, 3) + ", "
                + Math.Round(sen4, 3) + ", " + Math.Round(sen5, 3) + ", " + Math.Round(sen6, 3) + ", "
                + Math.Round(sen7, 3) + ", " + Math.Round(sen8, 3) + ", " + Math.Round(sen9, 3) + ", "
                + Math.Round(sen10, 3) + ", " + Math.Round(sen11, 3) + ", " + Math.Round(sen12, 3) + ", "
                + Math.Round(sen13, 3) + ", " + Math.Round(sen14, 3));
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }

    }

    private void OnApplicationQuit()
    {
        sw.Close();
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


