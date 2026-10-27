using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooker.Domain.BaseProperty
{
    public interface IHasGuid
    {
        public string Guid { get; set; }
    }
}