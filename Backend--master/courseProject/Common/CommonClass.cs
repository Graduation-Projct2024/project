using courseProject.Core.Models;
using courseProject.Core.Models.DTO;
using System.Text.RegularExpressions;

namespace courseProject.Common
{
    public class CommonClass
    {


        public void ImageTOHttp(UserInfoDTO userInfoDTO)
        {
            string url = "http://localhost:5134/";
            userInfoDTO.ImageUrl = url + userInfoDTO.ImageUrl;
        }
        public void AddHttpToImage(User GetUser, string role)
        {
            string url = "http://localhost:5134/";
            switch (role.ToLower())
            {
                case "admin" when GetUser.admin?.ImageUrl != null:
                    GetUser.admin.ImageUrl = url + GetUser.admin.ImageUrl;
                    break;
                case "subadmin" when GetUser.subadmin?.ImageUrl != null:
                    GetUser.subadmin.ImageUrl = url + GetUser.subadmin.ImageUrl;
                    break;
                case "instructor" when GetUser.instructor?.ImageUrl != null:
                    GetUser.instructor.ImageUrl = url + GetUser.instructor.ImageUrl;
                    break;
                case "student" when GetUser.student?.ImageUrl != null:
                    GetUser.student.ImageUrl = url + GetUser.student.ImageUrl;
                    break;
            }

        }


        public void EditImageInFor (IReadOnlyList<Course>? model , Course? modelcourse)
        {
            string url = "http://localhost:5134/";
            if(model != null)
            {
            foreach (var course in model)
            {
                if (course.ImageUrl != null)
                {
                    course.ImageUrl = url + course.ImageUrl;
                }
            }
            }
            if(modelcourse.ImageUrl != null)
            {
                modelcourse.ImageUrl = url + modelcourse.ImageUrl;
            }
        }

        public bool IsValidTimeFormat(string time)
        {
            return Regex.IsMatch(time, @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");
        }


        public bool CheckStartAndEndTime(TimeSpan startTime, TimeSpan endTime)
        {


            if (startTime == null || endTime == null)
            {
                return false; // Invalid format or input
            }

            return startTime < endTime; // Ensure start time is before end time
        }

        public TimeSpan ConvertToTimeSpan (string time)
        {
            
                return TimeSpan.Parse(time);
            
        }

    }
}
