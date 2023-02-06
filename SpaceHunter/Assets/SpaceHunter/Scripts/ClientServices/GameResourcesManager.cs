using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

namespace ClientServices
{
    public interface IGameResourcesManager
    {
        Sprite GetSprite(string name);
        T GetPrefab<T>(string name) where T : Component;
    }

    public class GameResourcesManager : IGameResourcesManager
    {
        public IReadOnlyReactiveProperty<float> Progress => _progress;

        private ReactiveProperty<float> _progress = new ReactiveProperty<float>();

        private Dictionary<string, IList<Object>> _groups = new Dictionary<string, IList<Object>>();
        private Dictionary<string, List<SpriteAtlas>> _spriteGroups = new Dictionary<string, List<SpriteAtlas>>();
        private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
        private Dictionary<string, Component> _prefabs = new Dictionary<string, Component>();

        private List<SpriteAtlas> _atlases = new List<SpriteAtlas>();

        public async void LoadPrefabsGroups(string[] groupsName, Action afterLoading)
        {
            foreach (var groupName in groupsName)
            {
                var loadPrefabs = Addressables.LoadAssetsAsync<GameObject>(groupName.ToLower(), null);
                await loadPrefabs.Task;

                foreach (var result in loadPrefabs.Result)
                {
                    if (_groups.ContainsKey(groupName))
                    {
                        _groups[groupName].Add(result);
                    }
                    else
                    {
                        var objects = new List<Object>();
                        objects.Add(result);
                        _groups.Add(groupName, objects);
                    }
                }
            }
            afterLoading?.Invoke();
        }
        
        public async void LoadSpriteAtlasGroups(string[] groupsName, Action afterLoading)
        {
            foreach (var groupName in groupsName)
            {
                var loadAtlases = Addressables.LoadAssetsAsync<SpriteAtlas>(groupName.ToLower(), null);
                await loadAtlases.Task;

                foreach (var loadAtlas in loadAtlases.Result)
                {
                    if (_spriteGroups.ContainsKey(groupName))
                    {
                        _spriteGroups[groupName].Add(loadAtlas);
                    }
                    else
                    {
                        var atlases = new List<SpriteAtlas>();
                        atlases.Add(loadAtlas);
                        _spriteGroups.Add(groupName, atlases);
                    }
                }
            }

            afterLoading?.Invoke();
        }
        
        public T GetPrefab<T>(string name) where T : Component
        {
            if (_prefabs.TryGetValue(name, out var value))
            {
                return (T)value;
            }
            
            foreach (var gGroup in _groups)
            {
                var gameObject =
                    gGroup.Value.FirstOrDefault(v =>
                        String.Equals(v.name, name, StringComparison.CurrentCultureIgnoreCase)) as GameObject;
                if (gameObject == null)
                {
                    continue;
                }
                var neededComponent = gameObject.GetComponent<T>();
                if (neededComponent == null)
                {
                    string txt = $"нет такого компонента/объекта  {name} {typeof(T)}";
                    Debug.LogWarning(txt);
                    throw new NullReferenceException(txt);
                }

                return neededComponent;
            }

            return null;
        }

        public Sprite GetSprite(string name)
        {
            if (_sprites.TryGetValue(name, out var sprite))
            {
                return sprite;
            }
            foreach (var spriteAtlas in _spriteGroups)
            {
                foreach (var atlases in spriteAtlas.Value)
                {
                    var correctSprite = atlases.GetSprite(name);
                    if (correctSprite != null)
                    {
                        return correctSprite;
                    }
                }
            }

            return null;
        }

        private void SetProgress(float currentProgress, float maxProgress)
        {
            _progress.Value = currentProgress < maxProgress ? currentProgress : maxProgress;
        }
    }
}