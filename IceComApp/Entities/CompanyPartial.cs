using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceComApp.Entities
{
    public partial class Company
    {
        public string FullName => $"{CompanyType.Name} \"{Name}\"";
    }
}
