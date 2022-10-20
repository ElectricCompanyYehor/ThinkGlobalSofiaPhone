using Plugin.NFC;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkGlobalSofia.ViewModels
{
    public class NfCManagerViewModel : BaseViewModel, IDisposable
    {
        private string _stateMessage = "Default";

        public string StateMessage { get => _stateMessage; set => SetProperty(ref _stateMessage, value); }

        public NfCManagerViewModel()
        {
            if (CrossNFC.Current.IsAvailable && CrossNFC.Current.IsEnabled)
            {

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
            }
            else
            {
                StateMessage = $"IsAvailable {CrossNFC.Current.IsAvailable} and IsEnabled { CrossNFC.Current.IsEnabled}";
            }
        }

        private void Current_OniOSReadingSessionCancelled(object sender, EventArgs e)
        {
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

        private void Current_OnMessageReceived(ITagInfo tagInfo)
        {
            StateMessage = "OnMessageReceived";
        }

        public void Dispose()
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
