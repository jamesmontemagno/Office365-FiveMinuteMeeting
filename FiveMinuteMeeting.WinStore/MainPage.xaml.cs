using FiveMinuteMeeting.Shared;
using FiveMinuteMeeting.Shared.ViewModels;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FiveMinuteMeeting.WinStore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		public ContactsViewModel viewModel;
        public MainPage()
        {
            this.InitializeComponent();

			DataContext = viewModel = new ContactsViewModel();
			this.Loaded += MainPage_Loaded;
			AuthenticationHelper.PlatformParameters = new PlatformParameters(PromptBehavior.Auto, false);
        }

	
		void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			if (viewModel.Contacts.Count > 0)
				return;


			viewModel.GetContactsAsync();
		}
    }
}
