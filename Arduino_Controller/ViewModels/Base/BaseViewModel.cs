using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino_Controller
{
    /// <summary>
    /// Základní ViewModel který podle potřeby spouští Property Changed
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event který se spustí, když se property změní
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender,e) => { };

        /// <summary>
        /// Tato metoda zavolá <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name) => PropertyChanged(this, new PropertyChangedEventArgs(name));
    }
}
