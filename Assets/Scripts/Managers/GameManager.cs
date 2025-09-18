using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private int _Score = 0;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void AddScore(int score)
    {
        _Score += score;
    }
}
