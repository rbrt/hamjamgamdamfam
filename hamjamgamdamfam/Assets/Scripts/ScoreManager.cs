using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;
    public Text scoreUI;

    public int score;
   
    void Awake()
    {
        Instance = this;
     
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void IncreaseScore(int points)
    {
        score += points;
        scoreUI.text = string.Format("{0:D8}", score);
    }
}