using SpaceHunter.Scripts.Modules.Controls;
using UniRx;
using UnityEngine;

namespace SpaceHunter.Scripts.Modules.Player.Movement
{
    public interface IPlayerMovementModule
    {
        public IReadOnlyReactiveProperty<Vector2> VelocityMovement { get; }
    }

    public class PlayerMovementModule : IPlayerMovementModule
    {
        public IReadOnlyReactiveProperty<Vector2> VelocityMovement { get; }

        public PlayerMovementModule(IControlsMovement controlsMovement)
        {
            VelocityMovement = controlsMovement.VelocityMovement;
        }
    }
}