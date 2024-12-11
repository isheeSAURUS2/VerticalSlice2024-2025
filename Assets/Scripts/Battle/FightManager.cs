using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField] private Pokemon friendlyPokemon, enemyPokemon;
    [SerializeField] private List<Pokemon.PKMType> enemyPKMType, friendlyPKMType = new List<Pokemon.PKMType>();
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private DialogueSystem dialogueManager;
    [SerializeField] private Skillmanager skillmanager = new Skillmanager();
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField]private Death deathScript;
    bool isYourTurn = true;
    Pokemon target, caster;
    public int turnIndex;
    private void Start()
    {
        playerAnimator = friendlyPokemon.GetComponent<Animator>();
        menuManager.SwitchToBattleMenu();
        //enemyAnimator = enemyAnimator.GetComponent<Animator>();
        //PokemonAttackSequence(5f);
    }
    private void Update()
    {
        if (friendlyPokemon.healthPoints <= 0f)
        {
            deathScript.KillPlayer();
        }
        else if (enemyPokemon.healthPoints <= 0f)
        {
            deathScript.KillEnemy();
        }
    }
    public float DoSkill(int pp, float power, int accuracy, Skillmanager.moveType moveType, bool isSpecial, Skillmanager.StatusEffect statusType, Pokemon caster, Pokemon target)
    {
        float isMiss = UnityEngine.Random.Range(0, 100);
        float isCrit = UnityEngine.Random.Range(0, 100);
        float effectivenessMult = 1;
        float random = UnityEngine.Random.Range(85, 100);
        float damage = 0f;
        if (moveType == Skillmanager.moveType.Poison && target.type[0] == Pokemon.PKMType.Fairy && target.type[1] == Pokemon.PKMType.Grass)
        {
           effectivenessMult += 3;
        }
        if (moveType == Skillmanager.moveType.Fairy && target.type[0] == Pokemon.PKMType.Poison || target.type[1] == Pokemon.PKMType.Poison)
        {
            effectivenessMult -= 0.5f;
        }
        if (moveType == Skillmanager.moveType.Grass && target.type[0] == Pokemon.PKMType.Poison || target.type[1] == Pokemon.PKMType.Poison)
        {
            effectivenessMult -= 0.5f;
        }
        if (moveType == Skillmanager.moveType.Electric && target.type[0] == Pokemon.PKMType.Grass || target.type[1] == Pokemon.PKMType.Grass)
        {
            effectivenessMult -= 0.5f;
        }
            Debug.Log((((((((2 * caster.level) / 5) * power * (caster.specialAttack / target.specialDefense)) / 50) + 2) * (random / 100)) * effectivenessMult));
            Debug.Log(power + " " + caster.level + " " + caster.specialAttack + " " + target.specialDefense + " " + random + " " + effectivenessMult);
            Debug.Log("caster: "+caster.name);
            Debug.Log("target: " + target.name);
            if (isMiss < accuracy)
            {
                if (isCrit <= 35.2)
                {
                    if (isSpecial)
                    {
                        damage = (((((((2 * caster.level) / 5) * power * (caster.specialAttack / target.specialDefense)) / 50) + 2) * (random / 100)) * effectivenessMult) * 1.5f;
                    }
                    if (!isSpecial)
                    {
                        damage = (((((((2 * caster.level) / 5) * power * (caster.attack / target.defense)) / 50) + 2) * (random / 100)) * effectivenessMult) * 1.5f;
                    }

                }
                if (isCrit > 35.2)
                {
                    if (isSpecial)
                    {
                        damage = ((((((2 * caster.level) / 5) * power * (caster.specialAttack / target.specialDefense)) / 50) + 2) * (random / 100)) * effectivenessMult;
                    }
                    if (!isSpecial)
                    {
                        damage = ((((((2 * caster.level) / 5) * power * (caster.attack / target.defense)) / 50) + 2) * (random / 100)) * effectivenessMult;
                    }

                }
            }
            else
            {
                // Miss
            }
        
        if (statusType != Skillmanager.StatusEffect.none)
        {
            if (statusType == Skillmanager.StatusEffect.poison)
            {
                target.isPoisoned = true;
            }
            if (statusType == Skillmanager.StatusEffect.ATKDown)
            {
                caster.healthPoints += target.attack;
                target.attack -= 12;
            }
            if(statusType == Skillmanager.StatusEffect.HealFromDamage)
            {
                caster.healthPoints += ((((((((2 * caster.level) / 5) * power * (caster.specialAttack / target.specialDefense)) / 50) + 2) * (random / 100)) * effectivenessMult) * 1.5f) / 2;
            }
        }
        Debug.Log("damage is "+damage);
        return damage;
    }
    public IEnumerator PokemonAttackSequence(int pp, float power, int accuracy, Skillmanager.moveType moveType, bool isSpecial, Skillmanager.StatusEffect statustype,Pokemon player,Pokemon enemy)
    {
        // texbox
        menuManager.inBattleSequence = true;
        
        
        float damage = 0;
        
        yield return new WaitForSeconds(1.9f);
        playerAnimator.Play("PokemonAttackAnimationForNow"); // Play player attack Animation
        damage = DoSkill(pp,power,accuracy,moveType, isSpecial, statustype, player, enemy);
        yield return new WaitForSeconds(2); 
        for (int i = 0; i < damage; i++)
        {
            enemy.healthPoints--;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(4f);
        damage = EnemyMove(); //textvox
        
        yield return new WaitForSeconds(2f);
        enemyAnimator.Play("EnemyPokemonAttack"); // Play enemy attack Animation
        
        yield return new WaitForSeconds(2);
        for (int i = 0; i < damage; i++)
        {
            player.healthPoints--;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForEndOfFrame();
        menuManager.inBattleSequence = false;
        menuManager.SwitchToBattleMenu(); // End battle scene
    }
    public float EnemyMove()
    {
        float random;
        random = UnityEngine.Random.Range(0, 3);
        random = Mathf.Round(random);
        if (random == 0) 
        {
            dialogueManager.Dialog("Shiinotic used Poison Powder");
            
            return DoSkill(skillmanager.PoisonPowderPP, 0, 100, Skillmanager.moveType.Poison, true, Skillmanager.StatusEffect.poison, enemyPokemon, friendlyPokemon);
        }
        if (random == 1)
        {
            dialogueManager.Dialog("Shiinotic used Dazzling Gleam");
            
            return DoSkill(skillmanager.DazzlingGleamPP, 80, 100, Skillmanager.moveType.Fairy, true, Skillmanager.StatusEffect.none, enemyPokemon, friendlyPokemon);
        }
        if (random == 2)
        {
            dialogueManager.Dialog("Shiinotic used Strength Sap");
            
            return DoSkill(skillmanager.StrengthSapPP, 0, 100, Skillmanager.moveType.Grass, true, Skillmanager.StatusEffect.ATKDown, enemyPokemon, friendlyPokemon);
        }
        if (random == 3)
        {
            dialogueManager.Dialog("Shiinotic used Giga Drain");
            
            return DoSkill(skillmanager.GigaDrainPP, 75, 100, Skillmanager.moveType.Grass, true, Skillmanager.StatusEffect.HealFromDamage, enemyPokemon, friendlyPokemon);
        }
        return 0f;
    }
}   
