using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnorlaxSwitch : MonoBehaviour
{
    [SerializeField] GameObject SnorlaxSpawn;
    [SerializeField] GameObject ToxtricitySpawn;
    public void Snorlax()
    {
        SnorlaxSpawn.SetActive(true);
        ToxtricitySpawn.SetActive(false);
    }
}
