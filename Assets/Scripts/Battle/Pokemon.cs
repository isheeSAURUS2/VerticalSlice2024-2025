using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour
{
    public string pokemonName;
    public float maxHealthPoints;
    public float healthPoints;
    public int specialDefense;
    public int defense;
    public int specialAttack;
    public int attack;
    public int level;
    public bool isPoisoned;
    public int thisPokemonTurnIndex;
    public float poisonDamage = 6.25f;
    [SerializeField] private FightManager battleManager;
    public enum PKMType{Water, Fire, Ground, Flying, Steel, Fairy, Poison, Dragon, Rock, Psycic, Electric, Fighting, Grass, Dark, Ghost, Bug, Ice, Normal};
    public PKMType[] type;
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject healthBarFill;
    Color orange = new Color(1, 0.6f, 0, 1);
    [HideInInspector]public bool typesAddedToBM;
    private bool wasPoisonedThisTurn;
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
            healthBarFill.GetComponent<Image>().color = Color.green;
        }
        if(healthPercentage <= 50&&healthPercentage > 25)
        {
            healthBarFill.GetComponent<Image>().color = orange;
        }
        if (healthPercentage <= 25 && healthPercentage > 0)
        {
            healthBarFill.GetComponent<Image>().color = Color.red;
        }
        if(healthPoints > maxHealthPoints)
        {
            healthPoints = maxHealthPoints;
        }
        if(battleManager.turnIndex != thisPokemonTurnIndex && !wasPoisonedThisTurn)
        {
            Poison();
            wasPoisonedThisTurn = true;
        }
    }

    private void Poison()
    {
        healthPoints -= poisonDamage;
        poisonDamage += 6.25f;
    }
}