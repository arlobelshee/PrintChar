using System;
using System.Windows.Input;

namespace PrintChar
{
	public class SimpleCommand : ICommand
	{
		private readonly Func<bool> _canExecute;
		private readonly Action _execute;

		public SimpleCommand(Func<bool> canExecute, Action execute)
		{
			_canExecute = canExecute;
			_execute = execute;
		}

		public void Execute(object parameter)
		{
			_execute();
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute();
		}

		public event EventHandler CanExecuteChanged;

		public void NotifyThatCanExecuteChanged(EventArgs e)
		{
			var handler = CanExecuteChanged;
			if (handler != null)
				handler(this, e);
		}
	}
}