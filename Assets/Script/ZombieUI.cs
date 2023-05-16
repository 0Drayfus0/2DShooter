using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieUI : MonoBehaviour
{
    public Slider slider;
    public Zombie zombie;

    private void Start()
    {
        slider.maxValue = zombie.health;
        slider.value = zombie.health;
        zombie.OnChangeHealth += UpdateHealthBar;
    }

    public void UpdateHealthBar()
    {
        slider.value = zombie.health;
    }
}
