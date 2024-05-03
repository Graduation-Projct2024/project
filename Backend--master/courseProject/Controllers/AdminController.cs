using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using courseProject.Core.Models;


using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using courseProject.Repository.GenericRepository;
using Microsoft.Data.SqlClient;


using System.Configuration;
using System.Data.SqlClient;


namespace courseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        
        private readonly projectDbContext dbContext;

        public AdminController(projectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        //SqlConnection conn = new SqlConnection(connStr);


    }
}
