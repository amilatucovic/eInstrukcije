using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS1_2024_2025.Domain.SearchObjects
{
    public class AdminTutorSearchObject
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? CityId { get; set; }
        public string? MaxHourlyRate { get; set; }
        public bool? IsLiveAvailable { get; set; }
    }
}
