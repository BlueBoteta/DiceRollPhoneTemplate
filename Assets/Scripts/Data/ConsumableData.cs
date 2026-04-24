using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Rot&Roll/Data/Consumable")]
public class ConsumableData : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string consumableName;
    [SerializeField] private Sprite icon;
    [TextArea] [SerializeField] private string description;
    [SerializeField] private ConsumableType consumableType;

    [Header("Effect Values")]
    [SerializeField] private int healAmount;
    [SerializeField] private float infectionReduction;
    [SerializeField] private int bonusDamageThisFight;
    [SerializeField] private int fortitudeBoost;
    [SerializeField] private int fortitudeBoostDuration;

    public string ConsumableName => consumableName;
    public Sprite Icon => icon;
    public string Description => description;
    public ConsumableType ConsumableType => consumableType;
    public int HealAmount => healAmount;
    public float InfectionReduction => infectionReduction;
    public int BonusDamageThisFight => bonusDamageThisFight;
    public int FortitudeBoost => fortitudeBoost;
    public int FortitudeBoostDuration => fortitudeBoostDuration;
}

public enum ConsumableType
{
    Medkit,
    AdrenalineShot,
    Molotov,
    Bandage,
    Painkillers
}
