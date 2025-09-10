using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Tools
{
    public static class Validator
    {
        private static ILogger Logger => Debug.unityLogger;

        [MenuItem("Tools/👏 Missing Components")]
        public static void FindMissingComponents()
        {
            foreach (var scene in OpenAllProjectScenes())
            foreach (var gameObject in AllGameObjects(scene))
            {
                if (HasMissingScript(gameObject))
                    Logger.LogError("🤷‍♂️",
                        $"GameObject {gameObject.name} from scene {scene.name} has a missing components");
            }

            static bool HasMissingScript(GameObject gameObject) => 
                GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0;
        }

        private static IEnumerable<Scene> OpenAllProjectScenes()
        {
            var scenePaths = AssetDatabase
                .FindAssets("t:Scene", new[] { "Assets" })
                .Select(AssetDatabase.GUIDToAssetPath);

            foreach (var scenePath in scenePaths)
            {
                Scene scene = SceneManager.GetSceneByPath(scenePath);

                if (scene.isLoaded)
                {
                    yield return scene;
                }

                else
                {
                    var openedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                    yield return openedScene;
                    EditorSceneManager.CloseScene(openedScene, true);
                }
            }
        }

        private static IEnumerable<GameObject> AllGameObjects(Scene scene)
        {
            Queue<GameObject> gameObjects = new Queue<GameObject>(scene.GetRootGameObjects());

            while (gameObjects.Count > 0)
            {
                GameObject gameObject = gameObjects.Dequeue();

                yield return gameObject;

                foreach (Transform child in gameObject.transform)
                {
                    gameObjects.Enqueue(child.gameObject);
                }
            }
        }
    }
}