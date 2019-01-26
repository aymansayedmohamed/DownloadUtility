using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDomainModels
{
    public interface IProcessingStatus
    {
         int Id { get; set; }
         string Status { get; set; }


    }
}
