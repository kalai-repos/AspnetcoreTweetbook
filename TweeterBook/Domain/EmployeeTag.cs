using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TweeterBook.Domain
{
    public class EmployeeTag
    {
        [ForeignKey(nameof(TagName))]
        public virtual Tag Tag { get; set; }

        public string TagName { get; set; }

        public virtual Employee Employee { get; set; }

        public Guid  EmpId{ get; set; }

    }
}
