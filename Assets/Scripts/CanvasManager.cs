using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text healthText;

    public GameObject titleScreen;
    public GameObject playerScreen;

    public void UpdateHealth(float health)
    {
        healthText.text = health.ToString();
    }
}
