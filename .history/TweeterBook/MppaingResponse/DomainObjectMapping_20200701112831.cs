using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
