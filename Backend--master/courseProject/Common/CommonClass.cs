using courseProject.Core.Models;

namespace courseProject.Common
{
    public class CommonClass
    {
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


        public void EditImageInFor (IReadOnlyList<Course> model)
        {
            string url = "http://localhost:5134/";
            foreach (var course in model)
            {
                if (course.ImageUrl != null)
                {
                    course.ImageUrl = url + course.ImageUrl;
                }
            }
        }

    }
}
