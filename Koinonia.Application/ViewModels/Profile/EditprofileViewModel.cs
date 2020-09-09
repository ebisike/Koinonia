using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.ViewModels.Profile
{
    public class EditprofileViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public StateOfOrigin StateOfOrigin { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
    }
}
