using Exiled.API.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace Scp999;
public class MoveComponent : MonoBehaviour
{
    void Awake()
    {
        if (!Player.TryGet(gameObject, out this._player))
            return;

        PlayerComponent component = _player.GameObject.GetComponent<PlayerComponent>();
        _schematic = component.GetCurrentSchematic;
        component.GetCurrentAudioPlayer.TryGetSpeaker("Scp999-Main", out _speaker);
        
        InvokeRepeating(nameof(UpdateEveryRate), 0f, 0.1f);
    }
    
    void UpdateEveryRate()
    {
        _schematic.transform.position = _player.GameObject.transform.position + new Vector3(0, -0.75f, 0);;
        _schematic.transform.rotation = _player.GameObject.transform.rotation;
        _speaker.transform.position = _player.GameObject.transform.position;
    }

    void OnDestroy()
    {
        CancelInvoke(nameof(UpdateEveryRate));
    }

    private Player _player;
    private SchematicObject _schematic;
    private Speaker _speaker;
}