using System.Collections.Generic;

public class StudentService
{
    private readonly IStudentRepository studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }

    public void AddStudent(int id, string name)
    {
        Student student = new Student(id, name);
        studentRepository.Add(student);
    }

    public Student? GetStudentById(int id)
    {
        return studentRepository.GetById(id);
    }

    public List<Student> GetStudents()
    {
        return studentRepository.GetAll();
    }
}
