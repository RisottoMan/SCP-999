namespace SCP999;
public class AbilityConfig
{
    public float Range { get; set; }
    public float EffectDuration { get; set; }
    public ItemType ItemType { get; set; }
    public AbilityConfig() { }
    public AbilityConfig(float range, float effectDuration, ItemType itemType)
    {
        Range = range;
        EffectDuration = effectDuration;
        ItemType = itemType;
    }
}