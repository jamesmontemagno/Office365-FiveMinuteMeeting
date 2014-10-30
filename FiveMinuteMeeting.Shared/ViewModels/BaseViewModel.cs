using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FiveMinuteMeeting.Shared.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

      public event PropertyChangedEventHandler PropertyChanged;
      public void OnPropertyChanged(string name)
      {
        if (PropertyChanged == null)
          return;

        PropertyChanged(this, new PropertyChangedEventArgs(name));
      }
    }
}
