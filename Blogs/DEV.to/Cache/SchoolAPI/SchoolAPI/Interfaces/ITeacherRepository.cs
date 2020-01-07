using SchoolAPI.Models;
using System.Collections.Generic;

namespace SchoolAPI.Interfaces
{
    public interface ITeacherRepository
    {
        IEnumerable<Teacher> GetAllTeachers();
        Teacher GetTeacher(int teacherId);
        Teacher GetTeacherCache(int teacherId);
    }
}
