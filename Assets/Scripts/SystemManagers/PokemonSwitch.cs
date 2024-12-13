using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSwitch : MonoBehaviour
{
    [SerializeField] GameObject SnorlaxSpawn;
    [SerializeField] GameObject ToxtricitySpawn;
    public void Snorlax()
    {
        SnorlaxSpawn.SetActive(true);
        ToxtricitySpawn.SetActive(false);
    }
    public void Toxtricity() 
    {
        SnorlaxSpawn.SetActive(false);
        ToxtricitySpawn.SetActive(true);
    }
}
