using IBusiness;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class DownloadStrategyFactory : IDownloadStrategyFactory
    {
        private readonly List<IDownloadStrategy> downloadStrategies;


        public DownloadStrategyFactory(List<IDownloadStrategy> downloadStrategies)
        {
            this.downloadStrategies = downloadStrategies;
        }

        public IDownloadStrategy Build(string soursesUrl)
        {
            if (soursesUrl != null)
            {
                var protocol = soursesUrl.Substring(0, soursesUrl.IndexOf(@"://")).ToUpper();


                return downloadStrategies.Where(O => O.IsMatch(protocol)).SingleOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
