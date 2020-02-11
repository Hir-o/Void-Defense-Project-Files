using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameController : MonoBehaviour
{
    public static LoadGameController Instance;

    public static bool IsContinueGame;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetIsContinueGame(bool value) { IsContinueGame = value; }
}