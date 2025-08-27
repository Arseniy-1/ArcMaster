using Project.Scripts.Infrastructure.AssetManagement;
using Project.Scripts.Infrastructure.Factory;
using Project.Scripts.Services;
using Project.Scripts.Services.Input;
using UnityEngine;

namespace Project.Scripts.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        private void RegisterServices()
        {
            _services.Register<IInputService>(GetInputService( ));
            _services.Register<IAssetProvider>(new AssetProvider());
            _services.Register<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>("Main");
        }

        public void Exit()
        {
            
        }

        private static IInputService GetInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}