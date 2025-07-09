using Exiled.API.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace Scp999.Features.Controller;
public class MovementController : MonoBehaviour
{
    public void Init(SchematicObject schematicObject, Speaker speaker, Vector3 offset)
    {
        _player = Player.Get(gameObject);
        _schematicObject = schematicObject;
        _speaker = speaker;
        _offset = offset;
        
        Log.Debug($"[ObjectController] Init the controller");
    }

    private void Update()
    {
        _schematicObject.transform.position = _player.GameObject.transform.position + _offset;
        _schematicObject.transform.rotation = _player.GameObject.transform.rotation;
        _speaker.transform.position = _player.GameObject.transform.position;
    }

    private void OnDestroy()
    {
        _schematicObject = null;
        _player = null;
        _speaker = null;
        
        Log.Debug($"[ObjectController] Destroy the controller");
    }

    private SchematicObject _schematicObject;
    private Player _player;
    private Speaker _speaker;
    private Vector3 _offset;
}