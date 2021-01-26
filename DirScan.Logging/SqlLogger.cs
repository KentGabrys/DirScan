using AutoMapper;
using DirScan.Data;

namespace DirScan.Logging
{
    public class SqlLogger : ILogger
    {
        private readonly DirScanRepository _repository;
        private readonly IMapper _mapper;

        public SqlLogger(DirScanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        /// <summary>
        /// The data to log
        /// </summary>
        /// <typeparam name="T">a LogDatum, or a type that supports ToData()</typeparam>
        /// <param name="data">the data to log</param>
        public void Log<T>( T data )
        {
            var dirScanLogDto = ((IDirScanLogDto)data).ToData();
            var dirScanLog = _mapper.Map<DirScanLog>( dirScanLogDto );
            _repository.AddDirScanLog( dirScanLog );
            //_repository.SaveChanges();
        }

        public void SaveLogs()
        {
            _repository.SaveChanges();
        }
        
    }
}