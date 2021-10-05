using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Infrastructure.ExtensionMethods
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Creates generate the content of log message
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GenerateLogContent(this Exception exception)
        {
            var content = exception.Message + Environment.NewLine + exception.StackTrace;
            return content;
        }

        /// <summary>
        /// Get inner exceptions
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Exception GetInnerException(this Exception ex)
        {
            var innerException = ex;
            
            while(innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
            }

            return innerException;
        }
    }
}
