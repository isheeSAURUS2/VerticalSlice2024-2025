using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField] private Pokemon friendlyPokemon, enemyPokemon;
    [SerializeField] private List<Pokemon.PKMType> enemyPKMType, friendlyPKMType = new List<Pokemon.PKMType>();
    Pokemon target, caster;
    int turnIndex;
    // Start is called before the first frame update
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
    public void DoSkill(int pp, float power, int accuracy, Skillmanager.moveType moveType, bool isSpecial, bool hasStatusEffect)
    {
        float isMiss = UnityEngine.Random.Range(0, 100);
        float isCrit = UnityEngine.Random.Range(0, 100);
        float effectivenessMult = 1;
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
            effectivenessMult += 4;
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
        if (isMiss < accuracy)
        {           
            if (isCrit <= 35.2)
            {
                if (isSpecial)
                {

                }
                if (!isSpecial)
                {

                }

            }
            if (isCrit > 35.2)
            {
                if (isSpecial)
                {

                }
                if (!isSpecial)
                {

                }

            }
        }
        else
        {
            //miss
        }
        turnIndex++;
    }

}
