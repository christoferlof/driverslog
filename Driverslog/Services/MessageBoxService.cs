using System;
using System.Windows;

namespace Driverslog.Services {
    public class MessageBoxService : IMessageBoxService {
        public void ShowMessage(string message) {
            MessageBox.Show(message);
        }

        public bool Confirm(string message) {
            return Confirm(Strings.MessageBoxConfirm,message);
        }

        public bool Confirm(string title, string message) {
            return MessageBox.Show(message, title, MessageBoxButton.OKCancel) == MessageBoxResult.OK;
        }
    }
}