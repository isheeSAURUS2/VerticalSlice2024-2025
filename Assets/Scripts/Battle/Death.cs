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
    IEnumerator DeathSequence(bool isPlayer)
    {
        while (true) {
            timer += Time.deltaTime;
            
            if (isPlayer)
            {
                playerPokemonAnimator.Play("PokemonDeath");
            }
            else
            {
                enemyPokemonAnimator.Play("EnemyPokemonDeath");
            }
            yield return new WaitForSeconds(1f);
            //fadeToBlack.transform.localScale = Vector3.Lerp(new Vector3(0.1f, 0.1f, 0f), new Vector3(20f, 20f, 0f), (timer / 3f));
            
            Vector3 fadeToBlackSize = Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(2000f, 2000f, 0f), (timer / 3f));
            fadeToBlack.GetComponent<RectTransform>().sizeDelta = fadeToBlackSize;
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
        }
    }
    public void KillPlayer()
    {
        StartCoroutine(DeathSequence(true));
    }
    public void KillEnemy()
    {
        StartCoroutine(DeathSequence(false));
    }

}
