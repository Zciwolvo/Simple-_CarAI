using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedyboimovement : MonoBehaviour
{

    private Vector2 originalPos;
    private Vector3 originalRot;
    public static int count;
    public static int Coincount;
    public float power = 30;
    public float maxspeed = 60;
    public float turnpower = 3;
    public float friction = 3;
    public Vector2 curspeed;
    Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        originalPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        originalRot = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
    }


    void FixedUpdate()
    {
        curspeed = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y);

        if (curspeed.magnitude > maxspeed)
        {
            curspeed = curspeed.normalized;
            curspeed *= maxspeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody2D.AddForce(transform.up * power);
            rigidbody2D.drag = friction;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody2D.AddForce(-(transform.up) * (power / 2));
            rigidbody2D.drag = friction;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * turnpower);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * -turnpower);
        }

        noGas();

    }

    void noGas()
    {
        bool gas;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            gas = true;
        }
        else
        {
            gas = false;
        }

        if (!gas)
        {
            rigidbody2D.drag = friction * 2;
        }
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "mapend")
        {
            gameObject.transform.position = originalPos;
            gameObject.transform.eulerAngles = originalRot;
            rigidbody2D.freezeRotation = true;
            rigidbody2D.freezeRotation = false;
            count += 1;
        }
        if (c.gameObject.tag == "coin")
        {
            gameObject.transform.position = originalPos;
            gameObject.transform.eulerAngles = originalRot;
            rigidbody2D.freezeRotation = true;
            rigidbody2D.freezeRotation = false;
            Coincount += 1;
            count += 1;

        }


    }
}
