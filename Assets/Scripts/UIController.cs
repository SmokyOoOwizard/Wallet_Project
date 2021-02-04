using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text goldText;
    [SerializeField]
    private Text crystalsText;

    [SerializeField]
    private InputField playerPrefsKeyField;
    [SerializeField]
    private InputField filePathField;

    [SerializeField]
    private Toggle fileModeTogle;

    [SerializeField]
    public bool CurrentFileMode;

    private void Start()
    {
        UpdateTexts();
    }

    public void UpdateTexts()
    {
        goldText.text = GameData.Instance.CurrentWallet.GetCurrency("Gold").ToString();
        crystalsText.text = GameData.Instance.CurrentWallet.GetCurrency("Crystals").ToString();
    }

    public void IncrementGold()
    {
        var gold = GameData.Instance.CurrentWallet.GetCurrency("Gold");
        gold++;
        GameData.Instance.CurrentWallet.SetCurrency("Gold", gold);

        UpdateTexts();
    }
    public void ResetGold()
    {
        GameData.Instance.CurrentWallet.SetCurrency("Gold", 0);
        UpdateTexts();
    }
    public void IncrementCrystals()
    {
        var crystals = GameData.Instance.CurrentWallet.GetCurrency("Crystals");
        crystals++;
        GameData.Instance.CurrentWallet.SetCurrency("Crystals", crystals);

        UpdateTexts();
    }
    public void ResetCrystals()
    {
        GameData.Instance.CurrentWallet.SetCurrency("Crystals", 0);
        UpdateTexts();
    }

    #region File
    public void TogleFileMode()
    {
        CurrentFileMode = fileModeTogle.isOn;
    }
    public void SaveInFile()
    {
        Wallet.IWalletStorage tmpStorage = null;
        var path = filePathField.text;

        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        if (CurrentFileMode)
        {
            tmpStorage = Wallet.FileWallStorage.CreateStorage(path, new Wallet.BinaryWalletConverter());
        }
        else
        {
            tmpStorage = Wallet.FileWallStorage.CreateStorage(path, new Wallet.JsonWalletConverter());
        }
        GameData.Instance.CurrentWallet.SaveCurrency(tmpStorage);
    }
    public void LoadFromFile()
    {
        Wallet.IWalletStorage tmpStorage = null;
        var path = filePathField.text;

        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        if (CurrentFileMode)
        {
            tmpStorage = Wallet.FileWallStorage.CreateStorage(path, new Wallet.BinaryWalletConverter());
        }
        else
        {
            tmpStorage = Wallet.FileWallStorage.CreateStorage(path, new Wallet.JsonWalletConverter());
        }
        GameData.Instance.CurrentWallet.ResetCurrency(tmpStorage);

        UpdateTexts();
    }
    #endregion

    #region PlayerPrefs
    public void SaveInPlayerPrefs()
    {
        var key = Wallet.PlayerPrefsWalletStorage.DEFAULT_NAME;
        if (!string.IsNullOrEmpty(playerPrefsKeyField.text))
        {
            key = playerPrefsKeyField.text;
        }

        var tmpStorage = Wallet.PlayerPrefsWalletStorage.CreateStorage(key, new Wallet.JsonWalletConverter());
        GameData.Instance.CurrentWallet.SaveCurrency(tmpStorage);
    }
    public void LoadFromPlayerPrefs()
    {
        var key = Wallet.PlayerPrefsWalletStorage.DEFAULT_NAME;
        if (!string.IsNullOrEmpty(playerPrefsKeyField.text))
        {
            key = playerPrefsKeyField.text;
        }

        var tmpStorage = Wallet.PlayerPrefsWalletStorage.CreateStorage(key, new Wallet.JsonWalletConverter());

        GameData.Instance.CurrentWallet.ResetCurrency(tmpStorage);

        UpdateTexts();
    }
    #endregion
}
