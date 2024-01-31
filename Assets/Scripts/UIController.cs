using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text enemyHealthValueText;
    [SerializeField]
    private TMP_Text coinsValueText;
    [SerializeField]
    private TMP_Text levelValueText;
    [SerializeField]
    private TMP_Text autoAttackIntervalValueText;
    [SerializeField]
    private TMP_Text clickDamageValueText;
    [SerializeField]
    private Slider enemyHealthBar;
    [SerializeField]
    private Coroutine coinAnimationCoroutine;

    public void UpdateEnemyHealthUI(int health, int maxHealth)
    {
        enemyHealthValueText.text = $"{health}/{maxHealth}";
        float healthPercentage = (float)health / maxHealth;
        enemyHealthBar.value = healthPercentage;
    }

    public void InstantUpdateCoinsUI(int coins)
    {
        coinsValueText.text = "Coins: " + coins.ToString();
    }

    public void UpdateCoinsUIAnimated(int currentCoinsValue, int targetCoinsValue)
    {
        if (coinAnimationCoroutine != null)
        {
            StopCoroutine(coinAnimationCoroutine);
        }

        coinAnimationCoroutine = StartCoroutine(AnimateCoinCount());

        IEnumerator AnimateCoinCount()
        {
            float animationSpeed = 500f;

            if (currentCoinsValue < targetCoinsValue)
            {
                while (currentCoinsValue < targetCoinsValue)
                {
                    currentCoinsValue++;
                    coinsValueText.text = $"{CoinsUIFormatter.Format(currentCoinsValue)}";
                    yield return new WaitForSeconds(1f / animationSpeed);
                }
            }
            else if (currentCoinsValue > targetCoinsValue)
            {
                while (currentCoinsValue > targetCoinsValue)
                {
                    currentCoinsValue--;
                    coinsValueText.text = $"{CoinsUIFormatter.Format(currentCoinsValue)}";
                    yield return new WaitForSeconds(1f / animationSpeed);
                }
            }
        }
    }

    public void UpdateLevelUI(int level)
    {
        levelValueText.text = "Level: " + level.ToString();
    }

    public void UpdateAutoAttackIntervalUI(float currentValue, float maxValue)
    {
        autoAttackIntervalValueText.text = (currentValue <= maxValue) ? "MAX" : currentValue.ToString("F1");
    }
    public void UpdateClickDamageUI(int damage)
    {
        clickDamageValueText.text = damage.ToString();
    }

}

