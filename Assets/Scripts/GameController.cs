
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Player player;
    private Enemy enemy;
    private int level = 1;
    private UIController uiController;
    private float autoAttackTimer; // Таймер для автоматического нанесения урона

    public delegate void UpdateLevelEventHandler(int level);
    //public event UpdateLevelEventHandler OnUpdateLevel;

    private void Start()
    {
        player = new Player(1);
        enemy = new Enemy(15,5);
        autoAttackTimer = player.AutoAttackInterval;
        uiController = GetComponent<UIController>();
        if (uiController == null)
        {
            Debug.LogError("UIController not found on GameController.");
        }
        else
        {
            enemy.OnUpdateHealth += uiController.UpdateHealth;
            uiController.UpdateCoinsInstant(player.CoinManager.CurrentCoins);
        }

        UpdateUI();
    }

    private void Update()
    {
        // Обновляем таймер каждый кадр
        autoAttackTimer -= Time.deltaTime;

        // Проверяем, прошло ли достаточно времени для автоматической атаки
        if (autoAttackTimer <= 0)
        {
            // Выполняем автоматическую атаку
            AttackAndTryNext();

            // Сбрасываем таймер
            autoAttackTimer = player.AutoAttackInterval;
        }
    }
    public void AttackAndTryNext()
    {
        player.Attack(enemy);

        if (enemy.IsDefeated())
        {
            GetCoinsReward();// Получаем награду
            enemy = enemy.NextEnemy(); // Обновляем врага
            level++; // Идём на следующий уровень
        }
        // Обновить UI после атаки и проверки
        UpdateUI();
    }

    private void GetCoinsReward()
    {
        player.CoinManager.SetTargetCoinsToAdd(enemy.Reward); // Устанавливаем награду для зачисления на баланс
        uiController.UpdateCoinsUIAnimated(player.CoinManager.CurrentCoins, player.CoinManager.TargetCoins); // Проигрываем анимацию
        player.CoinManager.ApplyCoins(); //Применяем награду
    }

    public void UpgradeDamage()
    {
        // Метод вызывается при клике на кнопку улучшения урона
        player.UpgradeDamage(10);
        uiController.UpdateCoinsUIAnimated(player.CoinManager.CurrentCoins, player.CoinManager.TargetCoins);
        uiController.UpdateDamageValueText(player.GetCurrentDamage());
        player.CoinManager.ApplyCoins(); //Применяем списание


    }

    public void UpgradeAutoAttack()
    {
        // Метод вызывается при клике на кнопку улучшения автоатаки
        player.UpgradeAutoAttack(15);
        uiController.UpdateCoinsUIAnimated(player.CoinManager.CurrentCoins, player.CoinManager.TargetCoins);
        uiController.UpdateAutoAttackCDValueText(player.AutoAttackInterval, player.MaxAutoAttackInterval);
        player.CoinManager.ApplyCoins(); //Применяем списание


    }

    private void UpdateUI()
    {
        uiController.UpdateHealth(enemy.Health, enemy.MaxHealth);
        uiController.UpdateLevelValueText(level);
        //uiController.UpdateCoinsUIInstant(player.CoinManager.CurrentCoins);
        uiController.UpdateDamageValueText(player.GetCurrentDamage());
        uiController.UpdateAutoAttackCDValueText(player.AutoAttackInterval, player.MaxAutoAttackInterval);


    }
}

