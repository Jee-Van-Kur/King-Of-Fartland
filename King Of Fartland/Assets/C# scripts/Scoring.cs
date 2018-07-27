using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {

    public static Scoring instace;
    public int CurrentScore;
    public Text scoreText;
    public Animator textAnim;
    // Use this for initialization
    void Start()
    {
        instace = this;
        CurrentScore = 0;
        setCount();
    }

    public void setCount()
    {
        textAnim.Play("score_up");
        scoreText.text = "Score:" + CurrentScore.ToString() ;
    }
}
