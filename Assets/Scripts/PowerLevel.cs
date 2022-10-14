using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PowerLevel : MonoBehaviour
{
    public int currentXP = 0;
    TornandoPlayerController playerController;
    public TextMeshProUGUI speedText;
    public Image cityImageHolder;
    public List<Sprite> cities;
    public Slider xpBar;
    private void Start()
    {
        playerController = GetComponent<TornandoPlayerController>();
        speedText.text = "A category 1 tornado is approaching!";
    }

    public void UpdateXP(int powerValue)
    {

        if (powerValue > playerController.PowerupCount)
        {
            playerController.PlayPowerSFX(false);
            if (currentXP > 0)
            {
                resetXP();
            }
            else
            {
                playerController.DoPowerup(10);
                resetXP();
            }
        }
        else
        {
            playerController.PlayPowerSFX(true);
            currentXP += powerValue * 100;
            if (currentXP >= playerController.PowerupCount * 200)
            {
                playerController.DoPowerup(0);
                resetXP();
            }

        }
        UIUpdate();

    }
    void UIUpdate()
    {
        xpBar.value = (float)currentXP / (playerController.PowerupCount * 200f);
        if (playerController.PowerupCount >= 3)
        {
            cityImageHolder.sprite = cities[1];
            GameManager.Instance.ChangeEndingMaterial(true);
            speedText.text = "EMERGENCY!A category " + playerController.PowerupCount + " tornado is approaching!";
        }
        else
        {
            GameManager.Instance.ChangeEndingMaterial(false);
            cityImageHolder.sprite = cities[0];

            speedText.text = "A category " + playerController.PowerupCount + " tornado is approaching!";
        }
    }


    public void resetXP()
    {
        currentXP = 0;

    }
}



