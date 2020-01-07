using RepoDb;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SchoolAPI.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher, SqlConnection>,
        ITeacherRepository
    {
        public TeacherRepository()
            : base(@"Server=.;Database=SchoolDB;Integrated Security=SSPI;")
        { }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return QueryAll();
        }

        public Teacher GetTeacher(int teacherId)
        {
            return Query(teacherId).FirstOrDefault();
        }

        public Teacher GetTeacherCache(int teacherId)
        {
            return Query(teacherId, cacheKey: $"Teacher{teacherId}").FirstOrDefault();
        }
    }
}
