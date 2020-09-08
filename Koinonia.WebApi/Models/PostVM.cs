using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Koinonia.WebApi.Models
{
    public class PostVM
    {
        public IFormFile MediaFile { get; set; }
    }
}
