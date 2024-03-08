// See https://aka.ms/new-console-template for more information

class Program
{

    static void Main(string[] args)
    {
        Console.Write("Please Enter your name");
        string name = Console.ReadLine();

        Console.WriteLine("Enter the number of subjects: ");

        int numberOfSubjects;

        while(!int.TryParse(Console.ReadLine(),out numberOfSubjects) )
        {
            Console.WriteLine("Invalid input. Please enter a number: ");
        }

        Dictionary<string,double> marks = new Dictionary<string, double>();

        for (int i = 0; i < numberOfSubjects; i++)
        {
            Console.WriteLine("Enter the subject name: ");
            string subject = Console.ReadLine();

            double grade;
            do
            {
              Console.Write($"Enter the grade for {subject}: ");
            } while (!double.TryParse(Console.ReadLine(), out grade) || grade < 0 || grade > 100);
            marks.Add(subject,grade);
        }   

        double totalGrade = 0;

        foreach(var mark in marks)
        {
            totalGrade += mark.Value;
        }

        double avgGrade = totalGrade / marks.Count;

        Console.WriteLine($"The average grade for {name} is {avgGrade}");
    }
}
