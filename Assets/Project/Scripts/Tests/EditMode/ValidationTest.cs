using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Tests.EditMode
{
    public class ValidationTest
    {
        [TestCaseSource(nameof(AllScenePaths))]
        public void AllGameObjectsShouldNotHaveMissingScripts(string scenePath)
        {
            var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

            var gameObjectsWithMissingScripts =
                AllGameObjects(scene)
                    .Where(HasMissingScript)
                    .Select(gameObject => gameObject.name)
                    .ToList();

            EditorSceneManager.CloseScene(scene, true);

            gameObjectsWithMissingScripts.Should().BeEmpty();
        }

        static bool HasMissingScript(GameObject gameObject) =>
            GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0;

        private static IEnumerable<string> AllScenePaths()
        {
            return AssetDatabase
                .FindAssets("t:Scene", new[] { "Assets" })
                .Select(AssetDatabase.GUIDToAssetPath);
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