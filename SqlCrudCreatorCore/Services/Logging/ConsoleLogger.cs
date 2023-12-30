using log4net.Core;
using log4net.Repository;


namespace SqlCrudCreatorCore.Services
{
    public class ConsoleLogger : ILogger
    {
        public string Name => throw new NotImplementedException();

        public ILoggerRepository Repository => throw new NotImplementedException();

        public bool IsEnabledFor(Level level)
        {
            throw new NotImplementedException();
        }

        public void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Log(LoggingEvent logEvent)
        {
            throw new NotImplementedException();
        }

        public void LogIt(string logText)
        {
           Console.WriteLine(logText);
        }
    }
}
