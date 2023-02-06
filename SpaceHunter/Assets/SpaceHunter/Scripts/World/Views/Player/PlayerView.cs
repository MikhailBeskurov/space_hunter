
using World.Core.View;
using SpaceHunter.Scripts.World.Models.Player;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace SpaceHunter.Scripts.World.Views.Player
{
    public class PlayerView : SimpleView<PlayerModel>
    {
        [Range(0,50f)]
        [SerializeField] private float _speedMovement = 20f;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _modelPlayer;
        
        private Vector2 _velocityMovement;
        private PlayerModel _model;
       
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void Bind(PlayerModel model)
        {
            _model = model;
            model.VelocityMovement.Subscribe(v =>
            {
                _velocityMovement = v;
            });
            
            _model.MousePosition.Subscribe(v =>
            {
                Vector2 Difference = new Vector2(v.x - _modelPlayer.position.x, v.z - _modelPlayer.position.z);
                Difference.Normalize();
                float RotationZ = Mathf.Atan2(Difference.x,Difference.y) * Mathf.Rad2Deg;
                _modelPlayer.rotation =  Quaternion.Euler(new Vector3(0f,RotationZ,0f));
            });
        }
        
        private void FixedUpdate()
        {
            //_rigidbody.velocity = _modelPlayer.rotation * new Vector3(_velocityMovement.x* _speedMovement,_rigidbody.velocity.y,_velocityMovement.y* _speedMovement) ;
            _rigidbody.velocity = new Vector3(_velocityMovement.x* _speedMovement,_rigidbody.velocity.y,_velocityMovement.y* _speedMovement);
        }
    }
}