using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooker.Domain.BaseProperty
{
    public interface IHasCurrentTime
    {
        public DateTime CreateTime { get; set; }
    }
}