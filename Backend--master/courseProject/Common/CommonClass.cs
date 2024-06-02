using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.EmployeesDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace courseProject.Common
{
    public class CommonClass
    {
        
        public static string Url= "https://localhost:7116/";
        private readonly IUnitOfWork unitOfWork;

        public CommonClass(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //public static void ImageTOHttp(UserInfoDTO userInfoDTO)
        //{
           
        //    userInfoDTO.ImageUrl = Url + userInfoDTO.ImageUrl;
        //}
        //public static  async void AddHttpToImage(User GetUser, string role)
        //{
          
        //    switch (role.ToLower())
        //    {
        //        case "admin" when GetUser.admin?.ImageUrl != null:
                   
        //            GetUser.admin.ImageUrl = Url + GetUser.admin.ImageUrl;
        //            break;
        //        case "subadmin" when GetUser.subadmin?.ImageUrl != null:
        //            GetUser.subadmin.ImageUrl = Url + GetUser.subadmin.ImageUrl;
        //            break;
        //        case "instructor" when GetUser.instructor?.ImageUrl != null:
        //            GetUser.instructor.ImageUrl = Url + GetUser.instructor.ImageUrl;
        //            break;
        //        case "student" when GetUser.student?.ImageUrl != null:
        //            GetUser.student.ImageUrl = Url + GetUser.student.ImageUrl;
        //            break;
        //    }

        //}


        //public static async void EditImageInFor (IReadOnlyList<Course>? model , Course? modelcourse)
        //{

           
        //    if (model != null)
        //    {
        //    foreach (var course in model)
        //    {
        //        if (course.ImageUrl != null)
        //        {
        //            course.ImageUrl = Url + course.ImageUrl;
        //        }
        //    }
        //    }
        //    if(modelcourse?.ImageUrl != null)
        //    {
        //        modelcourse.ImageUrl = Url + modelcourse.ImageUrl;
        //    }
        //}

        //public static async void EditFileInMaterial(CourseMaterial? material)
        //{
        //    material.pdfUrl = Url + material.pdfUrl;

        //}
        //public static async void EditImageInEmployeeDTO(IReadOnlyList<EmployeeDto>? employees)
        //{
        //    foreach (var employee in employees)
        //    {
        //        if (employee.ImageUrl != null)
        //        {
        //            employee.ImageUrl = Url + employee.ImageUrl;
        //        }
        //    }
           

        //}

        //public static async void EditImageInStudents(IReadOnlyList<Student>? students)
        //{
        //    foreach (var student in students)
        //    {
        //        if (student.ImageUrl != null)
        //        {
        //            student.ImageUrl = Url + student.ImageUrl;
        //        }
        //    }


        //}
        public static bool IsValidTimeFormat(string time)
        {
            return Regex.IsMatch(time, @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");
        }


        public static bool CheckStartAndEndTime(TimeSpan startTime, TimeSpan endTime)
        {


            if (startTime == null || endTime == null)
            {
                return false; // Invalid format or input
            }

            return startTime < endTime; // Ensure start time is before end time
        }

        public static TimeSpan ConvertToTimeSpan (string time)
        {
            
                return TimeSpan.Parse(time);
            
        }

    }
}
