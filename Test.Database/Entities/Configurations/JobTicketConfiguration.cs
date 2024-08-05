using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Test.Database.Entities.Configurations
{
    public class JobTicketConfiguration : IEntityTypeConfiguration<JobTicket>
    {
        public void Configure(EntityTypeBuilder<JobTicket> builder)
        {
            var jobTicketId = 1;
            var jobTicketStepId = 1;

            builder.HasData([
                new JobTicket { Id = jobTicketId++, },
                new JobTicket { Id = jobTicketId++, },
                new JobTicket { Id = jobTicketId++, },
                new JobTicket { Id = jobTicketId++, },
                new JobTicket { Id = jobTicketId++, },
            ]);
        }
    }
}
