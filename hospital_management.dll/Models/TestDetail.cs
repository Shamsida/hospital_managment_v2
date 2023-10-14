using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_management.DAL.Models
{
    public class TestDetail
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserTestId { get; set; }
        public string? TestName { get; set; }
        public string? TestResult { get; set;}
    }
}
