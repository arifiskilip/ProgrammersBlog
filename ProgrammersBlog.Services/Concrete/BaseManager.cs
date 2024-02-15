using AutoMapper;
using ProgrammersBlog.Data.UnitOfWork;

namespace ProgrammersBlog.Services.Concrete
{
    public class BaseManager
    {
        public BaseManager(IUow uow, IMapper mapper)
        {
            Uow = uow;
            Mapper = mapper;
        }

        protected IUow Uow { get; }
        protected IMapper Mapper { get; }

    }
}
