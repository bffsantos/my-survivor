using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(StartGame);
        _optionsButton.onClick.AddListener(OpenOptions);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {
        GameManager.Instance.LoadScene("GameScene");
    }

    private void OpenOptions()
    {
        PanelManager.Instance.ShowPanel("OptionsPanel");
    }

    private void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
