using System;

namespace AzurGames.Main
{
    public class GameManager
    {
        public static event Action OnRestartLevel;
        public static event Action<State> OnStateChange;
        public static event Action OnTakeCrystal;
        public static event Action OnTakeCollectable;

        private static State CurrentState;

        public static void SetState(State state)
        {
            CurrentState = state;
            OnStateChange?.Invoke(state);
        }

        public static void OnTakeCrystalInvoke() => OnTakeCrystal?.Invoke();

        public static void OnTakeCollectableInvoke() => OnTakeCollectable?.Invoke();

        public static void OnRestartLevelInvoke() => OnRestartLevel?.Invoke();

        public static State GetCurrentState => CurrentState;
    }

    public enum State
    {
        Start,
        Play,
        Stop,
        Complete
    }
}
