using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateScoreboard : MonoBehaviour
{
    //public Text scoreText;
    public TMP_Text scoreText;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Scan: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        score += 1;
        scoreText.text = "Scan: " + score;
    }
}
