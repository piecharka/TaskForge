using Domain;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class NotficationStatusRepository : GenericRepository<NotficationStatus>, INotficationStatusRepository
    {
        public NotficationStatusRepository(TaskForgeDbContext context) : base(context) { }
    }
}
