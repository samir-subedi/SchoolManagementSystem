using Dapper;

namespace ManagementSystem
{
    public class Student
    {
        private readonly string _username;

        // Constructor to initialize the username and display the menu
        public Student(string username)
        {
            _username = username;

            // Display the menu in a loop until the user chooses to exit
            while (true)
            {
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("             Student Menu                 ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("1.  View Attendance                       ");
                Console.WriteLine("2.  View Marks                            ");
                Console.WriteLine("3.  View Percentage                       ");
                Console.WriteLine("4.  View Subjects Assigned                ");
                Console.WriteLine("    0.  Exit                              ");
                Console.WriteLine("══════════════════════════════════════════");

                Console.Write("Enter an Option: ");

                int userResponse;
                if (!int.TryParse(Console.ReadLine(), out userResponse)) // Check if input is a valid integer
                {
                    Console.WriteLine("Please enter a valid option.");
                    continue; // Continue to next iteration of the loop
                }

                // Call the appropriate method based on the user's choice
                switch (userResponse)
                {
                    case 1:
                        ViewAttendance();
                        break;
                    case 2:
                        ViewMarks();
                        break;
                    case 3:
                        ViewPercentage();
                        break;
                    case 4:
                        ViewSubjectsAssigned();
                        break;
                    case 0:
                        return; // Exit the loop if the user chooses to exit
                    default:
                        Console.WriteLine("Invalid choice."); // Inform the user of an invalid choice
                        break;
                }
            }
        }

        // Method to view attendance records
        private void ViewAttendance()
        {
            var connect = SqlConnection.GetConnection(); // Establish a database connection
            string query = "SELECT a.Date, a.Status FROM Attendance a " +
                           "JOIN Student s ON a.StudentID = s.StudentID " +
                           "WHERE s.Username = @Username";
            var parameters = new { Username = _username };
            var attendanceRecords = connect.Query(query, parameters);

            Console.WriteLine("Attendance Records:");
            foreach (var record in attendanceRecords)
            {
                Console.WriteLine($"Date: {record.Date}, Status: {record.Status}");
            }
        }

        // Method to view marks records
        private void ViewMarks()
        {
            var connect = SqlConnection.GetConnection(); // Establish a database connection
            string query = "SELECT sub.SubjectName, m.MarkValue FROM Marks m " +
                           "JOIN Student s ON m.StudentID = s.StudentID " +
                           "JOIN Subject sub ON m.SubjectID = sub.SubjectID " +
                           "WHERE s.Username = @Username";
            var parameters = new { Username = _username };
            var marksRecords = connect.Query(query, parameters);

            Console.WriteLine("Marks Records:");
            foreach (var record in marksRecords)
            {
                Console.WriteLine($"Subject: {record.SubjectName}, Marks: {record.MarkValue}");
            }
        }

        // Method to view percentage records
        private void ViewPercentage()
        {
            var connect = SqlConnection.GetConnection(); // Establish a database connection
            string query = "SELECT SUM(m.MarkValue) AS TotalMarks, COUNT(m.SubjectID) AS TotalSubjects " +
                           "FROM Marks m " +
                           "JOIN Student s ON m.StudentID = s.StudentID " +
                           "WHERE s.Username = @Username";
            var parameters = new { Username = _username };
            var result = connect.QuerySingle(query, parameters);

            decimal totalMarks = result.TotalMarks;
            decimal totalSubjects = result.TotalSubjects;
            decimal percentage = (totalMarks / totalSubjects);

            Console.WriteLine("Percentage:");
            Console.WriteLine($"Total Marks: {totalMarks}, Total Subjects: {totalSubjects}, Percentage: {percentage:F2}%");
        }

        // Method to view subjects assigned to the student
        private void ViewSubjectsAssigned()
        {
            var connect = SqlConnection.GetConnection(); // Establish a database connection
            string query = "SELECT sub.SubjectName FROM StudentSubject ss " +
                           "JOIN Student s ON ss.StudentID = s.StudentID " +
                           "JOIN Subject sub ON ss.SubjectID = sub.SubjectID " +
                           "WHERE s.Username = @Username";
            var parameters = new { Username = _username };
            var subjectsRecords = connect.Query(query, parameters);

            Console.WriteLine("Subjects Assigned:");
            foreach (var record in subjectsRecords)
            {
                Console.WriteLine($"Subject: {record.SubjectName}");
            }
        }
    }
}
