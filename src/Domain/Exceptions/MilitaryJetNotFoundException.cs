using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class MilitaryJetNotFoundException : NotFoundException
    {
        public MilitaryJetNotFoundException(int jetId)
          : base($"No military jet found with ID {jetId}")
        {
        }
    }
}
