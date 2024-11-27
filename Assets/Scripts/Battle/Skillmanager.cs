using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Skillmanager : MonoBehaviour
{

    public FightManager BM;
    int ToxicPP;
    int VenoShockPP;
    int SparkPP;
    int DischargePP;
    public enum StatusEffect {poison, ATKDown, none};
    public enum moveType {Water, Fire, Ground, Flying, Steel, Fairy, Poison, Dragon, Rock, Psycic, Electric, Fighting, Grass, Dark, Ghost, Bug, Ice, Normal}
    public void Toxic()
    {
        BM.DoSkill(ToxicPP,0,100,moveType.Poison,true,true,StatusEffect.poison);
        ToxicPP--;
    }
    public void VenoShock()
    {
        BM.DoSkill(VenoShockPP, 65, 100, moveType.Poison, true, false,StatusEffect.none);
        VenoShockPP--;
    }
    public void Spark()
    {
        BM.DoSkill(SparkPP, 65, 100, moveType.Electric, true, false, StatusEffect.none);
        SparkPP--;
    }
    public void Discharge()
    {
        BM.DoSkill(DischargePP, 80, 100, moveType.Electric, true, false, StatusEffect.none);
        DischargePP--;
    }
}
