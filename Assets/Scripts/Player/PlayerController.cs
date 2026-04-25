using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Color playerColor  = new Color(0.95f, 0.35f, 0.35f, 1f);
    [SerializeField] private float playerScale  = 0.38f;
    [SerializeField] private float jumpHeight   = 0.7f;
    [SerializeField] private float jumpDuration = 0.27f;

    private InputAction      rollAction;
    private BoardGenerator   board;
    private DiceSystem       diceSystem;
    private CameraController cameraController;
    private int              currentTileIndex = 0;
    private bool             isMoving         = false;
    private Vector3          baseScale;

    private void Awake()
    {
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite       = CreateCircleSprite();
        sr.color        = playerColor;
        sr.sortingOrder = 5;

        transform.localScale = Vector3.one * playerScale;
        baseScale            = transform.localScale;

        rollAction = new InputAction("RollDice", InputActionType.Button);
        rollAction.AddBinding("<Keyboard>/space");
        rollAction.AddBinding("<Touchscreen>/touch*/tap");
        rollAction.performed += ctx => Debug.Log("Roll Dice!");
    }

    private void Start()
    {
        board            = FindFirstObjectByType<BoardGenerator>();
        diceSystem       = FindFirstObjectByType<DiceSystem>();
        cameraController = FindFirstObjectByType<CameraController>();

        if (board != null && board.Tiles.Count > 0)
        {
            Vector3 pos = board.Tiles[0].transform.position;
            pos.z       = -0.1f;
            transform.position = pos;
        }

        if (diceSystem != null)
            diceSystem.OnRollComplete += MoveSteps;
    }

    private void OnEnable()  => rollAction?.Enable();
    private void OnDisable() => rollAction?.Disable();

    public void MoveSteps(int steps)
    {
        if (isMoving || board == null) return;

        // First move — zoom camera in to gameplay size
        cameraController?.ZoomToGameplay();

        StartCoroutine(MoveCoroutine(steps));
    }

    private IEnumerator MoveCoroutine(int steps)
    {
        isMoving = true;
        int total = board.Tiles.Count;

        for (int i = 0; i < steps; i++)
        {
            currentTileIndex = (currentTileIndex - 1 + total) % total;
            var tile = board.Tiles[currentTileIndex];

            Vector3 target = tile.transform.position;
            target.z = -0.1f;

            yield return StartCoroutine(JumpTo(transform.position, target));

            tile.Highlight(board.TileHighlightColor);
            yield return new WaitForSeconds(0.1f);
            tile.ResetColor();

            yield return new WaitForSeconds(0.03f);
        }

        isMoving = false;

        // Re-enable the roll button now that movement is done
        diceSystem?.UnlockRoll();
    }

    private IEnumerator JumpTo(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;
            float t      = Mathf.Clamp01(elapsed / jumpDuration);
            float eased  = t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;

            Vector3 pos  = Vector3.Lerp(from, to, eased);
            pos.y       += Mathf.Sin(t * Mathf.PI) * jumpHeight;
            transform.position = pos;

            Vector3 dir  = to - from;
            float   tilt = Mathf.Sin(t * Mathf.PI) * 18f * Mathf.Sign(dir.x != 0 ? dir.x : dir.y);
            transform.rotation = Quaternion.Euler(0f, 0f, tilt);

            float stretchY = 1f + Mathf.Sin(t * Mathf.PI * 0.5f) * 0.25f;
            float stretchX = 1f - Mathf.Sin(t * Mathf.PI * 0.5f) * 0.12f;
            transform.localScale = new Vector3(baseScale.x * stretchX, baseScale.y * stretchY, baseScale.z);

            yield return null;
        }

        transform.position     = to;
        transform.rotation     = Quaternion.identity;

        yield return StartCoroutine(LandSquash());
    }

    private IEnumerator LandSquash()
    {
        const float duration = 0.13f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed  += Time.deltaTime;
            float t   = elapsed / duration;
            transform.localScale = new Vector3(
                baseScale.x * (1f + Mathf.Sin(t * Mathf.PI) * 0.28f),
                baseScale.y * (1f - Mathf.Sin(t * Mathf.PI) * 0.38f),
                baseScale.z);
            yield return null;
        }
        transform.localScale = baseScale;
    }

    private static Sprite CreateCircleSprite()
    {
        const int size = 128;
        Texture2D tex  = new Texture2D(size, size, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Bilinear;
        Color[] pixels = new Color[size * size];
        float cx = size * 0.5f, cy = size * 0.5f, r = size * 0.44f;
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            float dist  = Mathf.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));
            float alpha = 1f - Mathf.Clamp01((dist - (r - 1.5f)) / 2.5f);
            pixels[y * size + x] = new Color(1f, 1f, 1f, alpha);
        }
        tex.SetPixels(pixels);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
    }
}
