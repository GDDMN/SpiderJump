using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private SpiderJump _player;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Text _scoreBar;
    private int _scores = 0;
    private int _highScores = 0;

    public void Awake()
    {
        _player = FindObjectOfType<SpiderJump>();
        _playerHealth = _player.gameObject.GetComponent<Health>();
        _scoreBar.text = _scores.ToString();
    }

    public void Update()
    {
        if (_playerHealth.Dead)
        {
            _highScores = _scores;
            PlayerPrefs.SetInt("HIGH_SCORES", _highScores);
            return;
        }

        if (_player.GetComponent<Rigidbody2D>().velocity.y > Vector2.zero.y)
        {
            _scores++;
            _scoreBar.text = _scores.ToString();
        }
    }
}
