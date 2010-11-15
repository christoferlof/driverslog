using System;
using System.Windows;

namespace Driverslog.Services {
    public class MessageBoxService : IMessageBoxService {
        public void ShowMessage(string message) {
            MessageBox.Show(message);
        }
    }
}