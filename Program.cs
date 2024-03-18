using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

class Serializer<T>
{
    public static async Task ToJson(T item, string filePath)
    {
        var json = JsonSerializer.Serialize(item);
        await File.WriteAllTextAsync(filePath, json);
    }

    public static async Task<T> FromJson(string filePath)
    {
        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<T>(json);
    }
}

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public readonly int RollNumber;
    public string Grade { get; set; }

    // Default constructor required for deserialization
    public Student() { }

    public Student(int rollNumber)
    {
        RollNumber = rollNumber;
    }
}


public class StudentList<T> : List<T> where T : Student
{
    public IEnumerable<T> SearchByName(string name)
    {
        return this.Where(s => s.Name == name);
    }

    public IEnumerable<T> SearchByRollNumber(int rollNumber)
    {
        return this.Where(s => s.RollNumber == rollNumber);
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        // Creating student list
        var studentList = new StudentList<Student>
        {
            new Student(1) { Name = "John", Age = 18, Grade = "A" },
            new Student(2) { Name = "Alice", Age = 20, Grade = "B" }
        };

        // Serialization
        await Serializer<StudentList<Student>>.ToJson(studentList, "C:\\Users\\fedas\\source\\repos\\LINQ\\students.json");

        // Deserialization
        var loadedList = await Serializer<StudentList<Student>>.FromJson("C:\\Users\\fedas\\source\\repos\\LINQ\\students.json");

        // Displaying all students
        Console.WriteLine("All Students:");
        foreach (var student in loadedList)
        {
            Console.WriteLine($"Name: {student.Name}, Roll Number: {student.RollNumber}, Age: {student.Age}, Grade: {student.Grade}");
        }

        Console.WriteLine("\nSearch Results by Name (John):");
        foreach (var student in loadedList.SearchByName("John"))
        {
            Console.WriteLine($"Name: {student.Name}, Roll Number: {student.RollNumber}, Age: {student.Age}, Grade: {student.Grade}");
        }

        Console.WriteLine("\nSearch Results by Roll Number (2):");
        foreach (var student in loadedList.SearchByRollNumber(2))
        {
            Console.WriteLine($"Name: {student.Name}, Roll Number: {student.RollNumber}, Age: {student.Age}, Grade: {student.Grade}");
        }
    }
}
