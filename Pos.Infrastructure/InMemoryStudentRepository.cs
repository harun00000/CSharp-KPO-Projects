using System;
using System.Collections.Generic;

// Ports & Adapters: adapter for IStudentRepository
public class InMemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> students;

    public InMemoryStudentRepository()
    {
        students = new List<Student>();
    }

    public void Add(Student student)
    {
        foreach (Student item in students)
        {
            if (item.Id == student.Id)
            {
                throw new ArgumentException("Студент с таким Id уже есть:(");
            }
        }

        students.Add(student);
    }

    public Student? GetById(int id)
    {
        foreach (Student student in students)
        {
            if (student.Id == id)
            {
                return student;
            }
        }

        return null;
    }

    public List<Student> GetAll()
    {
        // return copy
        return new List<Student>(students);
    }
}
