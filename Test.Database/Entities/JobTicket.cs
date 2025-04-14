using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Database.Entities
{
    public class JobTicket
    {
        [Key,
            DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public List<JobTicketStep> Steps { get; set; } = [];
    }
}
