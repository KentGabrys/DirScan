namespace DirScan.Logging
{
    public class SqlLogger : ILogger
    {
        internal SqlLogger(string connectionString)
        {

        }


        /// <summary>
        /// The data to log
        /// </summary>
        /// <typeparam name="T">a LogDatum, or a type that supports ToData()</typeparam>
        /// <param name="data">the data to log</param>
        public void Log<T>( T data )
        {
            var datum = ((ILogDatum)data).ToData();


        }

        
    }
}