using Microsoft.AspNetCore.Mvc;
using RepoDb.Extensions;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private IStudentRepository m_studentRepository;
        private ITeacherRepository m_teacherRepository;

        public StudentController(IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            m_studentRepository = studentRepository;
            m_teacherRepository = teacherRepository;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Student>> Get()
        {
            return m_studentRepository.GetAllStudents().AsList();
        }

        [HttpGet("querystat")]
        public ActionResult<dynamic> GetQueryStat()
        {
            var students = m_studentRepository.GetAllStudents();
            var iteration = 0;
            var dateTime = DateTime.UtcNow;
            foreach (var student in students)
            {
                var teacher = m_teacherRepository.GetTeacher(student.TeacherId);
                if (teacher != null)
                {
                    iteration++;
                }
            }
            var elapsed = (DateTime.UtcNow - dateTime).TotalMilliseconds;
            return new
            {
                Iteration = iteration,
                ElapsedInMilliseconds = elapsed
            };
        }

        [HttpGet("querystatcache")]
        public ActionResult<dynamic> GetQueryStatCache()
        {
            var students = m_studentRepository.GetAllStudents();
            var iteration = 0;
            var dateTime = DateTime.UtcNow;
            foreach (var student in students)
            {
                var teacher = m_teacherRepository.GetTeacherCache(student.TeacherId);
                if (teacher != null)
                {
                    iteration++;
                }
            }
            var elapsed = (DateTime.UtcNow - dateTime).TotalMilliseconds;
            return new
            {
                Iteration = iteration,
                ElapsedInMilliseconds = elapsed
            };
        }

        [HttpGet("generate/{count}")]
        public ActionResult<dynamic> Generate(int count = 10000)
        {
            var teachers = m_teacherRepository
                .GetAllTeachers()
                .AsList();
            var random = new Random();
            var students = new List<Student>();
            var dateTime = DateTime.UtcNow;

            for (var i = 0; i < count; i++)
            {
                var teacher = teachers.ElementAt(random.Next(0, teachers.Count - 1));
                var student = new Student
                {
                    Name = $"Student-{Guid.NewGuid().ToString()}",
                    TeacherId = teacher.Id
                };
                students.Add(student);
            }

            m_studentRepository.SaveAll(students);

            var elapsed = (DateTime.UtcNow - dateTime).TotalMilliseconds;
            return new
            {
                Inserted = students.Count,
                ElapsedInMilliseconds = elapsed
            };
        }
    }
}