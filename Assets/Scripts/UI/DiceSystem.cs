using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceSystem : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite[] rollAnimFrames;
    [SerializeField] private Sprite[] numberSprites;

    [Header("UI References")]
    [SerializeField] private DiceFace die1;
    [SerializeField] private DiceFace die2;
    [SerializeField] private Button   rollButton;
    [SerializeField] private Text     resultText;
    [SerializeField] private GameObject dicePanel;

    public event Action<int> OnRollComplete;

    private bool isRolling;

    private void Start()
    {
        rollButton.onClick.AddListener(OnRollButtonPressed);
        dicePanel.SetActive(false);
        resultText.gameObject.SetActive(false);
    }

    // Called by PlayerController when movement finishes
    public void UnlockRoll()
    {
        isRolling              = false;
        rollButton.interactable = true;
    }

    private void OnRollButtonPressed()
    {
        if (isRolling) return;
        StartCoroutine(RollSequence());
    }

    private IEnumerator RollSequence()
    {
        isRolling              = true;
        rollButton.interactable = false;
        resultText.gameObject.SetActive(false);
        dicePanel.SetActive(true);

        int r1    = UnityEngine.Random.Range(1, 7);
        int r2    = UnityEngine.Random.Range(1, 7);
        int total = r1 + r2;

        float die1Duration = UnityEngine.Random.Range(1.1f, 1.5f);
        float die2Duration = die1Duration + UnityEngine.Random.Range(0.3f, 0.55f);

        StartCoroutine(die1.PlayRoll(rollAnimFrames, numberSprites[r1 - 1], die1Duration));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(die2.PlayRoll(rollAnimFrames, numberSprites[r2 - 1], die2Duration));

        yield return new WaitForSeconds(die2Duration + 0.45f);

        yield return StartCoroutine(ShowResult(r1, r2, total));

        // Let player read the result, then clear the board before movement starts
        yield return new WaitForSeconds(0.55f);
        yield return StartCoroutine(FadeOutDice());

        // Fire event — PlayerController moves, then calls UnlockRoll() when done
        OnRollComplete?.Invoke(total);
    }

    private IEnumerator FadeOutDice()
    {
        var panelGroup  = dicePanel.GetComponent<CanvasGroup>();
        var resultGroup = resultText.gameObject.GetComponent<CanvasGroup>();

        float elapsed = 0f;
        const float duration = 0.3f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - elapsed / duration;
            if (panelGroup  != null) panelGroup.alpha  = alpha;
            if (resultGroup != null) resultGroup.alpha = alpha;
            yield return null;
        }

        dicePanel.SetActive(false);
        resultText.gameObject.SetActive(false);

        if (panelGroup  != null) panelGroup.alpha  = 1f;
        if (resultGroup != null) resultGroup.alpha = 1f;
    }

    private IEnumerator ShowResult(int r1, int r2, int total)
    {
        resultText.gameObject.SetActive(true);
        resultText.transform.localScale = Vector3.one;

        resultText.text  = $"{r1}  +  {r2}";
        resultText.color = new Color(1f, 0.85f, 0.85f);
        yield return new WaitForSeconds(0.5f);

        resultText.color = new Color(1f, 0.9f, 0.15f);
        for (int i = 1; i <= total; i++)
        {
            resultText.text = $"= {i}";
            yield return new WaitForSeconds(i <= 3 ? 0.12f : 0.065f);
        }

        resultText.text = $"MOVE  {total}!";
        yield return StartCoroutine(PunchScale(resultText.rectTransform));
    }

    private IEnumerator PunchScale(RectTransform rt)
    {
        Vector3 original = rt.localScale;
        float   elapsed  = 0f;
        const float duration = 0.4f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float s = 1f + Mathf.Sin(t * Mathf.PI) * 0.7f * (1f - t * 0.4f);
            rt.localScale = original * s;
            yield return null;
        }
        rt.localScale = original;
    }
}
