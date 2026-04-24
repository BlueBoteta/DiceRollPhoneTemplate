using UnityEngine;
using System.Collections.Generic;

// Reference: Assets/References/Tiles camera and rotation.png
// Board is a diamond-shaped loop of isometric tiles. Each tile is a rhombus (2:1 width:height).
// Camera is top-down isometric. Grid formula: worldX = (col-row)*cell/2, worldY = (col+row)*cell/4.
public class BoardGenerator : MonoBehaviour
{
    [Header("Tile Sprite")]
    [Tooltip("Optional — leave empty to use the auto-generated isometric diamond sprite")]
    [SerializeField] private Sprite tileSprite;

    [Header("Board Colors")]
    [SerializeField] private Color tileColor          = new Color(0.20f, 0.20f, 0.22f, 1f);
    [SerializeField] private Color tileOutlineColor   = new Color(0.50f, 0.50f, 0.55f, 1f);
    [SerializeField] private Color startTileColor     = new Color(0.10f, 0.75f, 0.20f, 1f);
    [SerializeField] private Color tileHighlightColor = new Color(0.95f, 0.75f, 0.10f, 1f);

    [Header("Board Layout")]
    [Tooltip("Tiles per side — total tiles = tilesPerSide x 4")]
    [SerializeField] private int tilesPerSide = 10;
    [Tooltip("Visual width of one tile in world units")]
    [SerializeField] private float tileSize = 1.3f;
    [Tooltip("Gap between adjacent tiles")]
    [SerializeField] private float tileGap = 0.05f;
    [Tooltip("How much larger the outline is (1.15 = 15% bigger than fill)")]
    [SerializeField] private float outlineScale = 1.15f;

    private readonly List<BoardTile> tiles = new List<BoardTile>();
    private GameObject boardRoot;
    private Sprite cachedDiamond;

    public IReadOnlyList<BoardTile> Tiles => tiles;
    public Color TileHighlightColor => tileHighlightColor;

    private void Awake() => GenerateBoard();

    [ContextMenu("Generate Board")]
    public void GenerateBoard()
    {
        ClearBoard();
        cachedDiamond = null;

        boardRoot = new GameObject("BoardTiles");
        boardRoot.transform.SetParent(transform);
        boardRoot.transform.localPosition = Vector3.zero;

        List<Vector2Int> path = BuildLoopPath();
        Vector2 center = ComputeBoardCenter();

        for (int i = 0; i < path.Count; i++)
        {
            bool isStart = (i == 0);
            Color col = isStart ? startTileColor : tileColor;
            tiles.Add(SpawnTile(IsoToWorld(path[i], center), i, isStart, col));
        }
    }

    // Clockwise loop of 40 tiles (10 per side, no shared corners).
    // Tile[0] at grid (0,0) = bottom tip of the diamond board = START tile.
    private List<Vector2Int> BuildLoopPath()
    {
        int n = tilesPerSide;
        var path = new List<Vector2Int>(n * 4);
        for (int x = 0; x < n; x++) path.Add(new Vector2Int(x, 0));  // bottom-right edge
        for (int y = 0; y < n; y++) path.Add(new Vector2Int(n, y));  // right edge
        for (int x = n; x > 0; x--) path.Add(new Vector2Int(x, n)); // top-left edge
        for (int y = n; y > 0; y--) path.Add(new Vector2Int(0, y)); // left edge
        return path;
    }

    // Classic isometric grid-to-world.
    // Each tile is 1 unit wide and 0.5 units tall (2:1 ratio from the diamond sprite).
    private Vector3 IsoToWorld(Vector2Int g, Vector2 center)
    {
        float cell = tileSize + tileGap;
        float wx = (g.x - g.y) * cell * 0.5f;
        float wy = (g.x + g.y) * cell * 0.25f;
        return new Vector3(wx - center.x, wy - center.y, 0f);
    }

    // Isometric world position of the grid center (n/2, n/2)
    private Vector2 ComputeBoardCenter()
    {
        float cell = tileSize + tileGap;
        float n = tilesPerSide;
        // worldX of (n/2, n/2) = (n/2 - n/2)*cell/2 = 0
        // worldY of (n/2, n/2) = (n/2 + n/2)*cell/4 = n*cell/4
        return new Vector2(0f, n * cell * 0.25f);
    }

    private BoardTile SpawnTile(Vector3 pos, int index, bool isStart, Color color)
    {
        // Root GameObject holds the fill SpriteRenderer and the BoardTile component
        GameObject go = new GameObject($"Tile_{index:D2}");
        go.transform.SetParent(boardRoot.transform);
        go.transform.position = pos;
        go.transform.localScale = new Vector3(tileSize, tileSize, 1f);

        SpriteRenderer fill = go.AddComponent<SpriteRenderer>();
        fill.sprite = GetDiamond();
        fill.sortingOrder = 1;

        // Outline: child of root, scaled larger, rendered behind the fill
        GameObject outlineGO = new GameObject("Outline");
        outlineGO.transform.SetParent(go.transform);
        outlineGO.transform.localPosition = Vector3.zero;
        outlineGO.transform.localScale = Vector3.one * outlineScale;

        SpriteRenderer outlineSR = outlineGO.AddComponent<SpriteRenderer>();
        outlineSR.sprite = GetDiamond();
        outlineSR.color = tileOutlineColor;
        outlineSR.sortingOrder = 0;

        BoardTile tile = go.AddComponent<BoardTile>();
        tile.Initialize(index, isStart, color);
        return tile;
    }

    // Generates a proper isometric diamond sprite: 128x64 pixels (2:1 ratio).
    // PPU = 128 so the sprite is exactly 1 unit wide and 0.5 units tall in world space.
    private Sprite GetDiamond()
    {
        if (tileSprite != null) return tileSprite;
        if (cachedDiamond != null) return cachedDiamond;

        const int w = 128, h = 64;
        Texture2D tex = new Texture2D(w, h, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Bilinear;
        tex.wrapMode = TextureWrapMode.Clamp;

        Color[] pixels = new Color[w * h];
        for (int y = 0; y < h; y++)
        for (int x = 0; x < w; x++)
        {
            float nx = (x + 0.5f) / w - 0.5f;
            float ny = (y + 0.5f) / h - 0.5f;
            pixels[y * w + x] = Mathf.Abs(nx) + Mathf.Abs(ny) < 0.48f
                ? Color.white
                : Color.clear;
        }

        tex.SetPixels(pixels);
        tex.Apply();
        cachedDiamond = Sprite.Create(tex, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f), w);
        return cachedDiamond;
    }

    [ContextMenu("Clear Board")]
    private void ClearBoard()
    {
        foreach (BoardTile t in tiles)
            if (t != null) DestroyImmediate(t.gameObject);
        tiles.Clear();
        if (boardRoot != null) DestroyImmediate(boardRoot);
        boardRoot = null;
    }
}
