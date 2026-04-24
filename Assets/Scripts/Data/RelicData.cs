using UnityEngine;

[CreateAssetMenu(fileName = "NewRelic", menuName = "Rot&Roll/Data/Relic")]
public class RelicData : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string relicName;
    [SerializeField] private Sprite icon;
    [TextArea] [SerializeField] private string flavorText;
    [TextArea] [SerializeField] private string effectDescription;

    [Header("Trigger")]
    [SerializeField] private RelicTrigger trigger;
    [SerializeField] private int triggerOnDiceValue;

    [Header("Effect")]
    [SerializeField] private RelicEffect effect;
    [SerializeField] private float effectValue;
    [SerializeField] private bool isPercentage;

    [Header("Economy")]
    [SerializeField] private int bloodSampleCost;

    public string RelicName => relicName;
    public Sprite Icon => icon;
    public string FlavorText => flavorText;
    public string EffectDescription => effectDescription;
    public RelicTrigger Trigger => trigger;
    public int TriggerOnDiceValue => triggerOnDiceValue;
    public RelicEffect Effect => effect;
    public float EffectValue => effectValue;
    public bool IsPercentage => isPercentage;
    public int BloodSampleCost => bloodSampleCost;
}

public enum RelicTrigger
{
    Passive,
    OnDiceRoll,
    OnKill,
    OnLoopComplete,
    OnLowHealth,
    OnInfected,
    OnTileEnter
}

public enum RelicEffect
{
    BonusBrutality,
    BonusFortitude,
    BonusVitality,
    SlowInfection,
    SurviveKillingBlow,
    DoubleDiceRoll,
    HealOnKill,
    DealDamageOnRoll,
    DoubleRollEffects,
    RevealStoryTiles
}
