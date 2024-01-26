[System.Serializable]
public class Enemy
{
    public delegate void UpdateHealthEventHandler(int currentHealth, int maxHealth);
    public event UpdateHealthEventHandler OnUpdateHealth;

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    public int RewardValue { get; private set; }

    public Enemy(int maxHealth, int reward)
    {
        CurrentHealth = maxHealth;
        MaxHealth = maxHealth;
        RewardValue = reward;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        UpdateUI();
    }

    public bool IsDefeated() => CurrentHealth <= 0;

    public Enemy NextEnemy() => new Enemy(MaxHealth + 20, RewardValue + 10);

    private void UpdateUI()
    {
        OnUpdateHealth?.Invoke(CurrentHealth,MaxHealth);
    }
}
