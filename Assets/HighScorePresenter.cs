using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePresenter : MonoBehaviour
{
    private void Start()
    {
        Text highScore = this.GetComponent<Text>();
        highScore.text = PlayerPrefs.GetInt("HIGH_SCORES").ToString();
    }
}
