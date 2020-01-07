using RepoDb;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolAPI.Repositories
{
    public class StudentRepository : BaseRepository<Student, SqlConnection>,
        IStudentRepository
    {
        public StudentRepository()
            : base(@"Server=.;Database=SchoolDB;Integrated Security=SSPI;")
        { }

        public IEnumerable<Student> GetAllStudents()
        {
            return QueryAll();
        }

        public int Save(Student student)
        {
            return Insert<int>(student);
        }

        public void SaveAll(IList<Student> students)
        {
            InsertAll(students);
        }
    }
}
