using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Infrastructure.ExtensionMethods
{
    /// <summary>
    /// Add logs extension method 
    /// </summary>
    public static class LoggerExtensions
    {
        public static void LogCritical(this ILogger logger, Exception exception)
        {
            logger.LogCritical(default(EventId), exception, exception.GetInnerException().GenerateLogContent());
        }

        public static void LogError(this ILogger logger, Exception exception)
        {
            logger.LogError(default(EventId), exception, exception.GetInnerException().GenerateLogContent());
        }
    }
}
