using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FiveMinuteMeeting.Shared.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

      public event PropertyChangedEventHandler PropertyChanged;
      public void OnPropertyChanged([CallerMemberName]string name = "")
      {
        if (PropertyChanged == null)
          return;

        PropertyChanged(this, new PropertyChangedEventArgs(name));
      }
    }
}
