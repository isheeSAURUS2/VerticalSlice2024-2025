using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public GameObject fadeToBlack;
    public Animator enemyPokemonAnimator, playerPokemonAnimator;
    float timer;
    float lerpTimer;
    bool isFading;

    private void Update()
    {
        if (isFading)
        {
            timer += Time.deltaTime;

            if (timer >= 4)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    void DeathSequence(bool isPlayer)
    {
                 
            if (isPlayer)
            {
                playerPokemonAnimator.Play("PokemonDeath");
            }
            else
            {
                enemyPokemonAnimator.Play("EnemyPokemonDeath");
            }
            isFading = true;
            Vector3 fadeToBlackSize = Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(2000f, 2000f, 0f), (timer / 3f));
            fadeToBlack.GetComponent<RectTransform>().sizeDelta = fadeToBlackSize;
        
        
    }
    public void KillPlayer()
    {
        DeathSequence(true);
    }
    public void KillEnemy()
    {
        DeathSequence(false);
    }

}
