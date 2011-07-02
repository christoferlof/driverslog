namespace Driverslog.Services {
    public interface IMessageBoxService {
        void ShowMessage(string message);

        bool Confirm(string message);
        bool Confirm(string title, string message);
    }
}