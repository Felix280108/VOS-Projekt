using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int geld;
    public Text money;
    // Start is called before the first frame update
    void Start()
    {
        geld = PlayerPrefs.GetInt("Money", 0);
    }

    // Update is called once per frame
    void Update()
    {
        string points = PlayerPrefs.GetInt("Money", 0).ToString();
        money.text = points; //points=das geld das ich habe
    }
    public void AddMoney(int coin)
    {
        geld += coin;
        PlayerPrefs.SetInt("Money", geld);
    }
}