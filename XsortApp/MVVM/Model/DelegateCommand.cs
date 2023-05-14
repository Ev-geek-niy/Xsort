using System;
using System.Windows.Input;

namespace XsortApp.MVVM.Model;

public class DelegateCommand : ICommand
{
    private Action<object> _execute;
    private Func<object, bool> _canExecute;

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return parameter == null || _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        this._execute(parameter);
    }
}