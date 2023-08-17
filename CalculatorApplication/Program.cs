using CalculatorApplication.DatabaseContext;
using CalculatorApplication.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CalculatorApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CalculatorDbContext())
            {
                context.Database.EnsureCreated();

                while (true)
                {
                    Console.WriteLine("Calculator Menu:");
                    Console.WriteLine("1. Addition");
                    Console.WriteLine("2. Subtraction");
                    Console.WriteLine("3. Multiplication");
                    Console.WriteLine("4. Division");
                    Console.WriteLine("5. View History");
                    Console.WriteLine("6. Exit");

                    Console.Write("Select an option: ");
                    int choice = int.Parse(Console.ReadLine());

                    if (choice == 6)
                    {
                        Console.WriteLine("Goodbye !");
                        break;
                    }
                    else if (choice == 5)
                    {
                        Console.WriteLine("Calculation History:\n");
                        var colNames = context.Model.FindEntityType(typeof(Calculator));
                        var propertyNames = colNames.GetProperties().Select(p => p.GetColumnName()).ToList();
                        Console.WriteLine(string.Join(" | ", propertyNames) + "\n-------------------------------------------------------------");

                        foreach (var history in context.History)
                        {
                            Console.WriteLine($"{history.CalculatorId} | {history.Operand1} | {history.Operand2}| {history.Operation} = {history.Result}");
                        }
                    }
                    else if (choice >= 1 && choice <= 4)
                    {
                        Console.Write("Enter first number: ");
                        double operand1 = double.Parse(Console.ReadLine());


                        Console.Write("Enter second number: ");
                        double operand2 = double.Parse(Console.ReadLine());

                        double result = 0;
                        string operation = string.Empty;
                        string operationSign = string.Empty;

                        switch (choice)
                        {
                            case 1:
                                result = operand1 + operand2;
                                operation = "Addition";
                                operationSign = "+";
                                break;
                            case 2:
                                result = operand1 - operand2;
                                operation = "Subtraction";
                                operationSign = "-";
                                break;
                            case 3:
                                result = operand1 * operand2;
                                operation = "Multiplication";
                                operationSign = "*";
                                break;
                            case 4:
                                result = operand1 / operand2;
                                operation = "Division";
                                operationSign = "/";
                                break;
                        }

                        context.History.Add(new Calculator
                        {
                            Operation = operation,
                            Operand1 = operand1,
                            Operand2 = operand2,
                            Result = result
                        });

                        context.SaveChanges();

                        Console.WriteLine($"Result: {operand1} {operationSign} {operand2} = {result}");
                        Console.WriteLine("Calculation saved to history.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                    }
                }
            }
        }
    }
}
