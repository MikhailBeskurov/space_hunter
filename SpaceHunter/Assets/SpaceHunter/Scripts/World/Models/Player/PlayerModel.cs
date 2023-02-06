using Utils.Update;
using World.Core;
using World.Core.Model;
using SpaceHunter.Scripts.Modules.Player.Movement;
using SpaceHunter.Scripts.Modules.Weapon;
using SpaceHunter.Scripts.World.Views.Player;
using UniRx;
using UnityEngine;

namespace SpaceHunter.Scripts.World.Models.Player
{
    public class PlayerModel : AbstractViewModel, IUpdatable
    {
        public IReadOnlyReactiveProperty<Vector2> VelocityMovement { get; }
        public IReadOnlyReactiveProperty<Vector3> MousePosition => _mousePosition;
        
        private ReactiveProperty<Vector3> _mousePosition = new ReactiveProperty<Vector3>();
        private WorldManager _worldManager;

        public PlayerModel(IPlayerMovementModule movementModule, WorldManager worldManager)
        {
            _worldManager = worldManager;
            VelocityMovement = movementModule.VelocityMovement;
        }

        public void Update(float deltaTime)
        {
            _mousePosition.Value = _worldManager.GetViewInstance<CrosshairView>().transform.position;
        }
    }
}