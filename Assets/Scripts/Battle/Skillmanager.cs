using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillmanager : MonoBehaviour
{
    public FightManager BM;
    public enum StatusEffect {poison, ATKDown};
    public enum moveType {Water, Fire, Ground, Flying, Steel, Fairy, Poison, Dragon, Rock, Psycic, Electric, Fighting, Grass, Dark, Ghost, Bug, Ice, Normal}
    public void Toxic()
    {
        BM.DoSkill(0,0,0,moveType.Poison,true,false);
    }
}
