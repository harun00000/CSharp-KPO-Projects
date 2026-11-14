using System.Collections.Generic;

// repository port for students
public interface IStudentRepository
{
    void Add(Student student);
    Student? GetById(int id);
    List<Student> GetAll();
}
