using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Domains.Shared
{
    public interface ITrackable
    {
        DateTime Created { get; set; }
        long CreatorId { get; set; }

        DateTime Updated { get; set; }
        long UpdatorId { get; set; }
    }
}
