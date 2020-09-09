using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.ViewModels
{
    public class KoinoniaUserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public StateOfOrigin stateOfOrigin { get; set; }
        public Gender Gender { get; set; }
    }
}
