using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Domain.Models
{
    public class MediaFiles
    {
        public Guid Id { get; set; }
        public string fileName { get; set; }
        public Guid PostId { get; set; }
        public Posts Post { get; set; }
    }
}
