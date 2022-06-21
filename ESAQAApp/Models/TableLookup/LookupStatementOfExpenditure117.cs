using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupStatementOfExpenditure117
    {
        [Key]
        public int StatementOfExpenditure117Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}