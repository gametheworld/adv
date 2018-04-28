using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.Util
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string message)
            : base(message)
        {

        }
        public UserFriendlyException(string errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public UserFriendlyException(string errorCode, string message, object data)
            : base(message)
        {
            this.ErrorCode = errorCode;
            ErrorBody = data;
        }

        public object ErrorBody { get; set; }


        public string ErrorCode { get; set; }
    }
}
