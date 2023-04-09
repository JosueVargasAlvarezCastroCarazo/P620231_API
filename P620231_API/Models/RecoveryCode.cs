using System;
using System.Collections.Generic;

namespace P620231_API.Models
{
    public partial class RecoveryCode
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime GenerateDate { get; set; }
        public bool Used { get; set; }
    }
}
