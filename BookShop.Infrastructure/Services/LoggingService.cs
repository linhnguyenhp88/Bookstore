using System;
using AWS.Logger;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BookShop.Domain.Interfaces.IServices;

namespace BookShop.Infrastructure.Services
{
    public class LoggingService : ILoggingService
    {
        private static ILoggerFactory _loggerFactory;
    
        public LoggingService(ILoggerFactory loggerFactory, IOptions<AWSLoggerConfig> awsLoggerOptions)
        {            
            if (_loggerFactory == null)
            {
                var awsLoggerConfig = awsLoggerOptions.Value;
                _loggerFactory = loggerFactory
                    .AddAWSProvider(awsLoggerConfig, LogLevel.Information); // add an AWS logger provider                  
            }           
        }

        /// <summary>
        /// Creates the logger for generic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ILogger CreateLogger<T>()
        {
            var logger = _loggerFactory.CreateLogger<T>();
            return logger;
        }

        /// <summary>
        /// Creates the logger for specific class
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        public ILogger CreateLogger(Type classType)
        {
            var logger = _loggerFactory.CreateLogger(classType);
            return logger;
        }
    }
}
