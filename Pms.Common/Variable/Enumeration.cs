using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Common.Variable.Enumerations
{
    public enum ServiceResponse
    {
        Success
    }

    public enum AccountState
    {
        Unverified = 1,
        Active = 2,
        Billable = 3,
        Probationary = 4,
        Resigned = 5,
        Terminated = 6
    }
}
