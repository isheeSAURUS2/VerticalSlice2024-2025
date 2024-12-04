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
        PokemonAttackSequence(5f);
    }
    private void Update()
    {
        if (!enemyPokemon.typesAddedToBM)
        {
            for (int i = 0; i < enemyPokemon.type.Length; i++)
            {
                enemyPKMType.Add(enemyPokemon.type[i]);
            }
            enemyPokemon.typesAddedToBM = true;
        }
        if (!friendlyPokemon.typesAddedToBM)
        {
            for (int i = 0; i < friendlyPokemon.type.Length; i++)
            {
                friendlyPKMType.Add(friendlyPokemon.type[i]);
            }
            friendlyPokemon.typesAddedToBM = true;
        }
        if(turnIndex > 1)
        {
            turnIndex = 0;
            isYourTurn = true;
        }
        if(turnIndex == 1)
        {
            Coroutine newRoutine = StartCoroutine(EnemyAttack());
        }
    }
    public void DoSkill(int pp, float power, int accuracy, Skillmanager.moveType moveType, bool isSpecial, bool hasStatusEffect, Skillmanager.StatusEffect statusType)
    {
        float isMiss = UnityEngine.Random.Range(0, 100);
        float isCrit = UnityEngine.Random.Range(0, 100);
        float effectivenessMult = 1;
        float random = UnityEngine.Random.Range(85, 100);
       
        if (turnIndex == 0)
        {
            target = enemyPokemon;
            caster = friendlyPokemon;
        }
        if (turnIndex == 1)
        {
            caster = enemyPokemon;
            target = friendlyPokemon;
        }
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
        if (!hasStatusEffect)
        {
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
                        StartCoroutine(PokemonAttackSequence(((((((((2 * caster.level) / 5) * power * (caster.specialAttack / target.specialDefense)) / 50) + 2) * (random / 100)) * effectivenessMult) * 1.5f)));
                    }
                    if (!isSpecial)
                    {
                        StartCoroutine(PokemonAttackSequence(((((((((2 * caster.level) / 5) * power * (caster.attack / target.defense)) / 50) + 2) * (random / 100)) * effectivenessMult) * 1.5f)));
                    }

                }
                if (isCrit > 35.2)
                {
                    if (isSpecial)
                    {
                        StartCoroutine(PokemonAttackSequence((((((((2 * caster.level) / 5) * power * (caster.specialAttack / target.specialDefense)) / 50) + 2) * (random / 100)) * effectivenessMult)));
                    }
                    if (!isSpecial)
                    {
                        StartCoroutine(PokemonAttackSequence((((((((2 * caster.level) / 5) * power * (caster.attack / target.defense)) / 50) + 2) * (random / 100)) * effectivenessMult)));
                    }

                }
            }
            else
            {
                // Miss
                StartCoroutine(AttackMissed());
            }
        }
        if (hasStatusEffect)
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
        turnIndex++;
    }
    public IEnumerator PokemonAttackSequence(float damage)
    {
        //dialogueManager.textSelector = 1;
        yield return new WaitForSeconds(2);
        playerAnimator.Play("PokemonAttackAnimationForNow");
        enemyAnimator.Play("EnemyPokemonAttack");
        yield return new WaitForSeconds(2);
        for (int i = 0; i < damage; i++)
        {
            target.healthPoints--;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForEndOfFrame();
        menuManager.SwitchToBattleMenu();
    }
    private IEnumerator AttackMissed()
    {
        //dialogueManager.textSelector = 3;
        yield return new WaitForSeconds(2);
        menuManager.SwitchToBattleMenu();
    }
    public IEnumerator EnemyAttack()
    {
        isYourTurn = false;
        Debug.Log("enemy is going to attack");
        yield return new WaitForSeconds(2);
        if (!isYourTurn)
        {
            menuManager.TurnOffUI();
            skillmanager.EnemySkills[UnityEngine.Random.Range(0, 3)]();
            isYourTurn = true;
        }
        yield break;
    }
}   
