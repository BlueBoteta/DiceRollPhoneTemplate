using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Rot&Roll/Data/Item")]
public class ItemData : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite icon;
    [TextArea] [SerializeField] private string flavorText;
    [SerializeField] private ItemType itemType;
    [SerializeField] private ItemRarity rarity;

    [Header("Stats")]
    [SerializeField] private int brutality;
    [SerializeField] private int fortitude;
    [SerializeField] private int vitality;
    [SerializeField] private float critChance;
    [SerializeField] private float lifesteal;
    [SerializeField] private int trueDamage;

    [Header("Special Effect")]
    [SerializeField] private StatusEffect specialEffect;
    [SerializeField] private float specialEffectChance;

    [Header("Economy")]
    [SerializeField] private int scrapValue;

    public string ItemName => itemName;
    public Sprite Icon => icon;
    public string FlavorText => flavorText;
    public ItemType ItemType => itemType;
    public ItemRarity Rarity => rarity;
    public int Brutality => brutality;
    public int Fortitude => fortitude;
    public int Vitality => vitality;
    public float CritChance => critChance;
    public float Lifesteal => lifesteal;
    public int TrueDamage => trueDamage;
    public StatusEffect SpecialEffect => specialEffect;
    public float SpecialEffectChance => specialEffectChance;
    public int ScrapValue => scrapValue;
}

public enum ItemType
{
    Weapon,
    Armor,
    Helmet,
    Accessory
}

public enum ItemRarity
{
    Common,
    Scarce,
    Salvaged,
    MilitaryGrade,
    PreCollapse,
    Eternal
}

public enum StatusEffect
{
    None,
    Bleed,
    Burn,
    Poison,
    Stun
}
