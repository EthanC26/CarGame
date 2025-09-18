using TMPro;
using UnityEngine.UI;

public class InGameMenu : BaseMenu
{
    public TMP_Text Laps;
    public Button PauseBtn;

    public override void Init(MenuController contex)
    {
        base.Init(contex);
        state = MenuStates.InGame;

        if(GameManager.Instance)
        {
            UpdateScore(GameManager.Instance.Score);
            GameManager.Instance.OnScoreChange += UpdateScore;
        }
        if (PauseBtn)
            PauseBtn.onClick.AddListener(() => SetNextMenu(MenuStates.Pause));
    }

    private void UpdateScore(int newScore)
    {
        if (Laps)
            Laps.text = $"Laps: {newScore}/5";
    }

    private void OnDestroy()
    {
        if(GameManager.Instance) 
            GameManager.Instance.OnScoreChange -= UpdateScore;
    }
}
