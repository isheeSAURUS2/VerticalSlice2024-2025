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
    bool startTimer;
    IEnumerator DeathSequence(bool isPlayer)
    {
        while (true) {
            
            
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
            
            FadeOut();
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
        }
    }
    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
        }
    }
    public void FadeOut()
    {
        startTimer = true;
        Vector2 fadeToBlackSize = Vector2.Lerp(new Vector2(1f, 1f), new Vector2(2000f, 2000f), (timer / 2f));
        fadeToBlack.GetComponent<RectTransform>().sizeDelta = fadeToBlackSize;
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
