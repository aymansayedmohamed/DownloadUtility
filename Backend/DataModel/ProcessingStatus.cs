using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    using IDomainModels;
    using System.Collections.Generic;

    public partial class ProcessingStatu: IProcessingStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProcessingStatu()
        {
            this.DownloadedFiles = new HashSet<DownloadedFile>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DownloadedFile> DownloadedFiles { get; set; }
    }
}
