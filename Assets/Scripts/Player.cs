using System;
using UnityEngine;

[System.Serializable]
public class Player
{
    public delegate void UpdateCoinsEventHandler(int coins);
    public event UpdateCoinsEventHandler OnUpdateCoins;

    public int Damage { get; private set; }
    public CoinManager CoinManager;

    public float CurrentAutoAttackIntervalValue { get; private set; } = 2f;
    public float MaxAutoAttackIntervalValue { get; private set; } = 0.2f;



    public Player(int initDamage, int initCoins)
    {
        this.Damage = Damage;
        CoinManager = new CoinManager(initCoins);
    }

    public void Attack(Enemy enemy)
    {
        enemy.TakeDamage(Damage);
        UpdateUI();
    }

    private bool CanUpgrade(int cost) => CoinManager.CurrentCoinsValue >= cost;

    public void UpgradeDamage(int cost, int increment)
    {
        if (CanUpgrade(cost))
        {
            CoinManager.SetTargetCoinsValueToSpend(cost);
            UpgradeDamageInternal(increment);
        }
        else
        {
            ShowNotEnoughCoinsMessage();
        }
    }
    private void UpgradeDamageInternal(int increment)
    {
        Damage += increment;
    }
    public void UpgradeAutoAttack(int cost, float increment)
    {
        if (CanUpgrade(cost))
        {
            CoinManager.SetTargetCoinsValueToSpend(cost);
            UpgradeAutoAttackInternal(increment);
        }
        else
        {
            ShowNotEnoughCoinsMessage();
        }
    }
    private void UpgradeAutoAttackInternal(float increment)
    {
        CurrentAutoAttackIntervalValue -= increment;
    }

    private void ShowNotEnoughCoinsMessage()
    {
        Debug.LogWarning("Not enough coins to upgrade damage.");
    }
    private void UpdateUI()
    {
        OnUpdateCoins?.Invoke(CoinManager.CurrentCoinsValue);
    }
}



