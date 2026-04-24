using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Color playerColor = new Color(0.95f, 0.35f, 0.35f, 1f);
    [SerializeField] private float playerScale = 0.38f;

    private InputAction rollAction;

    private void Awake()
    {
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = CreateCircleSprite();
        sr.color = playerColor;
        sr.sortingOrder = 5;
        transform.localScale = Vector3.one * playerScale;

        rollAction = new InputAction("RollDice", InputActionType.Button);
        rollAction.AddBinding("<Keyboard>/space");
        rollAction.AddBinding("<Touchscreen>/touch*/tap");
        rollAction.performed += OnRollDice;
    }

    private void Start()
    {
        BoardGenerator board = FindFirstObjectByType<BoardGenerator>();
        if (board != null && board.Tiles.Count > 0)
        {
            Vector3 pos = board.Tiles[0].transform.position;
            pos.z = -0.1f;
            transform.position = pos;
        }
    }

    private void OnEnable()  => rollAction?.Enable();
    private void OnDisable() => rollAction?.Disable();

    private void OnRollDice(InputAction.CallbackContext ctx)
    {
        Debug.Log("Roll Dice!");
    }

    private static Sprite CreateCircleSprite()
    {
        const int size = 128;
        Texture2D tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Bilinear;
        Color[] pixels = new Color[size * size];
        float cx = size * 0.5f, cy = size * 0.5f, r = size * 0.44f;
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            float dist = Mathf.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));
            float alpha = 1f - Mathf.Clamp01((dist - (r - 1.5f)) / 2.5f);
            pixels[y * size + x] = new Color(1f, 1f, 1f, alpha);
        }
        tex.SetPixels(pixels);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
    }
}
