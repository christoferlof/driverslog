namespace Driverslog.Services {
    public interface IMessageBoxService {
        void ShowMessage(string message);

        bool Confirm(string message);
    }
}