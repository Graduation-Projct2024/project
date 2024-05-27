using courseProject.Core.Models;
using courseProject.Core.Models.DTO.MaterialsDTO;
using ErrorOr;
using System.Collections;
namespace courseProject.Services.Materials
{
    public interface IMaterialServices
    {

        public Task<ErrorOr<Created>> AddTask(TaskDTO taskDTO);
        public Task<ErrorOr<Created>> AddFile(FileDTO fileDTO);
        public Task<ErrorOr<Created>> AddAnnouncement(AnnouncementDTO AnnouncementDTO);
        public Task<ErrorOr<Created>> AddLink(LinkDTO linkDTO);
        public Task<ErrorOr<Updated>> EditTask(Guid id, TaskDTO taskDTO);
        public Task<ErrorOr<Updated>> EditFile(Guid id, FileDTO fileDTO);
        public Task<ErrorOr<Updated>> EditAnnouncement(Guid id, AnnouncementDTO AnnouncementDTO);
        public Task<ErrorOr<Updated>> EDitLink(Guid id, LinkDTO linkDTO);
        public Task<ErrorOr<Deleted>> DeleteMaterial(Guid id);
        public Task<ErrorOr<CourseMaterial>> GetMaterialById(Guid id);
        public Task<ErrorOr<ArrayList>> GetAllMaterialInTheCourse(Guid courseId);

    }
}
