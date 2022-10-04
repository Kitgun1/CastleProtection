using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    [CreateAssetMenu]
    public class GameTileContentFactory : ScriptableObject
    {
        [SerializeField] private GameTileContentPrefabsData tileContentPrefabsData;
        
        public void Reclaim(GameTileContent content)
        {
            Destroy(content.gameObject);
        }

        public GameTileContent Get(GameTileContentType type)
        {
            switch (type)
            {
                case GameTileContentType.Empty:
                    return Get(tileContentPrefabsData.empty);
                case GameTileContentType.Destination:
                    return Get(tileContentPrefabsData.destination);
                case GameTileContentType.Wall:
                    return Get(tileContentPrefabsData.wall);
                case GameTileContentType.SpawnPoint:
                    return Get(tileContentPrefabsData.spawnPoint);
                default:
                    return null;
            }
        }

        private GameTileContent Get(GameTileContent prefab)
        {
            GameTileContent instance = Instantiate(prefab);
            instance.OriginFactory = this;
            MoveToFactoryScene(instance.gameObject);
            return instance;
        }

        private Scene _contentScene;

        private void MoveToFactoryScene(GameObject gameObject)
        {
            if (!_contentScene.isLoaded)
            {
                if (Application.isEditor)
                {
                    _contentScene = SceneManager.GetSceneByName(name);
                    if (!_contentScene.isLoaded)
                    {
                        _contentScene = SceneManager.CreateScene(name);
                    }
                }
                else
                {
                    _contentScene = SceneManager.CreateScene(name);
                }
            }
            SceneManager.MoveGameObjectToScene(gameObject, _contentScene);
        }
    }

    [Serializable]
    public struct GameTileContentPrefabsData
    {
        public GameTileContent empty;
        public GameTileContent destination;
        public GameTileContent wall;
        public GameTileContent spawnPoint;
    }
}