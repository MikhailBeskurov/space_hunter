using System;
using System.Collections.Generic;
using HoneyWood.Scripts.ClientServices;
using HoneyWood.Scripts.UI.Core;
using HoneyWood.Scripts.Utils.RXExtension;
using HoneyWood.Scripts.Utils.Update;
using HoneyWood.Scripts.World.Core;
using SpaceHunter.Scripts.Models.Dialog;
using SpaceHunter.Scripts.Modules.Controls;
using SpaceHunter.Scripts.Modules.Crosshair;
using SpaceHunter.Scripts.Modules.Dialog;
using SpaceHunter.Scripts.Modules.Dialog.View;
using SpaceHunter.Scripts.Modules.Gameplay;
using SpaceHunter.Scripts.Modules.Player.Movement;
using SpaceHunter.Scripts.Modules.Weapon;
using SpaceHunter.Scripts.ScriptableObjects;
using SpaceHunter.Scripts.UI.Models.Dialog;
using SpaceHunter.Scripts.UI.Models.Weapon;
using SpaceHunter.Scripts.World.Models.Player;
using UnityEngine;
using Zenject;

namespace SpaceHunter.Scripts.Installer
{
    public class MenuInstaller : MonoInstaller
    {
        [Header("Scene")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TriggerDialogModule _triggerDialogModule;
        
        [Space][Header("ScriptableObjects")]
        [SerializeField] private Bullets _bullets;
        [SerializeField] private Characters _characters;
        [SerializeField] private DialogStories _dialogStories;
        
        private DiContainer _diContainer;
        private GameResourcesManager _gameResourcesManager;
        private AssetProvider _assetProvider;
      
        private UIManager _uiManager;
        private WorldManager _worldManager;

        private IGameplayModule _gameplayModule;
        
        private DisposableList _disposableList = new DisposableList();
        private List<IUpdatable> _updatablesList = new List<IUpdatable>();
        
        public override void InstallBindings()
        {
            _diContainer = new DiContainer();
            LoadResources();
        }
        
        private void LoadResources()
        {
            _gameResourcesManager = new GameResourcesManager();

            _assetProvider = new AssetProvider(_gameResourcesManager);
            SpriteProvider.Init(_gameResourcesManager);

            _gameResourcesManager.LoadGroups("", Init);
        }
        
        private void Update()
        {
            if (_updatablesList.Count <= 0)
            {
                return;
            }

            foreach (var updatable in _updatablesList)
            {
                updatable.Update(Time.deltaTime);
            }
        }
        
        private void Init()
        {
            InitScriptableObject();
            InitModules();
            InitUI();
            InitFabrics();
            InitWorld();

            _updatablesList.AddRange(_diContainer.ResolveAll<IUpdatable>());
            _disposableList.AddRange(_diContainer.ResolveAll<IDisposable>());
            
            _gameplayModule.StartGame();
        }

        private void InitScriptableObject()
        {
            _diContainer.BindInterfacesAndSelfTo<Bullets>().FromInstance(_bullets).AsSingle();
            _diContainer.BindInterfacesAndSelfTo<DialogStories>().FromInstance(_dialogStories).AsSingle();
            _diContainer.BindInterfacesAndSelfTo<Characters>().FromInstance(_characters).AsSingle();
        }
        
        private void InitModules()
        {
            _gameplayModule = new GameplayModule();
            _worldManager = new WorldManager(_assetProvider);
            
            _diContainer.BindInterfacesAndSelfTo<WorldManager>().FromInstance(_worldManager).AsSingle();
            _diContainer.BindInterfacesAndSelfTo<AssetProvider>().FromInstance(_assetProvider).AsSingle();
            
            _diContainer.BindInterfacesAndSelfTo<GameplayModule>().FromInstance(_gameplayModule).AsSingle();
            _diContainer.BindInterfacesAndSelfTo<WeaponModule>().FromNew().AsSingle();
            _diContainer.BindInterfacesAndSelfTo<PlayerControls>().FromNew().AsSingle();
            _diContainer.BindInterfacesAndSelfTo<PlayerMovementModule>().FromNew().AsSingle();
            _diContainer.BindInterfacesAndSelfTo<CrosshairModule>().FromNew().AsSingle();
            _diContainer.BindInterfacesAndSelfTo<ControlsModule>().FromNew().AsSingle();
            _diContainer.BindInterfacesAndSelfTo<DialogProvider>().FromNew().AsSingle();
            _diContainer.BindInterfacesAndSelfTo<DialogModule>().FromNew().AsSingle();

            _diContainer.Inject(_gameplayModule);
            _diContainer.Inject(_triggerDialogModule);
        }

        private void InitUI()
        {
            _uiManager = new UIManager(_assetProvider, _canvas);
            _diContainer.BindInterfacesAndSelfTo<UIManager>().FromInstance(_uiManager).AsSingle();
            
            _uiManager.Bind(_diContainer.Instantiate<DialogModel>());
            //_uiManager.Bind(_diContainer.Instantiate<WeaponModel>());
            _uiManager.DisableAllScreen();
        }

        private void InitWorld()
        {
            var playerModel = _diContainer.Instantiate<PlayerModel>();
             
            _diContainer.BindInterfacesAndSelfTo<PlayerModel>().FromInstance(playerModel).AsSingle();
            _worldManager.Bind(playerModel);
            _worldManager.Bind(_diContainer.Instantiate<CrosshairModel>());
        }

        private void InitFabrics()
        {
            
        }
    }
}