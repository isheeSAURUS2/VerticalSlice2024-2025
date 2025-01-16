using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField] private Pokemon friendlyPokemon, enemyPokemon;
    [SerializeField] private List<Pokemon.PokemonType> enemyPokemonType, friendlyPokemonType = new List<Pokemon.PokemonType>();
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private DialogueSystem dialogueManager;
    [SerializeField] private Skillmanager skillmanager = new Skillmanager();
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private GameObject Camera1, Camera2, Camera3, Camera4;
    [SerializeField] private GameObject PlayerAttackParticle, PlayerAttackHitParticle;
    bool isYourTurn = true;
    Pokemon target, caster;
    private void Start()
    {
        menuManager.SwitchToBattleMenu();
    }
    public float DoSkill(int pp, float power, int accuracy, Skillmanager.moveType moveType, bool isSpecial, Skillmanager.StatusEffect statusType, Pokemon caster, Pokemon target) // Calculating the damage with given variables
    {
        float isMiss = UnityEngine.Random.Range(0, 100);
        float isCrit = UnityEngine.Random.Range(0, 100);
        float effectivenessMult = 1;
        float random = UnityEngine.Random.Range(85, 100);
        float damage = 0f;
        if (moveType == Skillmanager.moveType.Poison && target.type[0] == Pokemon.PokemonType.Fairy && target.type[1] == Pokemon.PokemonType.Grass)
        {
           effectivenessMult += 3;
        }
        if (moveType == Skillmanager.moveType.Fairy && target.type[0] == Pokemon.PokemonType.Poison || target.type[1] == Pokemon.PokemonType.Poison)
        {
            effectivenessMult -= 0.5f;
        }
        if (moveType == Skillmanager.moveType.Grass && target.type[0] == Pokemon.PokemonType.Poison || target.type[1] == Pokemon.PokemonType.Poison)
        {
            effectivenessMult -= 0.5f;
        }
        if (moveType == Skillmanager.moveType.Electric && target.type[0] == Pokemon.PokemonType.Grass || target.type[1] == Pokemon.PokemonType.Grass)
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
    public IEnumerator PokemonAttackSequence(int pp, float power, int accuracy, Skillmanager.moveType moveType, bool isSpecial, Skillmanager.StatusEffect statustype,Pokemon player,Pokemon enemy) // Pokemon attack sequence in order
    {
        menuManager.inBattleSequence = true;
        
        
        float damage = 0;
        
        yield return new WaitForSeconds(1.9f);
        Camera3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimator.Play("AttackAnimation"); // Play player attack Animation
        PlayerAttackParticle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        PlayerAttackParticle.SetActive(false);
        PlayerAttackHitParticle.SetActive(true);
        // Play Hurt Animation Shinotic
        Camera3.SetActive(false);
        Camera4.SetActive(true);
        damage = DoSkill(pp,power,accuracy,moveType, isSpecial, statustype, player, enemy);
        yield return new WaitForSeconds(1);
        PlayerAttackHitParticle.SetActive(false);
        for (int i = 0; i < damage; i++)
        {
            enemy.healthPoints--;
            yield return new WaitForSeconds(0.01f);
        }
        Camera1.SetActive(true);
        Camera4.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        damage = EnemyMove(); //textvox
        Camera4.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        enemyAnimator.Play("EnemyPokemonAttack"); // Play enemy attack Animation
        yield return new WaitForSeconds(1.3f);
        playerAnimator.Play("HitAnimation");
        Camera3.SetActive(true);
        Camera4.SetActive(false);

        yield return new WaitForSeconds(2);
        for (int i = 0; i < damage; i++)
        {
            player.healthPoints--;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForEndOfFrame();
        Camera1.SetActive(true);
        Camera3.SetActive(false);
        menuManager.inBattleSequence = false;
        menuManager.SwitchToBattleMenu(); // End battle scene
    }
    public float EnemyMove() // Enemy chooses a random move and returns the damage value
    {
        float random;
        random = UnityEngine.Random.Range(0, 3);
        random = Mathf.Round(random);
        if (random == 0) 
        {
            dialogueManager.Dialogue("Shiinotic used Poison Powder");
            
            return DoSkill(skillmanager.PoisonPowderPP, 0, 100, Skillmanager.moveType.Poison, true, Skillmanager.StatusEffect.poison, enemyPokemon, friendlyPokemon);
        }
        if (random == 1)
        {
            dialogueManager.Dialogue("Shiinotic used Dazzling Gleam");
            
            return DoSkill(skillmanager.DazzlingGleamPP, 80, 100, Skillmanager.moveType.Fairy, true, Skillmanager.StatusEffect.none, enemyPokemon, friendlyPokemon);
        }
        if (random == 2)
        {
            dialogueManager.Dialogue("Shiinotic used Strength Sap");
            
            return DoSkill(skillmanager.StrengthSapPP, 0, 100, Skillmanager.moveType.Grass, true, Skillmanager.StatusEffect.ATKDown, enemyPokemon, friendlyPokemon);
        }
        if (random == 3)
        {
            dialogueManager.Dialogue("Shiinotic used Giga Drain");
            
            return DoSkill(skillmanager.GigaDrainPP, 75, 100, Skillmanager.moveType.Grass, true, Skillmanager.StatusEffect.HealFromDamage, enemyPokemon, friendlyPokemon);
        }
        return 0f;
    }
}   