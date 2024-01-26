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
        // ��������� ����� �����
        float startY = transform.position.y;
        float targetY = startY + 1f; // ���������� �������
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f; // �������� ��������
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(startY, targetY, t), transform.position.z);
            yield return null;
        }
        Destroy(gameObject);
    }
}