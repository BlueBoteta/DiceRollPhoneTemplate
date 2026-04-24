using UnityEngine;

[CreateAssetMenu(fileName = "NewTile", menuName = "Rot&Roll/Data/Tile")]
public class TileData : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string tileName;
    [SerializeField] private TileType tileType;
    [SerializeField] private Sprite icon;
    [SerializeField] private Color tileColor = Color.white;

    [Header("Spawn Weight")]
    [Tooltip("Higher = more likely to appear on the board")]
    [SerializeField] private float spawnWeight = 1f;
    [SerializeField] private bool availableFromLoopOne = true;
    [SerializeField] private int availableFromLoop = 1;

    [Header("Infection Tile")]
    [SerializeField] private float infectionAmount;

    public string TileName => tileName;
    public TileType TileType => tileType;
    public Sprite Icon => icon;
    public Color TileColor => tileColor;
    public float SpawnWeight => spawnWeight;
    public bool AvailableFromLoopOne => availableFromLoopOne;
    public int AvailableFromLoop => availableFromLoop;
    public float InfectionAmount => infectionAmount;
}

public enum TileType
{
    Encounter,
    Scavenge,
    Trader,
    SafeRoom,
    StoryTile,
    InfectionTile,
    Trap,
    SOSSignal,
    Event
}
