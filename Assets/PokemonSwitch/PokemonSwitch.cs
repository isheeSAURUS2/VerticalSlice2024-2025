using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSwitch : MonoBehaviour
{
    [SerializeField] GameObject snorlaxSpawn;
    [SerializeField] GameObject toxtricitySpawn;
    [SerializeField] GameObject snorlaxText;
    public void Snorlax()
    {
        snorlaxSpawn.SetActive(true);
        toxtricitySpawn.SetActive(false);
        snorlaxText.SetActive(true);
    }
    public void Toxtricity() 
    {
        snorlaxSpawn.SetActive(false);
        toxtricitySpawn.SetActive(true);
        snorlaxText.SetActive(false);
    }
}
