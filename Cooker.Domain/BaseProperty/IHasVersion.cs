using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooker.Domain.BaseProperty
{
    public interface IHasVersion
    {
        public long Version { get; set; }
    }
}