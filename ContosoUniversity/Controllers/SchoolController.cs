namespace ContosoUniversity.Controllers
{
    using ContosoUniversity.Data;
    using ContosoUniversity.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SchoolController : Controller
    {
        private SchoolDbContext schoolDbContext;

        public SchoolController(SchoolDbContext dbContext)
        {
            this.schoolDbContext = dbContext;
        }

        public async Task<List<Student>> GetStudents()
        {
            return await this.schoolDbContext.Students.ToListAsync<Student>();
        }

        public async Task<Student> GetStudentByID(int id)
        {
            return await this.schoolDbContext.Students.FirstOrDefaultAsync(student => student.ID == id);
        }

        [HttpPost]
        public async Task<bool> CreateStudent([FromBody] Student student)
        {
            try
            {
                this.schoolDbContext.Students.Add(student);
                await this.schoolDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        [HttpPost]
        public async Task<string> DeleteStudent(int studentId)
        {
            string result = "Student ID doesnot exist";
            if(studentId > 0)
            {
                Student student = this.schoolDbContext.Students.FirstOrDefault(item => item.ID == studentId);
                if (student?.ID != 0)
                {
                    this.schoolDbContext.Students.Remove(student);
                    await this.schoolDbContext.SaveChangesAsync();
                    result = "Deleted Successfully";
                }
            }
            return result;
        }
    }
}
