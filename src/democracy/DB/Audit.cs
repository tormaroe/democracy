using System;
using System.Collections.Generic;
using System.Linq;
using democracy.Models;

namespace democracy.DB
{
    public class Audit : Repository<AuditItem>
    {
        public Audit() : base("audittrail") { }
    }
}