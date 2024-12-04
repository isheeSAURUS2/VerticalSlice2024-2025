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
    bool isYourTurn = true;
    Pokemon target, caster;
    public int turnIndex;
    private void Start()
    {
        playerAnimator = friendlyPokemon.GetComponent<Animator>();
        //enemyAnimator = enemyAnimator.GetComponent<Animator>();
        //PokemonAttackSequence(5f);
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
            Debug.Log(caster.name);
            Debug.Log(target.name);
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
    public IEnumerator PokemonAttackSequence(int pp, float power, int accuracy, Skillmanager.moveType moveType, bool isSpecial, Skillmanager.StatusEffect statustype,Pokemon attacker,Pokemon defender)
    {
        float damage = 0;
        menuManager.ShowOnlyHPCard();
        yield return new WaitForSeconds(2);
        playerAnimator.Play("PokemonAttackAnimationForNow");
        damage = DoSkill(pp,power,accuracy,moveType, isSpecial, statustype, attacker, defender);
        yield return new WaitForSeconds(2); 
        for (int i = 0; i < damage; i++)
        {
            defender.healthPoints--;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2);
        enemyAnimator.Play("EnemyPokemonAttack");
        damage = DoSkill(pp, power, accuracy, moveType, isSpecial, statustype, attacker, defender);
        yield return new WaitForSeconds(2);
        for (int i = 0; i < damage; i++)
        {
            attacker.healthPoints--;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForEndOfFrame();
        menuManager.SwitchToBattleMenu();
    }
    
}   
