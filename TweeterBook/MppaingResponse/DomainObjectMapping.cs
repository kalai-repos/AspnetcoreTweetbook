using AutoMapper;
using System.Linq;
using TweeterBook.Contract.Response;
using TweeterBook.Controllers;
using TweeterBook.Domain;

namespace TweeterBook.MppaingResponse
{
    public class DomainObjectMapping:Profile
    {
        public DomainObjectMapping()
        {

            CreateMap<Employee, PostResponse>()
               .ForMember(dest => dest.Tags, opt =>
                   opt.MapFrom(src => src.Tags.Select(x => new TagResponse { Name = x.TagName })));

            CreateMap<Tag, TagResponse>();
        }
    }
}
