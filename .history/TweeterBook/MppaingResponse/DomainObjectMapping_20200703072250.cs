using AutoMapper;
using TweeterBook.Controllers;
using TweeterBook.Domain;

namespace TweeterBook.MppaingResponse
{
    public class DomainObjectMapping:Profile
    {
        public DomainObjectMapping()
        {
            CreateMap<Employee, PostResponse>();
        }
    }
}
