namespace Driverslog.Services {
    public interface ITrialService {
        int Limit{get;}
        bool LimitReached();
        void Buy();
        bool IsTrial{get;}
    }
}