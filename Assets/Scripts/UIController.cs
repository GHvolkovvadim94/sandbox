using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text enemyHealthValueText;
    [SerializeField]
    private Text coinsValueText;
    [SerializeField]
    private Text levelValueText;
    [SerializeField]
    private Text autoAttackIntervalValueText;
    [SerializeField]
    private Text clickDamageValueText;
    private Coroutine coinAnimationCoroutine;

    public void UpdateEnemyHealthUI(int health, int maxHealth)
    {
        enemyHealthValueText.text = "Enemy Health: " + health.ToString();
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
        clickDamageValueText.text += damage.ToString();
    }

}

