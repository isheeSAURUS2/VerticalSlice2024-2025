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
    int ToxicPP;
    int VenoShockPP;
    int SparkPP;
    int DischargePP;
    int PoisonPowderPP;
    int DazzlingGleamPP;
    int StrengthSapPP;
    int GigaDrainPP;
    public List<Action> EnemySkills = new List<Action>();
    public enum StatusEffect {poison, ATKDown,HealFromDamage, none};
    public enum moveType { Water, Fire, Ground, Flying, Steel, Fairy, Poison, Dragon, Rock, Psycic, Electric, Fighting, Grass, Dark, Ghost, Bug, Ice, Normal }
    private void Start()
    {
        EnemySkills.Add(PoisonPowder);
        EnemySkills.Add(StrengthSap);
        EnemySkills.Add(DazzlingGleam);
        EnemySkills.Add(GigaDrain);
    }

    public void Toxic()
    {
        BM.DoSkill(ToxicPP,0,100,moveType.Poison,true,true,StatusEffect.poison);
        ToxicPP--;
        TurnOffFightMenu();
    }
    public void VenoShock()
    {
        BM.DoSkill(VenoShockPP, 65, 100, moveType.Poison, true, false,StatusEffect.none);
        VenoShockPP--;
        TurnOffFightMenu();
    }
    public void Spark()
    {
        BM.DoSkill(SparkPP, 65, 100, moveType.Electric, true, false, StatusEffect.none);
        SparkPP--;
        TurnOffFightMenu();
    }
    public void Discharge()
    {
        BM.DoSkill(DischargePP, 80, 100, moveType.Electric, true, false, StatusEffect.none);
        DischargePP--;
        TurnOffFightMenu();
    }
    public void TurnOffFightMenu()
    {
        menuManager.TurnOffFightMenu();
    }
    public void PoisonPowder()
    {
        BM.DoSkill(PoisonPowderPP, 0, 100, moveType.Poison, true, true, StatusEffect.poison);
        PoisonPowderPP--;
        TurnOffFightMenu();
    }
    public void DazzlingGleam()
    {
        BM.DoSkill(DazzlingGleamPP, 80, 100, moveType.Fairy, true, false, StatusEffect.none);
        DazzlingGleamPP--;
        TurnOffFightMenu();
    }
    public void StrengthSap()
    {
        BM.DoSkill(StrengthSapPP, 0, 100, moveType.Grass, true, true, StatusEffect.ATKDown);
        StrengthSapPP--;
        TurnOffFightMenu();
    }
    public void GigaDrain()
    {
        BM.DoSkill(GigaDrainPP, 75, 100, moveType.Grass, true, true, StatusEffect.HealFromDamage);
        GigaDrainPP--;
        TurnOffFightMenu();
    }
}