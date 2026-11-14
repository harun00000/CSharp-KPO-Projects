using System;

// entity
// ubiquitous language: Student - студент
public class Student
{
    public int Id { get; }
    public string Name { get; }

    public Student(int id, string name)
    {
        // invariant: student id must be positive
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("id", "Идентификатор должен быть > 0");
        }
        Id = id;

        // invariant: student name can't be empty
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Пустое имя:(", "name");
        }
        Name = name;
    }
}
