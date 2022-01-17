using AzurGames.Main;
using UnityEngine;

namespace AzurGames.UI
{
    public class MainView : MonoBehaviour
    {
        [SerializeField] private CompletePanelView completePanelView;
        [SerializeField] private CrystalCounterView crystalCounterView;
        [SerializeField] private BonusCounterView bonusCounterView;
        [SerializeField] private HintView hintView;

        private void Awake()
        {
            OnStartState();

            GameManager.OnStateChange += SetState;
        }

        private void SetState(State state)
        {
            switch (state)
            {
                case State.Start:
                {
                    OnStartState();
                    break;
                }
                case State.Play:
                {
                    OnPlayState();
                    break;
                }
                case State.Stop:
                {
                    OnStopState();
                    break;
                }
                case State.Complete:
                {
                    OnCompleteState();
                    break;
                }
            }
        }

        private void OnStartState()
        {
            hintView.Show();
            
            bonusCounterView.Hide();
            crystalCounterView.Hide();
            completePanelView.Hide();
        }

        private void OnPlayState()
        {
            hintView.Hide();
            bonusCounterView.Show();
            crystalCounterView.Show();
        }

        private void OnStopState()
        {
            bonusCounterView.Hide();
            crystalCounterView.Hide();
        }

        private void OnCompleteState()
        {
            bonusCounterView.Hide();
            crystalCounterView.Hide();
            completePanelView.Show();
        }
    }
}
