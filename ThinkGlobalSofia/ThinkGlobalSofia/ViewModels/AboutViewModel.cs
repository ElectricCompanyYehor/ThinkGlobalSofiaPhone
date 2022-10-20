using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ThinkGlobalSofia.ViewModels
{
    public class AboutViewModel : BaseViewModel, IDisposable
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
        public NfCManagerViewModel NFC { get; private set; } = new NfCManagerViewModel();

        public void Dispose()
        {
           NFC.Dispose();
        }
    }
}