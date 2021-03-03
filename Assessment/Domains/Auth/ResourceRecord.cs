using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Domains.Auth
{
    public class Record
    {
        public string Domain { get; init; }
        public string Area { get; init; }
        public string Controller { get; init; }
        public string Action { get; init; }
        public string Id { get; init; }
    }
}
