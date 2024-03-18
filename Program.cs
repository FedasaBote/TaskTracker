using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public readonly int RollNumber;
    public string Grade { get; set; }

    public Student(int rollNumber)
    {
        RollNumber = rollNumber;
    }
}

public class StudentList<T> where T : Student
{
    public List<T> students = new List<T>();

    public void AddStudent(T student)
    {
        students.Add(student);
    }

    public IEnumerable<T> SearchByName(string name)
    {
        return students.Where(student => student.Name == name);
    }

    public IEnumerable<T> SearchByRollNumber(int rollNumber)
    {
        return students.Where(student => student.RollNumber == rollNumber);
    }
}

public class FileManager
{
    public static void SerializeToJson<T>(T obj, string filePath)
    {
        Console.WriteLine(obj); // StudentList`1
        string json = JsonSerializer.Serialize(obj);
        File.WriteAllText(filePath, json);
    }

    public static T DeserializeFromJson<T>(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json);
    }
}

class Program
{
    static void Main(string[] args)
    {
        StudentList<Student> studentList = new StudentList<Student>();
        studentList.AddStudent(new Student(1) { Name = "John", Age = 18, Grade = "A" });
        studentList.AddStudent(new Student(2) { Name = "Bilise", Age = 20, Grade = "B" });

        FileManager.SerializeToJson(studentList, "C:\\Users\\fedas\\source\\repos\\LINQ\\students.json");

        StudentList<Student> loadedWrapper = FileManager.DeserializeFromJson<StudentList<Student>>("C:\\Users\\fedas\\source\\repos\\LINQ\\students.json");
        // get length of the list
        Console.WriteLine($"All Students: {loadedWrapper.students.Count}");
        foreach (var student in loadedWrapper.SearchByName("John"))
        {
            Console.WriteLine($"Name: {student.Name}, Roll Number: {student.RollNumber}, Age: {student.Age}, Grade: {student.Grade}");
        }

        Console.WriteLine("\nSearch Results by Roll Number:");
        foreach (var student in loadedWrapper.SearchByRollNumber(2))
        {
            Console.WriteLine($"Name: {student.Name}, Roll Number: {student.RollNumber}, Age: {student.Age}, Grade: {student.Grade}");
        }
    }
}
