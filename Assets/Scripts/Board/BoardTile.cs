using UnityEngine;

public class BoardTile : MonoBehaviour
{
    [SerializeField] private int tileIndex;
    [SerializeField] private TileType tileType = TileType.Encounter;
    [SerializeField] private bool isStartTile;

    private SpriteRenderer fillRenderer;
    private Color defaultColor;

    public int TileIndex => tileIndex;
    public TileType TileType => tileType;
    public bool IsStartTile => isStartTile;

    public void Initialize(int index, bool startTile, Color color)
    {
        tileIndex = index;
        isStartTile = startTile;
        fillRenderer = GetComponent<SpriteRenderer>();
        defaultColor = color;
        fillRenderer.color = color;
    }

    public void Highlight(Color highlightColor)
    {
        if (fillRenderer != null)
            fillRenderer.color = highlightColor;
    }

    public void ResetColor()
    {
        if (fillRenderer != null)
            fillRenderer.color = defaultColor;
    }

    public void SetTileType(TileType type)
    {
        tileType = type;
    }
}
