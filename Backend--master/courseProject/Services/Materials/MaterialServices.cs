using AutoMapper;
using courseProject.Common;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.MaterialsDTO;
using courseProject.ServiceErrors;
using ErrorOr;
using System.Collections;
using System.Net;

namespace courseProject.Services.Materials
{
    public class MaterialServices : IMaterialServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MaterialServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

    

        public async Task<ErrorOr<Created>> AddTask(TaskDTO taskDTO)
        {
            await unitOfWork.FileRepository.UploadFile1(taskDTO.pdf);
            var taskMapped = mapper.Map<TaskDTO, CourseMaterial>(taskDTO);
            taskMapped.type = "Task";            
            var getcourses = await unitOfWork.instructorRepositpry.GetAllCoursesGivenByInstructorIdAsync(taskDTO.InstructorId);
            var getConsultations = await unitOfWork.instructorRepositpry.GetAllConsultationRequestByInstructorIdAsync(taskDTO.InstructorId);
            if (!getcourses.Any(x => x.Id == taskDTO.courseId) && !getConsultations.Any(x => x.Id == taskDTO.consultationId))
            {
                return ErrorUser.Unauthorized;
            }
            taskMapped.pdfUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(taskDTO.pdf);
            await unitOfWork.instructorRepositpry.AddMaterial(taskMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Created;
        }


        public async Task<ErrorOr<Created>> AddFile(FileDTO fileDTO)
        {
            await unitOfWork.FileRepository.UploadFile1(fileDTO.pdf);
            var fileMapped = mapper.Map<FileDTO, CourseMaterial>(fileDTO);
            fileMapped.type = "File";
            var getcourses = await unitOfWork.instructorRepositpry.GetAllCoursesGivenByInstructorIdAsync(fileDTO.InstructorId);
            var getConsultations = await unitOfWork.instructorRepositpry.GetAllConsultationRequestByInstructorIdAsync(fileDTO.InstructorId);
            if (!getcourses.Any(x => x.Id == fileDTO.courseId) && !getConsultations.Any(x => x.Id == fileDTO.consultationId))
                return ErrorUser.Unauthorized;
            fileMapped.pdfUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(fileDTO.pdf);
            await unitOfWork.instructorRepositpry.AddMaterial(fileMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Created>> AddAnnouncement(AnnouncementDTO AnnouncementDTO)
        {
            var AnnouncementMapped = mapper.Map<AnnouncementDTO, CourseMaterial>(AnnouncementDTO);
            AnnouncementMapped.type = "Announcement";
            var getcourses = await unitOfWork.instructorRepositpry.GetAllCoursesGivenByInstructorIdAsync(AnnouncementDTO.InstructorId);
            var getConsultations = await unitOfWork.instructorRepositpry.GetAllConsultationRequestByInstructorIdAsync(AnnouncementDTO.InstructorId);
            if (!getcourses.Any(x => x.Id == AnnouncementDTO.courseId) && !getConsultations.Any(x => x.Id == AnnouncementDTO.consultationId))
                return ErrorUser.Unauthorized;
            await unitOfWork.instructorRepositpry.AddMaterial(AnnouncementMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Created>> AddLink(LinkDTO linkDTO)
        {
            var linkMapped = mapper.Map<LinkDTO, CourseMaterial>(linkDTO);
            linkMapped.type = "Link";
            var getcourses = await unitOfWork.instructorRepositpry.GetAllCoursesGivenByInstructorIdAsync(linkDTO.InstructorId);
            var getConsultations = await unitOfWork.instructorRepositpry.GetAllConsultationRequestByInstructorIdAsync(linkDTO.InstructorId);
            if (!getcourses.Any(x => x.Id == linkDTO.courseId) && !getConsultations.Any(x => x.Id == linkDTO.consultationId))
                return ErrorUser.Unauthorized;
            await unitOfWork.instructorRepositpry.AddMaterial(linkMapped);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Updated>> EditTask(Guid id, TaskDTO taskDTO)
        {
            
            var TaskToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (TaskToUpdate == null) return ErrorMaterial.NotFound;
            

            var Taskmapper = mapper.Map(taskDTO, TaskToUpdate);
            Taskmapper.Id = id;
            Taskmapper.type = "Task";
            Taskmapper.pdfUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(taskDTO.pdf);
            await unitOfWork.instructorRepositpry.EditMaterial(Taskmapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Updated>> EditFile(Guid id, FileDTO fileDTO)
        {
           
            var FileToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (FileToUpdate == null) return ErrorMaterial.NotFound;
           

            var filemapper = mapper.Map(fileDTO, FileToUpdate);
            filemapper.Id = id;
            filemapper.type = "File";
            filemapper.pdfUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(fileDTO.pdf);
            await unitOfWork.instructorRepositpry.EditMaterial(filemapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Updated>> EditAnnouncement(Guid id, AnnouncementDTO AnnouncementDTO)
        {
           
            var AnnouncementToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (AnnouncementToUpdate == null) return ErrorMaterial.NotFound;
           

            var Announcementmapper = mapper.Map(AnnouncementDTO, AnnouncementToUpdate);
            Announcementmapper.Id = id;
            Announcementmapper.type = "Announcement";
            await unitOfWork.instructorRepositpry.EditMaterial(Announcementmapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Updated>> EDitLink(Guid id, LinkDTO linkDTO)
        {
           
            var LinkToUpdate = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (LinkToUpdate == null) return ErrorMaterial.NotFound;
            

            var Linkmapper = mapper.Map(linkDTO, LinkToUpdate);
            Linkmapper.Id = id;
            Linkmapper.type = "Link";
            await unitOfWork.instructorRepositpry.EditMaterial(Linkmapper);

            var success1 = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Deleted>> DeleteMaterial(Guid id)
        {
            var materail = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (materail == null) return ErrorMaterial.NotFound;
            
            await unitOfWork.instructorRepositpry.DeleteMaterial(id);
            var success = await unitOfWork.instructorRepositpry.saveAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<CourseMaterial>> GetMaterialById(Guid id)
        {
            var material = await unitOfWork.materialRepository.GetMaterialByIdAsync(id);
            if (material == null) return ErrorMaterial.NotFound;
            CommonClass.EditFileInMaterial(material);
            return material;           
        }

        public async Task<ErrorOr<ArrayList>> GetAllMaterialInTheCourse(Guid? courseId , Guid? consultationId)
        {
            var getCourse = await unitOfWork.CourseRepository.GetCourseByIdAsync(courseId);
            if (getCourse == null && courseId!=null) return ErrorCourse.NotFound;
            var getConsultation = await unitOfWork.StudentRepository.GetConsultationById(consultationId);
            if (getConsultation == null && consultationId!=null) return ErrorLectures.NotFound;
            var AlMaterials = await unitOfWork.materialRepository.GetAllMaterial(courseId , consultationId);

            ArrayList arrayList = new ArrayList();

            foreach (var material in AlMaterials)
            {
                CommonClass.EditFileInMaterial(material);
                arrayList.Add(material);
                
            }
            return arrayList;
        }
    }
}
