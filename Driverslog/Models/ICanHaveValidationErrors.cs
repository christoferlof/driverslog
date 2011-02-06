using System.Collections.Generic;

namespace Driverslog.Models {
    public interface ICanHaveValidationErrors {

        bool IsValid();
        Dictionary<string, string> ValidationMessages { get;}

    }
}