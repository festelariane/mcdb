using Mercedes.Data.Repositories.Contract;
using Mercedes.Plugins.TrinhMinh.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Data
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        IEnumerable<Project> GetPublishedProjects();
    }
}
