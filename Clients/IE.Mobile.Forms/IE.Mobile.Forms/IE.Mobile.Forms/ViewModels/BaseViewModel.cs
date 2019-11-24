using Caliburn.Micro;

namespace IE.Mobile.Forms.ViewModels
{
    internal class BaseViewModel : Screen
    {
        protected IEventAggregator _aggregator => IoC.Get<IEventAggregator>();

        private string _ApplicationIcon;
        public string ApplicationIcon
        {
            get => _ApplicationIcon;
            set => this.Set(ref _ApplicationIcon, value);
        }

        private void InitializeBaseViewModel()
        {
            _aggregator.Subscribe(this); //Subscribe to any Event Aggregator messages -- This will act as the message broker
        }
        public BaseViewModel() { InitializeBaseViewModel(); }
    }
}
