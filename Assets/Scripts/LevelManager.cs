using System;
using System.Collections;
using System.Collections.Generic;
using AnimatorParams;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private float animationLength;
    
    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void LoadGameOver()
    {
        AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.fadeAnimator, AnimatorParameters.FADE,
                                           AnimatorParameters.ParameterTypeTrigger);

        animationLength = ObjectReferenceHolder.Instance.fadeAnimator.GetCurrentAnimatorStateInfo(0).length;

        StartCoroutine(InitializeGameOver());
    }

    private IEnumerator InitializeGameOver()
    {
        yield return new WaitForSeconds(animationLength);
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}