using AutoMapper;

namespace Yarish.University.Filmark.Core.Interfaces
{
    public interface IMapperProvider
    {
        IMapper GetMapper();
    }
}