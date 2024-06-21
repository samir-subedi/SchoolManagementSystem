using System;
using Dapper;
using Internal;

namespace ManagementSystem
{
    public class Admin
    {
        public Admin()
        {
            while (true)
            {
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("             Management System            ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("1.  Teacher Management                    ");
                Console.WriteLine("    11. Add Teacher                       ");
                Console.WriteLine("    12. Delete Teacher                    ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("2.  Student Management                    ");
                Console.WriteLine("    21. Add Student                       ");
                Console.WriteLine("    22. Delete Student                    ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("3.  Subject Management                    ");
                Console.WriteLine("    31. Add Subject                       ");
                Console.WriteLine("    32. Delete Subject                    ");
                Console.WriteLine("    33. Assign Subjects to Teacher        ");
                Console.WriteLine("    34. Assign Class for Subjects         ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("4.  Report Management                     ");
                Console.WriteLine("    41. Student Performance               ");
                Console.WriteLine("    42. Attendance Report                 ");
                Console.WriteLine("    43. Average Marks given by Teacher    ");
                Console.WriteLine("══════════════════════════════════════════");

                Console.WriteLine("Enter an option: ");
                int userResponse = Convert.ToInt32(Console.ReadLine());

                try
                {
                    if (userResponse == 11)
                    {
                        AddTeacher();
                    }
                    else if (userResponse == 12)
                    {
                        DeleteTeacher();
                    }
                    else if (userResponse == 21)
                    {
                        AddStudent();
                    }
                    else if (userResponse == 22)
                    {
                        DeleteStudent();
                    }
                    else if (userResponse == 31)
                    {
                        AddSubject();
                    }
                    else if (userResponse == 32)
                    {
                        DeleteSubject();
                    }
                    else if (userResponse == 33)
                    {
                        AssignSubjectsToTeacher();
                    }
                    else if (userResponse == 34)
                    {
                        AssignClassForSubjects();
                    }
                    else if (userResponse == 41)
                    {
                        GenerateStudentPerformanceReport();
                    }
                    else if (userResponse == 42)
                    {
                        GenerateAttendanceReport();
                    }
                    else if (userResponse == 43)
                    {
                        GenerateTeacherPerformanceReport();
                    }
                    else if (userResponse == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to connect to the database. \n" + ex.Message);
                }
            }
        }

        private void AddTeacher()
        {
            Console.Write("Enter Teacher ID: ");
            int Teacherid = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter First Name: ");
            string Firstname = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string Lastname = Console.ReadLine();

            Console.Write("Enter Contact Number: ");
            string Contactnumber = Console.ReadLine();

            Console.Write("Enter Address: ");
            string Address = Console.ReadLine();

            Console.Write("Enter Username: ");
            string Username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string Password = Console.ReadLine();

            AddTeacherProfile(Teacherid, Firstname, Lastname, Contactnumber, Address, Username, Password);
        }

        private void DeleteTeacher()
        {
            Console.Write("Enter Teacher ID: ");
            int Teacherid = Convert.ToInt32(Console.ReadLine());

            DeleteTeacherProfile(Teacherid);
        }

        private void AddStudent()
        {
            Console.Write("Enter Student ID: ");
            int Studentid = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter First Name: ");
            string Firstname = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string Lastname = Console.ReadLine();

            Console.Write("Enter Contact Number: ");
            string Contactnumber = Console.ReadLine();

            Console.Write("Enter Address: ");
            string Address = Console.ReadLine();

            Console.Write("Enter Username: ");
            string Username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string Password = Console.ReadLine();

            AddStudentProfile(Studentid, Firstname, Lastname, Contactnumber, Address, Username, Password);
        }

        private void DeleteStudent()
        {
            Console.Write("Enter Student ID: ");
            int Studentid = Convert.ToInt32(Console.ReadLine());

            DeleteStudentProfile(Studentid);
        }

        private void AddSubject()
        {
            Console.Write("Enter Subject ID: ");
            int SubjectID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Subject Name: ");
            string SubjectName = Console.ReadLine();

            AddSubjectProfile(SubjectID, SubjectName);
        }

        private void DeleteSubject()
        {
            Console.Write("Enter Subject ID: ");
            int SubjectID = Convert.ToInt32(Console.ReadLine());

            DeleteSubjectProfile(SubjectID);
        }

        private void AssignSubjectsToTeacher()
        {
            Console.Write("Enter Teacher ID: ");
            int TeacherID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Subject ID: ");
            int SubjectID = Convert.ToInt32(Console.ReadLine());

            AssignSubjectToTeacher(TeacherID, SubjectID);
        }

        private void AssignClassForSubjects()
        {
            Console.Write("Enter Subject ID: ");
            int SubjectID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Class ID: ");
            int ClassID = Convert.ToInt32(Console.ReadLine());

            AssignClassToSubject(SubjectID, ClassID);
        }

        private void GenerateStudentPerformanceReport()
        {
            Console.WriteLine("Generating Student Performance Report...");
            using (var connect = SqlConnection.GetConnection())
            {
                string query = @"
                    SELECT s.FirstName, s.LastName, su.SubjectName, m.MarkValue
                    FROM Student s
                    JOIN Marks m ON s.StudentID = m.StudentID
                    JOIN Subject su ON m.SubjectID = su.SubjectID
                    ORDER BY s.StudentID, su.SubjectID";

                var results = connect.Query(query);

                foreach (var row in results)
                {
                    Console.WriteLine($"{row.FirstName} {row.LastName} - {row.SubjectName}: {row.MarkValue}");
                }
            }
        }

        private void GenerateAttendanceReport()
        {
            Console.WriteLine("Generating Attendance Report...");
            using (var connect = SqlConnection.GetConnection())
            {
                string query = @"
                    SELECT s.FirstName, s.LastName, su.SubjectName, a.Date, a.Status
                    FROM Student s
                    JOIN Attendance a ON s.StudentID = a.StudentID
                    JOIN Subject su ON a.SubjectID = su.SubjectID
                    ORDER BY s.StudentID, su.SubjectID, a.Date";

                var results = connect.Query(query);

                foreach (var row in results)
                {
                    Console.WriteLine($"{row.FirstName} {row.LastName} - {row.SubjectName} on {row.Date}: {row.Status}");
                }
            }
        }

        private void GenerateTeacherPerformanceReport()
        {
            Console.WriteLine("Generating Teacher Performance Report...");
            using (var connect = SqlConnection.GetConnection())
            {
                string query = @"
                    SELECT t.FirstName, t.LastName, su.SubjectName, AVG(m.MarkValue) AS AverageMarks
                    FROM Teacher t
                    JOIN TeacherSubject ts ON t.TeacherID = ts.TeacherID
                    JOIN Subject su ON ts.SubjectID = su.SubjectID
                    JOIN Marks m ON su.SubjectID = m.SubjectID
                    GROUP BY t.TeacherID, su.SubjectID";

                var results = connect.Query(query);

                foreach (var row in results)
                {
                    Console.WriteLine($"{row.FirstName} {row.LastName} - {row.SubjectName}: Average Marks = {row.AverageMarks}");
                }
            }
        }

        // Add a Teacher Profile
        public static void AddTeacherProfile(int Teacherid, string Firstname, string Lastname, string Contactnumber, string Address, string Username, string Password)
        {
            using (var connect = SqlConnection.GetConnection())
            {
                string query = "INSERT INTO TEACHER(TeacherID, FirstName, LastName, ContactNumber, Address, Username, Password) VALUES (@TeacherID, @FirstName, @LastName, @ContactNumber, @Address, @Username, @Password)";
                var parameters = new { TeacherID = Teacherid, FirstName = Firstname, LastName = Lastname, ContactNumber = Contactnumber, Address = Address, Username = Username, Password = Password };
                try
                {
                    connect.Execute(query, parameters);
                    Console.WriteLine("Teacher profile created. \n Data Added: \n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There's an issue in creating new Teacher Profile. \n " + ex.Message);
                }
            }
        }

        // Delete an Existing Teacher Profile
        public static void DeleteTeacherProfile(int Teacherid)
        {
            using (var connect = SqlConnection.GetConnection())
            {
                string query = "DELETE FROM TEACHER WHERE TeacherID = @TeacherID";
                var parameters = new { TeacherID = Teacherid };
                try
                {
                    connect.Execute(query, parameters);
                    Console.WriteLine("Teacher profile deleted. \n Data Deleted: \n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There's an issue in deleting the Teacher Profile. \n " + ex.Message);
                }
            }
        }

        // Add a Student Profile
        public static void AddStudentProfile(int Studentid, string Firstname, string Lastname, string Contactnumber, string Address, string Username, string Password)
        {
            using (var connect = SqlConnection.GetConnection())
            {
                string query = "INSERT INTO STUDENT(StudentID, FirstName, LastName, ContactNumber, Address, Username, Password) VALUES (@StudentID, @FirstName, @LastName, @ContactNumber, @Address, @Username, @Password)";
                var parameters = new { StudentID = Studentid, FirstName = Firstname, LastName = Lastname, ContactNumber = Contactnumber, Address = Address, Username = Username, Password = Password };
                try
                {
                    connect.Execute(query, parameters);
                    Console.WriteLine("Student profile created. \n Data Added: \n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There's an issue in creating new Student Profile. \n " + ex.Message);
                }
            }
        }

        // Delete an Existing Student Profile
        public static void DeleteStudentProfile(int Studentid)
        {
            using (var connect = SqlConnection.GetConnection())
            {
                string query = "DELETE FROM STUDENT WHERE StudentID = @StudentID";
                var parameters = new { StudentID = Studentid };
                try
                {
                    connect.Execute(query, parameters);
                    Console.WriteLine("Student profile deleted. \n Data Deleted: \n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There's an issue in deleting the Student Profile. \n " + ex.Message);
                }
            }
        }

        // Add a Subject Profile
        public static void AddSubjectProfile(int SubjectID, string SubjectName)
        {
            using (var connect = SqlConnection.GetConnection())
            {
                string query = "INSERT INTO SUBJECT(SubjectID, SubjectName) VALUES (@SubjectID, @SubjectName)";
                var parameters = new { SubjectID = SubjectID, SubjectName = SubjectName };
                try
                {
                    connect.Execute(query, parameters);
                    Console.WriteLine("Subject profile created. \n Data Added: \n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There's an issue in creating new Subject Profile. \n " + ex.Message);
                }
            }
        }

        // Delete an Existing Subject Profile
        public static void DeleteSubjectProfile(int SubjectID)
        {
            using (var connect = SqlConnection.GetConnection())
            {
                string query = "DELETE FROM SUBJECT WHERE SubjectID = @SubjectID";
                var parameters = new { SubjectID = SubjectID };
                try
                {
                    connect.Execute(query, parameters);
                    Console.WriteLine("Subject profile deleted. \n Data Deleted: \n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There's an issue in deleting the Subject Profile. \n " + ex.Message);
                }
            }
        }

        // Assign Subjects to Teacher
        public static void AssignSubjectToTeacher(int TeacherID, int SubjectID)
        {
            using (var connect = SqlConnection.GetConnection())
            {
                string query = "INSERT INTO TeacherSubject(TeacherID, SubjectID) VALUES (@TeacherID, @SubjectID)";
                var parameters = new { TeacherID = TeacherID, SubjectID = SubjectID };
                try
                {
                    connect.Execute(query, parameters);
                    Console.WriteLine("Subject assigned to teacher. \n Data Added: \n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There's an issue in assigning subject to teacher. \n " + ex.Message);
                }
            }
        }

        // Assign Class for Subjects
        public static void AssignClassToSubject(int SubjectID, int ClassID)
        {
            using (var connect = SqlConnection.GetConnection())
            {
                string query = "INSERT INTO ClassSubject(SubjectID, ClassID) VALUES (@SubjectID, @ClassID)";
                var parameters = new { SubjectID = SubjectID, ClassID = ClassID };
                try
                {
                    connect.Execute(query, parameters);
                    Console.WriteLine("Class assigned for subject. \n Data Added: \n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There's an issue in assigning class for subject. \n " + ex.Message);
                }
            }
        }
    }
}
