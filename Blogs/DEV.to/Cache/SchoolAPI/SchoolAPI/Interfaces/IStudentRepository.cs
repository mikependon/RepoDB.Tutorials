using SchoolAPI.Models;
using System.Collections.Generic;

namespace SchoolAPI.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();
        int Save(Student student);
        void SaveAll(IList<Student> students);
    }
}
