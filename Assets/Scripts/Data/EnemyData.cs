using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Rot&Roll/Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string enemyName;
    [SerializeField] private Sprite icon;
    [TextArea] [SerializeField] private string description;

    [Header("Stats")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int brutality;
    [SerializeField] private int fortitude;
    [SerializeField] private float critChance;

    [Header("Special")]
    [SerializeField] private EnemyAbility ability;
    [SerializeField] private bool appliesBleed;
    [SerializeField] private bool explodesOnDeath;
    [SerializeField] private bool callsReinforcements;

    [Header("Rewards")]
    [SerializeField] private int scrapDropMin;
    [SerializeField] private int scrapDropMax;
    [SerializeField] private float bloodSampleDropChance;
    [SerializeField] private float itemDropChance;

    public string EnemyName => enemyName;
    public Sprite Icon => icon;
    public string Description => description;
    public int MaxHealth => maxHealth;
    public int Brutality => brutality;
    public int Fortitude => fortitude;
    public float CritChance => critChance;
    public EnemyAbility Ability => ability;
    public bool AppliesBleed => appliesBleed;
    public bool ExplodesOnDeath => explodesOnDeath;
    public bool CallsReinforcements => callsReinforcements;
    public int ScrapDropMin => scrapDropMin;
    public int ScrapDropMax => scrapDropMax;
    public float BloodSampleDropChance => bloodSampleDropChance;
    public float ItemDropChance => itemDropChance;
}

public enum EnemyAbility
{
    None,
    Stalker,
    Screamer,
    Bloater,
    HordeSpawn
}
