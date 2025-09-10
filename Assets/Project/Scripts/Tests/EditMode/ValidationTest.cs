using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Tests.EditMode
{
    public class ValidationTest
    {
        private static ILogger Logger => Debug.unityLogger;

        [Test]
        public void NewTestScriptSimplePasses()
        {
            bool missingComponentsDetected = false;

            foreach (var scene in OpenAllProjectScenes())
            foreach (var gameObject in AllGameObjects(scene))
            {
                if (HasMissingScript(gameObject))
                {
                    missingComponentsDetected = true;
                    Logger.LogError("🤷‍♂️",
                        $"GameObject {gameObject.name} from scene {scene.name} has a missing components");
                }
            }

            Assert.That(missingComponentsDetected, Is.True);
        }

        static bool HasMissingScript(GameObject gameObject) =>
            GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0;

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