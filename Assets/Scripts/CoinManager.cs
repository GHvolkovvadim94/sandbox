public class CoinManager
{
    public int CurrentCoinsValue { get; private set; }
    public int TargetCoinsValue { get; private set; }

    public CoinManager(int initialCoins)
    {
        CurrentCoinsValue = initialCoins;
        TargetCoinsValue = initialCoins;
    }

    public void SetTargetCoinsValueToAdd(int amountToAdd)
    {
        TargetCoinsValue = CurrentCoinsValue + amountToAdd;
    }

    public void SetTargetCoinsValueToSpend(int amountToSpend)
    {
        TargetCoinsValue = CurrentCoinsValue - amountToSpend;
    }

    public void ApplyCoins()=> CurrentCoinsValue = TargetCoinsValue;
}
