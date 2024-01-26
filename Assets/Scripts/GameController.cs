
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Player player;
    private Enemy enemy;
    private int level = 1;
    private UIController uiController;
    private float autoAttackTimer;
    private void Start()
    {
        uiController = TryGetComponent(out UIController result) ? result :
            throw new System.Exception("Component not found");

        player = new Player(1, 0);
        enemy = new Enemy(15, 5);
        autoAttackTimer = player.CurrentAutoAttackIntervalValue;

        enemy.OnUpdateHealth += uiController.UpdateEnemyHealthUI;
        uiController.InstantUpdateCoinsUI(player.CoinManager.CurrentCoinsValue);
        UpdateUI();
    }

    private void Update()
    {
        autoAttackTimer -= Time.deltaTime;
        if (autoAttackTimer <= 0)
        {
            AttackAndTryNext();
            autoAttackTimer = player.CurrentAutoAttackIntervalValue;
        }
    }
    public void AttackAndTryNext()
    {
        player.Attack(enemy);

        if (enemy.IsDefeated())
        {
            GetCoinsReward();
            enemy = enemy.NextEnemy();
            level++;
        }
        UpdateUI();
    }

    private void GetCoinsReward()
    {
        player.CoinManager.SetTargetCoinsValueToAdd(enemy.RewardValue);
        uiController.UpdateCoinsUIAnimated(player.CoinManager.CurrentCoinsValue, player.CoinManager.TargetCoinsValue);
        player.CoinManager.ApplyCoins();
    }

    public void UpgradeDamage()
    {
        player.UpgradeDamage(10, 1);
        uiController.UpdateCoinsUIAnimated(player.CoinManager.CurrentCoinsValue, player.CoinManager.TargetCoinsValue);
        uiController.UpdateClickDamageUI(player.Damage);
        player.CoinManager.ApplyCoins();
    }

    public void UpgradeAutoAttack()
    {
        player.UpgradeAutoAttack(15, 0.2f);
        uiController.UpdateCoinsUIAnimated(player.CoinManager.CurrentCoinsValue, player.CoinManager.TargetCoinsValue);
        uiController.UpdateAutoAttackIntervalUI(player.CurrentAutoAttackIntervalValue, player.MaxAutoAttackIntervalValue);
        player.CoinManager.ApplyCoins();
    }

    private void UpdateUI()
    {
        uiController.UpdateEnemyHealthUI(enemy.CurrentHealth, enemy.MaxHealth);
        uiController.UpdateLevelUI(level);
        uiController.UpdateAutoAttackIntervalUI(player.CurrentAutoAttackIntervalValue, player.MaxAutoAttackIntervalValue);
    }
}

