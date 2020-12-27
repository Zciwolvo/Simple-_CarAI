using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class listfollow : MonoBehaviour
{

    private static int Count;
    private static int CoinCount;
    Text txt;

    void Start()
    {
        txt = GetComponent<Text>();

    }

    void Update()
    {
        Count = CarAgent.count;
        CoinCount = CarAgent.Coincount;
        txt.text = "Number of subjects : " + Count + "\nCoins achieved : " + CoinCount;



    }
}
