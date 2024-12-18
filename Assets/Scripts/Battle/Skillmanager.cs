using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Skillmanager : MonoBehaviour
{
    public FightManager BM;
    public MenuManager menuManager;
    [SerializeField] private DialogueSystem dialogBox;
    int ToxicPP;
    int VenoShockPP;
    int SparkPP;
    int DischargePP;
    public int PoisonPowderPP;
    public int DazzlingGleamPP;
    public int StrengthSapPP;
    public int GigaDrainPP;
    public List<Action> EnemySkills = new List<Action>();
    public enum StatusEffect {poison, ATKDown,HealFromDamage, none};
    public enum moveType { Water, Fire, Ground, Flying, Steel, Fairy, Poison, Dragon, Rock, Psycic, Electric, Fighting, Grass, Dark, Ghost, Bug, Ice, Normal }
    [SerializeField] private Pokemon playerPokemon, enemyPokemon;
    private void Start()
    {
        EnemySkills.Add(PoisonPowder);
        EnemySkills.Add(StrengthSap);
        EnemySkills.Add(DazzlingGleam);
        EnemySkills.Add(GigaDrain);
    }

    public void Toxic()
    {
        dialogBox.Dialogue("Toxtricity used Toxic.");
        StartCoroutine(BM.PokemonAttackSequence(ToxicPP, 0, 100, moveType.Poison, true, StatusEffect.poison, playerPokemon, enemyPokemon));
        ToxicPP--;
        TurnOffFightMenu();
    }
    public void VenoShock()
    {
        dialogBox.Dialogue("Toxtricity used Venoshock.");
        StartCoroutine(BM.PokemonAttackSequence(VenoShockPP,65,100,moveType.Poison,true,StatusEffect.none,playerPokemon,enemyPokemon));
        VenoShockPP--;
        TurnOffFightMenu();
    }
    public void Spark()
    {
        dialogBox.Dialogue("Toxtricity used Spark.");
        StartCoroutine(BM.PokemonAttackSequence(SparkPP, 65, 100, moveType.Electric, true, StatusEffect.none, playerPokemon, enemyPokemon));
        SparkPP--;
        TurnOffFightMenu();
    }
    public void Discharge()
    {
        dialogBox.Dialogue("Toxtricity used Discharge.");
        StartCoroutine(BM.PokemonAttackSequence(DischargePP, 80, 100, moveType.Electric, true, StatusEffect.none, playerPokemon, enemyPokemon));
        DischargePP--;
        TurnOffFightMenu();
    }
    public void TurnOffFightMenu()
    {
        menuManager.TurnOffFightMenu();
    }
    public void PoisonPowder()
    {
        //BM.DoSkill(PoisonPowderPP, 0, 100, moveType.Poison, true, true, StatusEffect.poison);
        PoisonPowderPP--;
        TurnOffFightMenu();
    }
    public void DazzlingGleam()
    {
        //BM.DoSkill(DazzlingGleamPP, 80, 100, moveType.Fairy, true, false, StatusEffect.none);
        DazzlingGleamPP--;
        TurnOffFightMenu();
    }
    public void StrengthSap()
    {
        //BM.DoSkill(StrengthSapPP, 0, 100, moveType.Grass, true, true, StatusEffect.ATKDown);
        StrengthSapPP--;
        TurnOffFightMenu();
    }
    public void GigaDrain()
    {
        //BM.DoSkill(GigaDrainPP, 75, 100, moveType.Grass, true, true, StatusEffect.HealFromDamage);
        GigaDrainPP--;
        TurnOffFightMenu();
    }
}