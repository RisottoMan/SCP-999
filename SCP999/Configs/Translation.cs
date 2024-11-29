using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SCP999;
public sealed class Translation : ITranslation
{
    [Description("The hint when the ability is in cooldown.")]
    public string CoolDownText { get; set; } = "Ability cooldown: %cooldown seconds remaining.";

    [Description("The hint when the ability is ready.")]
    public string AbilityReadyText { get; set; } = "Ability ready!";
    
    [Description("The hint when the ability is error.")]
    public string AbilityErrorText { get; set; } = "There are no players nearby that can be cured.";
}