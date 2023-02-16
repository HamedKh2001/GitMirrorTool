using System.Diagnostics;

namespace GitMirror
{
    internal class Program
    {
        static string SourceUrl = string.Empty;
        static string DestinationUrl = string.Empty;
        static string WorkingDirectory = Environment.CurrentDirectory;
        static void Main(string[] args)
        {
            Console.WriteLine($"Enter Source Git Url?");
            SourceUrl = InputValidator(Console.ReadLine());
            Console.WriteLine($"Enter Destination Git Url?");
            DestinationUrl = InputValidator(Console.ReadLine());

            CommandExecutor($"git clone --mirror {SourceUrl}", WorkingDirectory);
            WorkingDirectory = Path.Combine(WorkingDirectory, SourceUrl.Split('/').ToList().Last()); ;
            CommandExecutor($"git remote set-url --push origin {DestinationUrl}", WorkingDirectory);
            CommandExecutor($"git push --mirror", WorkingDirectory);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Mirroring is Done :)");
            Console.ReadLine();
        }


        static string InputValidator(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid Input...Try Again...");
                Console.ForegroundColor = ConsoleColor.White;
                InputValidator(Console.ReadLine());
            }
            return string.Empty;
        }

        static void CommandExecutor(string command, string workingDirectory)
        {
            command = "/c " + command;
            var p = new Process
            {
                StartInfo =
                    {
                        FileName = "C:\\Windows\\system32\\cmd.exe",
                        WorkingDirectory = workingDirectory,
                        Arguments = command
                    }
            };
            p.Start();
        }
    }
}