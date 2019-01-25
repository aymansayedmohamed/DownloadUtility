﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;
namespace DomainModels
{
    public partial class DownloadedFile
    {
        public int Id { get; set; }
        public string FileRemotePath { get; set; }
        public string LocalPath { get; set; }
        public Nullable<int> ProcessingStatus { get; set; }

        public virtual ProcessingStatu ProcessingStatu { get; set; }
    }
}
