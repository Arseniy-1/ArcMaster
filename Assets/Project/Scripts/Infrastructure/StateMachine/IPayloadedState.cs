namespace Project.Scripts.Infrastructure.StateMachine
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload sceneName);
    }
}