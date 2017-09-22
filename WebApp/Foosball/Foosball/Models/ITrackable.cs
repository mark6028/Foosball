using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public interface ITrackable
    {
        DateTime? CreatedAt { get; set; }
        DateTime? LastUpdatedAt { get; set; }
    }
}
