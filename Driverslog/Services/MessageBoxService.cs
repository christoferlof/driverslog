using System;
using System.Windows;

namespace Driverslog.Services {
    public class MessageBoxService : IMessageBoxService {
        public void ShowMessage(string message) {
            MessageBox.Show(message);
        }

        public bool Confirm(string message) {
            return MessageBox.Show(message,"Confirm",MessageBoxButton.OKCancel) == MessageBoxResult.OK; 
        }
    }
}