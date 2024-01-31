using System;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int Damage { get; private set; }

    public float CurrentAutoAttackIntervalValue { get; private set; } = 2f;
    public float MaxAutoAttackIntervalValue { get; private set; } = 0.2f;
    public CoinManager CoinManager;

    public Player(int initDamage, int initCoins)
    {
        this.Damage = initDamage;
        CoinManager = new CoinManager(initCoins);
    }

    public void Attack(Enemy enemy)
    {
        enemy.TakeDamage(Damage);
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
        if (CurrentAutoAttackIntervalValue <= MaxAutoAttackIntervalValue)
        {
            CurrentAutoAttackIntervalValue = MaxAutoAttackIntervalValue;
        }
        else
        {
            CurrentAutoAttackIntervalValue -= increment;
        }
    }

    private void ShowNotEnoughCoinsMessage()
    {
        Debug.LogWarning("Not enough coins to upgrade damage.");
    }
}



