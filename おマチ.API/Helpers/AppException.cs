using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Helpers
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(String message) : base(message) { }

        public AppException(String message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
