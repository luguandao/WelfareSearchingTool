using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckRanking
{
    public enum ResultCodeType
    {
        Success,
        UserNameOrPasswordFailed,
        PageLoadFailed,
        CannotCatchingMessage,
        ValidateCodeFailed
    }
}
