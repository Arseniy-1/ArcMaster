using Project.Scripts.Infrastructure.StateMachine;
using UnityEngine;

namespace Project.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        
        public LoadingCurtain LoadingCurtain;

        private void Awake()
        {
            _game = new Game(this, LoadingCurtain);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}