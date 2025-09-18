using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private int _Score = 0;
    public int Score => _Score;
    public event Action<int> OnScoreChange; //Event when score updates

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(gameObject);
    }

    private void Update()
    {

        if (_Score >= 5)//go to game over scene when laps = 5
        {
            string sceneName = (SceneManager.GetActiveScene().name.Contains("Level")) ? "GameOver" : "Level";
            SceneManager.LoadScene(sceneName);

            ResetScore();
        }
    }

    public void AddScore(int score)//adds a int to _Score when called
    {
        _Score += score;
        OnScoreChange?.Invoke(_Score);
    }

    public void ResetScore()//resets score when called
    {
        _Score = 0;
        OnScoreChange?.Invoke(_Score);
    }
}
