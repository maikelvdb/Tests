using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Database.Entities
{
    public class JobTicketStep
    {
        [Key,
            DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int JobTicketId { get; set; }
        public JobTicket? JobTicket { get; set; }

        public string State { get; set; }

        public int? CombinedJobTicketStepId { get; set; }
        public JobTicketStep? CombinedJobTicketStep { get; set; }
    }
}
