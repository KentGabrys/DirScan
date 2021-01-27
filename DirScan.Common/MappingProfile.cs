using System.IO;
using System.Security.Principal;
using AutoMapper;
using DirScan.Common.Models;
using DirScan.Data;

namespace DirScan.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DirScanLogDto, DirScanLog>();
            CreateMap<DirScanLog, DirScanLogDto>();
            CreateMap<DirectoryInfo, DirectoryFile>()
                .ForMember( dest => dest.Name, opt => opt.MapFrom( src => src.FullName ) )
                .ForMember( dest => dest.DateCreated, opt => opt.MapFrom( src => src.CreationTime ) )
                .ForMember( dest => dest.DateLastModified, opt => opt.MapFrom( src => src.LastWriteTime ) )
                .ForMember( dest => dest.Owner, opt => opt.MapFrom( src => GetFileOwner( src.FullName ) ) )
                .ForMember( dest => dest.FileAttributes, opt => opt.MapFrom( src => src.Attributes ) );
            CreateMap<FileInfo, DirectoryFile>()
                .ForMember( dest => dest.Name, opt => opt.MapFrom( src => src.FullName ) )
                .ForMember( dest => dest.Size, opt => opt.MapFrom( src => src.Length ) )
                .ForMember( dest => dest.DateCreated, opt => opt.MapFrom( src => src.CreationTime ) )
                .ForMember( dest => dest.DateLastModified, opt => opt.MapFrom( src => src.LastWriteTime ) )
                .ForMember( dest => dest.Owner, opt => opt.MapFrom( src => GetFileOwner( src.FullName ) ) )
                .ForMember( dest => dest.FileAttributes, opt => opt.MapFrom( src => src.Attributes ) );

        }

        private static string GetFileOwner( string fullPathFileName )
        {
            var fs = File.GetAccessControl( fullPathFileName );
            var sid = fs.GetOwner( typeof( SecurityIdentifier ) );
            return sid.Translate( typeof( NTAccount ) ).Value;
        }

    }
}
