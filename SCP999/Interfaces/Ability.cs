using Exiled.API.Features;
using UserSettings.ServerSpecific;

namespace Scp999.Interfaces;
public abstract class Ability : IAbility
{
    public virtual string Name { get; }
    public virtual string Description { get; }
    public virtual int KeyId { get; }
    public virtual float Cooldown { get; }
    public virtual void Register()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += OnKeybindActivateAbility;
    }

    public virtual void Unregister()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= OnKeybindActivateAbility;
    }

    private void OnKeybindActivateAbility(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
    {
        if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != this.KeyId || !keybindSetting.SyncIsPressed)
            return;
        
        if (!Player.TryGet(referenceHub, out Player player))
            return;
        
        this.ActivateAbility(player);
        Log.Debug($"[Ability] Activating the {this.Name.ToLower()} ability");
    }

    protected abstract void ActivateAbility(Player player);
}