using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    public Wallet.Wallet CurrentWallet { get; private set; }

    private void Awake()
    {
        if (Instance != this)
        {
            Instance = this;
        }

        CurrentWallet = Wallet.Wallet.CreateWallet(null);
    }
}
