using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : BaseMenu
{
    public TMP_Text Title;
    public Button QuitBtn;
    public Button MainMenuBtn;

    public override void Init(MenuController contex)
    {
        base.Init(contex);
        state = MenuStates.GameOver;

        if (MainMenuBtn) MainMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        if (QuitBtn) QuitBtn.onClick.AddListener(QuitGame);

        if (Title) Title.text = "GameOver! You drove 5 Laps!";

    }

}
