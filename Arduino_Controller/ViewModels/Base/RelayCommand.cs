using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Arduino_Controller
{

    /// <summary>
    /// Základní příkaz, který spustí akci
    /// </summary>
    public class RelayCommand : ICommand
    {

        #region Private Members

        /// <summary>
        /// Akce, která se má provést
        /// </summary>
        private Action mAction;

        #endregion

        #region Public Events

        /// <summary>
        /// Event, který se spustí, když se hodnota <see cref="CanExecute(object)"/> změní.
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Basic ctor
        /// </summary>
        public RelayCommand(Action action)
        {
            mAction = action;
        }
        #endregion

        #region Metody příkazu

        /// <summary>
        /// Relay příkaz lze vždy spustit
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Spustí Akci
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion
    }
}
