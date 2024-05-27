using courseProject.Core.Models;
using ErrorOr;

namespace courseProject.Services.SubAdmins
{
    public interface ISubAdminServices
    {
        public Task<IReadOnlyList<SubAdmin>> GetAllSubAdmins();
        public Task<ErrorOr<SubAdmin>> getSubAdminById(Guid subAdminId);
    }
}
