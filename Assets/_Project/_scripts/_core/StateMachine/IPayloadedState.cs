namespace Assets._Project._scripts._core.StateMachine
{
    public interface IPayloadedState<TPayload>: IExitableState
    {
        void Enter(TPayload payload);
    }
}
