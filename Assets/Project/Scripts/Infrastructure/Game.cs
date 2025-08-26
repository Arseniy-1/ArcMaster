using Project.Scripts.Infrastructure.StateMachine;
using Project.Scripts.Services.Input;
using Unity.VisualScripting;

namespace Project.Scripts.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        
        public static IInputService InputService;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain);
        }
    }
}