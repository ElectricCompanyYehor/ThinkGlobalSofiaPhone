﻿using Plugin.NFC;
using System;
using System.Collections.Generic;
using System.Text;
using ThinkGlobalSofia.Views;
using Xamarin.Forms;

namespace ThinkGlobalSofia.ViewModels
{
    public class NfCManagerViewModel : BaseViewModel, IDisposable
    {
        private string _stateMessage = "Default";

        public string StateMessage { get => _stateMessage; set => SetProperty(ref _stateMessage, value); }

        public Command StartLinteningCommand { get; }

        public NfCManagerViewModel()
        {
            StartLinteningCommand = new Command(()=> {
                if (CrossNFC.Current.IsAvailable && CrossNFC.Current.IsEnabled)
                {
                    Unsubscribe();

                    _stateMessage = "IsAvailable";
                    // Event raised when a ndef message is received.
                    CrossNFC.Current.OnMessageReceived += Current_OnMessageReceived;
                    // Event raised when a ndef message has been published.
                    CrossNFC.Current.OnMessagePublished += Current_OnMessagePublished;
                    // Event raised when a tag is discovered. Used for publishing.
                    CrossNFC.Current.OnTagDiscovered += Current_OnTagDiscovered;
                    // Event raised when NFC listener status changed
                    CrossNFC.Current.OnTagListeningStatusChanged += Current_OnTagListeningStatusChanged;

                    // Android Only:
                    // Event raised when NFC state has changed.
                    CrossNFC.Current.OnNfcStatusChanged += Current_OnNfcStatusChanged;

                    // iOS Only: 
                    // Event raised when a user cancelled NFC session.
                    CrossNFC.Current.OniOSReadingSessionCancelled += Current_OniOSReadingSessionCancelled;
                    CrossNFC.Current.StartListening();
                }
                else
                {
                    StateMessage = $"IsAvailable {CrossNFC.Current.IsAvailable} and IsEnabled {CrossNFC.Current.IsEnabled}";
                }

            });

        }

        private void Current_OniOSReadingSessionCancelled(object sender, EventArgs e)
        {
            Unsubscribe();
            StateMessage = "iOSReadingSessionCancelled";
        }

        private void Current_OnNfcStatusChanged(bool isEnabled)
        {
            StateMessage = "StatusChanged";
        }

        private void Current_OnTagListeningStatusChanged(bool isListening)
        {
            StateMessage = "agListeningStatusChanged";
        }

        private void Current_OnTagDiscovered(ITagInfo tagInfo, bool format)
        {
            StateMessage = "OnTagDiscovered";
        }

        private void Current_OnMessagePublished(ITagInfo tagInfo)
        {
            StateMessage = "OnMessagePublishe";
        }

        private async void Current_OnMessageReceived(ITagInfo tagInfo)
        {
            switch (tagInfo.SerialNumber)
            {
                case "04226AEA836B80":
                        Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new FourGradePage());
                    break;
                case "04A038EA836B80":
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new CupboardPage());
                    break;
                case "049547EA836B80":
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new MagnitsPage());
                    break;
                case "040142EA836B81":
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new FireplacePage());
                    break;
                case "045E4EEA836B80":
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SpiderWebPage());
                    break;
                case "04874BEA836B80":
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new WhiteBoardPage());
                    break;
                default:
                    break;
            }
            
            StateMessage = "OnMessageReceived";
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            CrossNFC.Current.OnMessageReceived -= Current_OnMessageReceived;
            CrossNFC.Current.OnMessagePublished -= Current_OnMessagePublished;
            CrossNFC.Current.OnTagDiscovered -= Current_OnTagDiscovered;
            CrossNFC.Current.OnTagListeningStatusChanged -= Current_OnTagListeningStatusChanged;
            CrossNFC.Current.OnNfcStatusChanged -= Current_OnNfcStatusChanged;
            CrossNFC.Current.OniOSReadingSessionCancelled -= Current_OniOSReadingSessionCancelled;
        }
    }
}
