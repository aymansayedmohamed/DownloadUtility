using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBusiness;

namespace Business
{
    public class SourcesParser : IParser
    {
        public IQueryable<string> Parse(string sourses, string delimiter)
        {
            return sourses.Split(new[] { delimiter }, StringSplitOptions.None).AsQueryable();
        }
    }
}
