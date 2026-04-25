using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DiceFace : MonoBehaviour
{
    private Image image;
    private RectTransform rt;

    public IEnumerator PlayRoll(Sprite[] rollFrames, Sprite resultSprite, float duration)
    {
        image = GetComponent<Image>();
        rt    = GetComponent<RectTransform>();
        Vector2 basePos = rt.anchoredPosition;

        image.sprite = rollFrames[0];
        image.color  = Color.white;

        yield return StartCoroutine(AnimateRoll(rollFrames, basePos, duration));

        // Snap to result
        rt.anchoredPosition = basePos;
        image.sprite = resultSprite;

        yield return StartCoroutine(PopBounce());

        // Brief gold flash on settle
        yield return StartCoroutine(ColorFlash(new Color(1f, 0.85f, 0.2f)));
    }

    private IEnumerator AnimateRoll(Sprite[] frames, Vector2 basePos, float duration)
    {
        const float fps = 12f;
        float frameInterval = 1f / fps;
        float elapsed = 0f;
        int frameIndex = 0;

        while (elapsed < duration)
        {
            float progress = elapsed / duration;
            image.sprite = frames[frameIndex % frames.Length];
            frameIndex++;

            // Shake: strong at start, fades out near the end
            float shake = Mathf.Lerp(14f, 0.5f, Mathf.Pow(progress, 0.6f));
            rt.anchoredPosition = basePos + new Vector2(
                Random.Range(-shake, shake),
                Random.Range(-shake, shake));

            // Slight rotation wiggle
            float angle = Mathf.Sin(elapsed * 25f) * Mathf.Lerp(8f, 0.5f, progress);
            rt.localEulerAngles = new Vector3(0f, 0f, angle);

            yield return new WaitForSeconds(frameInterval);
            elapsed += frameInterval;
        }

        rt.anchoredPosition    = basePos;
        rt.localEulerAngles    = Vector3.zero;
    }

    private IEnumerator PopBounce()
    {
        Vector3 original = transform.localScale;
        float elapsed = 0f;
        const float duration = 0.38f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            // Overshoot spring: goes past 1, bounces back
            float s = 1f + Mathf.Sin(t * Mathf.PI) * 0.55f * (1f - t * 0.5f);
            transform.localScale = original * s;
            yield return null;
        }
        transform.localScale = original;
    }

    private IEnumerator ColorFlash(Color flashColor)
    {
        float elapsed = 0f;
        const float duration = 0.25f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            image.color = Color.Lerp(flashColor, Color.white, elapsed / duration);
            yield return null;
        }
        image.color = Color.white;
    }
}
