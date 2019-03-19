using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
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

    private string path = "/Users/licho/Downloads/";
    private float nextActionTime = 0.0f;
    public float period = 0.1f;


    private string[] filenames;
    private double[][][] means;
    private int[] info;
    private double[] tuple;
       

    // Start is called before the first frame update
    void Start()
    {
        filenames = new string[5];
        //TODO
        filenames[0] = path + "finger1.txt";
        filenames[1] = path + "finger2.txt";
        filenames[2] = path + "finger3.txt";
        filenames[3] = path + "finger4.txt";
        filenames[4] = path + "finger5.txt";
        tuple = new double[14];
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
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            tuple[0] = VRPN.vrpnAnalog("Glove14Right@localhost", 0);
            tuple[1] = VRPN.vrpnAnalog("Glove14Right@localhost", 1);
            tuple[2] = VRPN.vrpnAnalog("Glove14Right@localhost", 2);
            tuple[3] = VRPN.vrpnAnalog("Glove14Right@localhost", 3);
            tuple[4] = VRPN.vrpnAnalog("Glove14Right@localhost", 4);
            tuple[5] = VRPN.vrpnAnalog("Glove14Right@localhost", 5);
            tuple[6] = VRPN.vrpnAnalog("Glove14Right@localhost", 6);
            tuple[7] = VRPN.vrpnAnalog("Glove14Right@localhost", 7);
            tuple[8] = VRPN.vrpnAnalog("Glove14Right@localhost", 8);
            tuple[9] = VRPN.vrpnAnalog("Glove14Right@localhost", 9);
            tuple[10] = VRPN.vrpnAnalog("Glove14Right@localhost", 10);
            tuple[11] = VRPN.vrpnAnalog("Glove14Right@localhost", 11);
            tuple[12] = VRPN.vrpnAnalog("Glove14Right@localhost", 12);
            tuple[13] = VRPN.vrpnAnalog("Glove14Right@localhost", 13);

            //Debug.Log(Math.Round(sen14,3));
            means = getMeansFromFile(filenames, 2); //TODO
            info = TestFingers(tuple, means);

            //Debug.Log(tuple[6]);
            //Debug.Log(tuple[7]);
            /*Debug.Log("Glove raw = " + String.Join(", ",
                new List<double>(tuple)
                .ConvertAll(i => i.ToString())
                .ToArray()));*/

            Debug.Log("Glove = " + String.Join(", ",
                new List<int>(info)
                .ConvertAll(i => i.ToString())
                .ToArray()));
        }

    



    }

    private void OnApplicationQuit()
    {
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

    public static double[] getFingersTuple(double[] tuple, int finger)
    {
        double[] r = new double[2];
        switch (finger)
        {
            case 1:
                r[0] = tuple[0];
                r[1] = tuple[1];
                break;
            case 2:
                r[0] = tuple[3];
                r[1] = tuple[4];
                break;
            case 3:
                r[0] = tuple[6];
                r[1] = tuple[7];
                break;
            case 4:
                r[0] = tuple[9];
                r[1] = tuple[10];
                break;
            case 5:
                r[0] = tuple[12];
                r[1] = tuple[13];
                break;
            default:
                r[0] = -1;
                r[1] = -1;
                break;
        }
        return r;
    }

    public static double[][][] getMeansFromFile(string[] fileNames, int numK)
    {
        double[][][] means = new double[5][][];

        for (int i = 0; i < fileNames.Length; i++)
        {
            means[i] = new double[numK][];
            string filename = fileNames[i];
            String line; try
            {
                StreamReader sr = new StreamReader(filename);
                line = sr.ReadLine();

                int j = 0;
                while (line != null)
                {
                    Regex reg = new Regex(@"([-+]?[0-9]*\.?[0-9]+)");
                    int tam = 0;
                    foreach (Match match in reg.Matches(line))
                    {
                        tam++;
                    }
                    means[i][j] = new double[tam];

                    int k = 0;
                    foreach (Match match in reg.Matches(line))
                    {
                        means[i][j][k] = double.Parse(match.Value, CultureInfo.InvariantCulture.NumberFormat);
                        k++;
                    }

                    j++;
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block files.");
            }
        }

        return means;
    }

    public static int[] TestFingers(double[] tuple, double[][][] means)
    {

        int[] r = new int[5];

        for (int i = 0; i < 5; i++) //num dedos
        {
            int minIndex = 0;
            double min = Distance(getFingersTuple(tuple, i), means[i][0]);
            for (int j = 1; j < means[i].Length; j++)
            {
                if (Distance(getFingersTuple(tuple, i), means[i][j]) < min)
                {
                    minIndex = j;
                }
            }
            r[i] = translateSensor(minIndex);
        }
        return r;
    }

    public static int translateSensor(int num)
    {
        //Debug.Log(num);
        int r = 99;
        if (num == 0)
        {
            r = 0;
        }
        else if (num == 1) //TODO
        {
            r = 1;
        }
        else if (num == 2)
        {
            r = -1;
        }
        return r;
    }

    private static double Distance(double[] tuple, double[] mean)
    {
        double sumSquaredDiffs = 0.0;
        for (int j = 0; j < tuple.Length; ++j)
            sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
        return Math.Sqrt(sumSquaredDiffs);

    }

}


