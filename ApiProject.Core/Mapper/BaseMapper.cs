using ApiProject.Core.Interfaces.IMapper;
using AutoMapper;

namespace ApiProject.Core.Mapper
{
    public class BaseMapper<TSource, TDestination>(IMapper mapper) : IBaseMapper<TSource, TDestination>
    {
        private readonly IMapper _mapper = mapper;

        public TDestination MapModel(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public IEnumerable<TDestination> MapList(IEnumerable<TSource> source)
        {
            return _mapper.Map<IEnumerable<TDestination>>(source);
        }
    }
}
