using System;
using Project.Scripts.CameraLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerInitialPoint = "PlayerInitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag(PlayerInitialPoint);

            var hero = Instantiate("hero", at: initialPoint.transform.position);
            Instantiate("HUD");

            CameraFollow(hero);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject hero)
        {
            Camera.main?.GetComponent<CameraFollower>().Follow(hero);
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(prefab);
        }

        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}