using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField] private Pokemon friendlyPokemon, enemyPokemon;
    [SerializeField] private List<Pokemon.PKMType> enemyPKMType, friendlyPKMType = new List<Pokemon.PKMType>();
    Pokemon target, caster;
    public int turnIndex;
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
            if (isMiss < accuracy)
            {
                if (isCrit <= 35.2)
                {
                    if (isSpecial)
                    {
                        target.healthPoints -= ((((((((2 * caster.level) / 5) * power * (caster.specialAttack / target.specialDefense)) / 50) + 2) * (random / 100)) * effectivenessMult) * 1.5f);
                    }
                    if (!isSpecial)
                    {
                        target.healthPoints -= ((((((((2 * caster.level) / 5) * power * (caster.attack / target.defense)) / 50) + 2) * (random / 100)) * effectivenessMult) * 1.5f);
                    }

                }
                if (isCrit > 35.2)
                {
                    if (isSpecial)
                    {
                        target.healthPoints -= (((((((2 * caster.level) / 5) * power * (caster.specialAttack / target.specialDefense)) / 50) + 2) * (random / 100)) * effectivenessMult);
                    }
                    if (!isSpecial)
                    {
                        target.healthPoints -= (((((((2 * caster.level) / 5) * power * (caster.attack / target.defense)) / 50) + 2) * (random / 100)) * effectivenessMult);
                    }

                }
            }
            else
            {
                //miss
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
        }
        turnIndex++;
    }
    public void PokemonAttackSequence()
    {

    }
}
