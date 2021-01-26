using AutoMapper;
using DirScan.Data;

namespace DirScan.Logging
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DirScanLogDto, DirScanLog>();
            CreateMap<DirScanLog, DirScanLogDto>();
        }
    }
}
