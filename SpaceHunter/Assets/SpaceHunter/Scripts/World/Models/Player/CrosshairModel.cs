using HoneyWood.Scripts.Utils.Update;
using HoneyWood.Scripts.World.Core;
using HoneyWood.Scripts.World.Core.Model;
using SpaceHunter.Scripts.Modules.Crosshair;
using SpaceHunter.Scripts.World.Views.Player;
using UniRx;
using UnityEngine;

namespace SpaceHunter.Scripts.World.Models.Player
{
    public class CrosshairModel : AbstractViewModel
    {
        public Vector3 PlayerPosition => _worldManager.GetViewInstance<PlayerView>().transform.position;
        public IReadOnlyReactiveProperty<Vector3> MousePosition { get; }
        
        private WorldManager _worldManager;

        public CrosshairModel(WorldManager worldManager, ICrosshairModule crosshairModule)
        {
            _worldManager = worldManager;
            MousePosition = crosshairModule.PositionMouse;
        }
    }
}