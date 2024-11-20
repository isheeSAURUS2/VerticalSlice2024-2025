using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour
{
    public string pokemonName;
    public int maxHealthPoints = 1;
    public int healthPoints;
    public int specialDefense;
    public int defense;
    public int specialAttack;
    public int attack;
    public Slider healthBar;
    [SerializeField] GameObject healthBarFill;
    private void Start()
    {
        healthBar.maxValue = maxHealthPoints;
    }
    private void Update()
    {
        float healthPercentage = (healthPoints / maxHealthPoints) * 100;
        healthBar.value = healthPoints;
        if(healthPercentage <= 100 && healthPercentage > 50)
        {
            healthBar.GetComponentInChildren<Image>(true).color = Color.green;
        }
        if(healthPercentage <= 50&&healthPercentage > 25)
        {
            healthBar.GetComponentInChildren<Image>(true).color = new Color(1,0.2f,0,1);
        }
        if (healthPercentage <= 25 && healthPercentage > 0)
        {
            healthBar.GetComponentInChildren<Image>(true).color = Color.red;
        }
    }
}
