using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    [CreateAssetMenu]
    public class GameTileContentFactory : ScriptableObject
    {
        [SerializeField] private GameTileContentPrefabsData data;
        public void Reclaim(GameTileContent content)
        {
            Destroy(content.gameObject);
        }

        public GameTileContent Get(GameTileContentType type)
        {
            switch (type)
            {
                case GameTileContentType.Empty:
                    return Get(data.empty);
                case GameTileContentType.Destination:
                    return Get(data.destination);
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
    }
}