using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Domain.Interfaces.IServices
{
    public interface ILoggingService
    {
        /// <summary>
        /// Create generic method logger to integrate with AWS CloudWatch
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ILogger CreateLogger<T>();

        /// <summary>
        /// Create logger
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        ILogger CreateLogger(Type classType);
    }
}
