using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text damageText;

    public void SetDamageText(int damage)
    {
        damageText.text = damage.ToString();
    }

    public void PlayAnimation()
    {
        StartCoroutine(AnimateDamageText());
    }

    private IEnumerator AnimateDamageText()
    {
        // Поднимаем текст вверх
        float startY = transform.position.y;
        float targetY = startY + 1f; // Расстояние подъема
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f; // Скорость анимации
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(startY, targetY, t), transform.position.z);
            yield return null;
        }
        Destroy(gameObject);
    }
}