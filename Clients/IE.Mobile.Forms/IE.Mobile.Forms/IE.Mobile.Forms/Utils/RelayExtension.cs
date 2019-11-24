using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace IE.Mobile.Forms.Utils
{
    public class RelayExtension : ICommand
    {
        protected Action Action { get; set; }
        protected Func<bool> CanActionExecute { get; set; }

        public RelayExtension(Action _Action) : this(_Action, null)
        {
            Action = _Action;
        }

        public RelayExtension(Action _Action, Func<bool> _CanActionExecute)
        {
            if (_Action == null)
                throw new ArgumentNullException("Action invoked cannot be null. Please review your parameters");
            if (_CanActionExecute == null)
                throw new ArgumentNullException("Can Action invoked cannot be null. Please review your parameters");

            Action = _Action;
            CanActionExecute = _CanActionExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (CanActionExecute != null)
                CanActionExecute.Invoke();

            return true;
        }

        public async void Execute(object parameter)
        {
            if (Action != null)
                Action.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
