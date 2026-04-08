using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttackAnimationEnemy : MonoBehaviour
{
    public Image targetImage;   // 点滅させるキャラ
    public float moveDistance = -100f;
    public float moveTime = 0.15f;

    // 攻撃アニメーション
    public IEnumerator Attack()
    {
        Vector3 startPos = transform.localPosition;
        Vector3 attackPos = startPos + new Vector3(moveDistance, 0, 0);

        float time = 0;

        // 前に動く
        while (time < moveTime)
        {
            transform.localPosition = Vector3.Lerp(startPos, attackPos, time / moveTime);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0;

        // 元に戻る
        while (time < moveTime)
        {
            transform.localPosition = Vector3.Lerp(attackPos, startPos, time / moveTime);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;

        // 攻撃された側を点滅
        yield return StartCoroutine(Flash());
    }

    // 点滅処理
    IEnumerator Flash()
    {
        for (int i = 0; i < 4; i++)
        {
            targetImage.enabled = false;
            yield return new WaitForSeconds(0.1f);

            targetImage.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator Shake()
    {
        Vector3 startPos = transform.localPosition;

        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(-5f, 5f);
            float y = Random.Range(-5f, 5f);

            transform.localPosition = startPos + new Vector3(x, y, 0);

            yield return new WaitForSeconds(0.03f);
        }

        transform.localPosition = startPos;
    }
}