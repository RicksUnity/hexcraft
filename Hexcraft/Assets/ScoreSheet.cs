using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSheet : MonoBehaviour
{
    public Text scoreText;
    private int counteru;

    // Use this for initialization
    void Start()
    {
        scoreText.text = "bloooooooooooopie";
        counteru = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "blooooootyujhgooooopie";
    }
}
