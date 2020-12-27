using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class CarAgent : Agent
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


    // Start is called before the first frame update
    public override void Initialize()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        originalPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        originalRot = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
    }


    public override void OnActionReceived(float[] vectorAction)
    {
        var forward = Mathf.FloorToInt(vectorAction[0]);
        var turnLeft = Mathf.FloorToInt(vectorAction[1]);
        var turnRight = Mathf.FloorToInt(vectorAction[2]);
        if (forward == 0)
        {
            MoveForward();
        }
        if (turnLeft == 0)
        {
            MoveLeft();
        }
        if (turnRight == 0)
        {
            MoveRight();
        }
    }
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        actionsOut[1] = 0;
        actionsOut[2] = 0;
        if (Input.GetKey(KeyCode.W))
        {
            actionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[2] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[1] = 1;
        }


        noGas();
    }
    public override void OnEpisodeBegin()
    {
        Reset();
    }

    void MoveForward()
    {

        rigidbody2D.AddForce(transform.up * power);
        rigidbody2D.drag = friction;
        noGas();

    }
    void MoveRight()
    {
        transform.Rotate(Vector3.forward * turnpower);
    }
    void MoveLeft()
    {
        transform.Rotate(Vector3.forward * -turnpower);
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





    
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "coin")
        {
            Reset();
            AddReward(10f);
            Coincount += 1;
            count += 1;
            Debug.Log(GetCumulativeReward());
            EndEpisode();
        }
        if (c.gameObject.tag == "point")
        {
            AddReward(2f);
            Debug.Log(GetCumulativeReward());
        }
        if (c.gameObject.tag == "mapend")
        {
            Reset();
            AddReward(-0.5f);
            count += 1;
            Debug.Log(GetCumulativeReward());
            EndEpisode();
        }
    }

    void Update()
    {
        AddReward(-0.005f);
    }
    private void Reset()
    {
        gameObject.transform.position = originalPos;
        gameObject.transform.eulerAngles = originalRot;
        rigidbody2D.freezeRotation = true;
        rigidbody2D.freezeRotation = false;
    }

}
