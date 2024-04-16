using Domain;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class NotficationRepository : GenericRepository<Notfication>, INotficationRepository
    {
        public NotficationRepository(TaskForgeDbContext context) : base(context) { }
    }
}
