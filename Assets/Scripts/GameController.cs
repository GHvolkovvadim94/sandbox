
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Player player;
    private Enemy enemy;
    private int level = 1;
    private UIController uiController;
    private float autoAttackTimer; // Таймер для автоматического нанесения урона

    private void Start()
    {
        uiController = TryGetComponent(out UIController result) ? result :
            throw new System.Exception("Component not found");

        player = new Player(1, 0);
        enemy = new Enemy(15, 5);
        autoAttackTimer = player.CurrentAutoAttackIntervalValue;

        enemy.OnUpdateHealth += uiController.UpdateEnemyHealthUI;

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
            autoAttackTimer = player.CurrentAutoAttackIntervalValue;
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
        player.CoinManager.SetTargetCoinsValueToAdd(enemy.RewardValue); // Устанавливаем награду для зачисления на баланс
        uiController.UpdateCoinsUIAnimated(player.CoinManager.CurrentCoinsValue, player.CoinManager.TargetCoinsValue); // Проигрываем анимацию
        player.CoinManager.ApplyCoins(); //Применяем награду
    }

    public void UpgradeDamage()
    {
        // Метод вызывается при клике на кнопку улучшения урона
        player.UpgradeDamage(10,1);
        uiController.UpdateCoinsUIAnimated(player.CoinManager.CurrentCoinsValue, player.CoinManager.TargetCoinsValue);
        uiController.UpdateClickDamageUI(player.Damage);
        player.CoinManager.ApplyCoins(); //Применяем списание


    }

    public void UpgradeAutoAttack()
    {
        // Метод вызывается при клике на кнопку улучшения автоатаки
        player.UpgradeAutoAttack(15,0.2f);
        uiController.UpdateCoinsUIAnimated(player.CoinManager.CurrentCoinsValue, player.CoinManager.TargetCoinsValue);
        uiController.UpdateAutoAttackIntervalUI(player.CurrentAutoAttackIntervalValue, player.MaxAutoAttackIntervalValue);
        player.CoinManager.ApplyCoins(); //Применяем списание


    }

    private void UpdateUI()
    {
        uiController.UpdateEnemyHealthUI(enemy.CurrentHealth, enemy.MaxHealth);
        uiController.UpdateLevelUI(level);
        uiController.InstantUpdateCoinsUI(player.CoinManager.CurrentCoinsValue);
        uiController.UpdateClickDamageUI(player.Damage);
        uiController.UpdateAutoAttackIntervalUI(player.CurrentAutoAttackIntervalValue, player.MaxAutoAttackIntervalValue);


    }
}

