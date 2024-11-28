using UnityEngine;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(Close);
    }

    private void Close()
    {
        PanelManager.Instance.HidePanel("OptionsPanel");
    }
}
