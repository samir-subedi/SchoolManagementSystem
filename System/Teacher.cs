using Dapper;


namespace ManagementSystem
{
    public class Teacher
    {
        string loggedInUsername;
        public Teacher(string username)
        {

           while (true)
            {


                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("             Management System            ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("1.  Student Management                    ");
                Console.WriteLine("    11. View Students Assigned            ");
                Console.WriteLine("    12. Search Students                   ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("2.  Class Management                      ");
                Console.WriteLine("    21. View Class Assigned               ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("3.  Subject Management                    ");
                Console.WriteLine("    31. View Subjects Assigned            ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("4.  Marks Management                      ");
                Console.WriteLine("    41. Assign Marks                      ");
                Console.WriteLine("    42. Update Marks                      ");
                Console.WriteLine("    43. View Marks                        ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("5.  Attendance Management                 ");
                Console.WriteLine("    51. Attendance for a Class            ");
                Console.WriteLine("    52. Update Attendance                 ");
                Console.WriteLine("══════════════════════════════════════════");
                Console.WriteLine("    0.  Exit                              ");
                Console.WriteLine("══════════════════════════════════════════");



                loggedInUsername = username;
                Console.Write("Enter a Option: ");
                int userResponse = Convert.ToInt32(Console.ReadLine());

                if (userResponse == 11)
                {
                    int teacherid = IdConverter(loggedInUsername);

                    List<string> subjects = SubjectAssigned(teacherid);

                    Console.WriteLine();
                    // loop through the list of subjects that are assigned to teacher
                    foreach (string sub in subjects)
                    {
                        Console.WriteLine($"\t \t {sub}: ");
                        Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -30} | {4, -30}", "Student ID", "First Name", "Last Name", "Contact Number", "Address");
                        Console.WriteLine(new string('-', 109));

                        // stores student details for the subject passed as argument
                        List<Student> students = DisplayStudents(sub);
                        foreach (var student in students)
                        {
                            Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -30} | {4, -30}", student.StudentID, student.FirstName, student.LastName, student.ContactNumber, student.Address);
                            Console.WriteLine();
                        }

                    }

                }

                else if (userResponse == 12)
                {
                    var connect = SqlConnection.GetConnection();
                    Console.WriteLine("Enter student ID to search: ");
                    int studentId = Convert.ToInt32(Console.ReadLine());
                    string studentIdExistsQuery = "SELECT StudentID from Student Where studentID = @studentID";
                    var parameterForStudentIdExistsQuery = new { studentID = studentId };
                    var student = connect.Query<int>(studentIdExistsQuery, parameterForStudentIdExistsQuery);
                    if (student.Contains(studentId))
                    {
                        SearchStudent(studentId);

                    }
                    else
                    {
                        Console.WriteLine("Student details not found.");
                    }

                }

                else if  (userResponse == 31)
                {
                    // returns teacher id 
                    int teacherid = IdConverter(loggedInUsername);

                    // returns the list of subjects assigned
                    List<string> subjects = SubjectAssigned(teacherid);


                    foreach (var sub in subjects)
                    {
                        Console.WriteLine("\t" + sub);
                    }
                }

                else if  (userResponse == 41)
                {
                    while (true)
                    {
                        var connect = SqlConnection.GetConnection();
                        Console.Write("Enter Student ID: ");
                        int studentId = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Subject ID: ");
                        int subjectId = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Mark: ");
                        decimal markValue = Convert.ToDecimal(Console.ReadLine());

                        string MarksassignQuery = "INSERT INTO Marks (StudentID, SubjectID, MarkValue) VALUES (@StudentID, @SubjectID, @MarkValue)";
                        var parameterForMarksassignQuery = new { StudentID = studentId, SubjectID = subjectId, MarkValue = markValue };

                        connect.Execute(MarksassignQuery, parameterForMarksassignQuery);

                        Console.WriteLine("");
                        string continueToAdd = Console.ReadLine();

                        if (continueToAdd != "YES" || continueToAdd != "yes")
                        {
                            break;
                        }
                    }

                }

                else if (userResponse == 42)
                {
                    var connect = SqlConnection.GetConnection();

                    Console.Write("Enter Student ID: ");
                    int studentId = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Subject ID: ");
                    int subjectId = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Mark: ");
                    decimal markValue = Convert.ToDecimal(Console.ReadLine());

                    string markUpdateQuery = "UPDATE Marks Set MarkValue = @MarkValue WHERE StudentID = @StudentID AND SubjectID = @SubjectID";
                    var parameterForMarkUpdateQuery = new { MarkValue = markValue, StudentID = studentId, SubjectID = subjectId };
                    connect.Execute(markUpdateQuery, parameterForMarkUpdateQuery);

                }
                else if (userResponse == 43)
                {
                    var connect = SqlConnection.GetConnection();

                    Console.Write("Enter Student ID: ");
                    int studentId = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Subject ID: ");
                    int subjectId = Convert.ToInt32(Console.ReadLine());

                    string markVieWQuery = "SELECT MarkValue FROM Marks WHERE StudentID= @StudentID AND SubjectID= @SubjectID";
                    var parameterForMarkViewQuery = new { StudentID = studentId, SubjectID = subjectId };
                    var mark = connect.QueryFirstOrDefault<float>(markVieWQuery, parameterForMarkViewQuery);
                    if (mark != 0)
                    {
                        Console.WriteLine($"Marks for the subject code: {subjectId} and Student ID: {studentId} is {mark}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid StudentId or SubjectID");
                    }
                }
                else if (userResponse == 51)
                {
                    var connect = SqlConnection.GetConnection();
                    int teacherId = IdConverter(loggedInUsername);
                    List<string> subjectsAssigned = SubjectAssigned(teacherId);
                    for (int i = 1; i <= subjectsAssigned.Count(); i++)
                    {
                        Console.WriteLine($"{i}. {subjectsAssigned[i - 1]}");
                    }
                    Console.Write("Select the subject for attendance: ");
                    int subjectSelection = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -30} | {4, -30}", "Student ID", "First Name", "Last Name", "Contact Number", "Address");

                    var students = DisplayStudents(subjectsAssigned[subjectSelection - 1]);
                    string subjectIdQuery = "SELECT SubjectID FROM Subject WHERE SubjectName = @SubjectName";
                    int subjectId = connect.QuerySingleOrDefault<int>(subjectIdQuery, new { SubjectName = subjectsAssigned[subjectSelection - 1] });

                    foreach (var student in students)
                    {
                        Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -30} | {4, -30}", student.StudentID, student.FirstName, student.LastName, student.ContactNumber, student.Address);
                        Console.Write("Attendence Status (Absent/Present): ");
                        string attendenceStatus = Console.ReadLine().ToUpper();

                        string attendanceAddQuery = "INSERT INTO Attendance (StudentID, SubjectID, Date, Status) VALUES (@StudentID, @SubjectID, @Date, @Status)";
                        var parameterForAttendanceAddQuery = new { StudentID = student.StudentID, SubjectID = subjectId, Date = DateTime.Now, Status = attendenceStatus };
                        connect.Execute(attendanceAddQuery, parameterForAttendanceAddQuery);

                    }
                }

                else if (userResponse == 52)
                {
                    var connect = SqlConnection.GetConnection();

                    Console.Write("Enter the student id: ");
                    int studentId = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter the subject id: ");
                    int subjectId = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter the date of attendence (YYy-mm-dd:");
                    DateTime Date = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Enter the status: ");
                    string status = Console.ReadLine();

                    string attendanceUpdateQuery = "UPDATE Attendance SET Status = @Status WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND Date = @Date";
                    var parameterForAttendanceUpdateQuery = new { Status = status, StudentID = studentId, SubjectID = subjectId, Date = Date };
                    connect.Execute(attendanceUpdateQuery, parameterForAttendanceUpdateQuery);
                }

                else if (userResponse == 0)
                {
                    break;
                }
            }


            // returns teacherid of the currentlyloggedin user
            static int IdConverter(string loggedInUsername)
            {
                var connect = SqlConnection.GetConnection();

                // stores a sql query that returns teacherid based on the username provided (logged in username)
                String query = "SELECT TeacherID FROM Teacher WHERE Username = @Username";

                // assign username of currently logged in teacher to the username variable
                var parameters = new { Username = loggedInUsername };

                // returns teacherid of the currently logged in teacher
                int teacherid = connect.QuerySingleOrDefault<int>(query, parameters);

                return teacherid;


            }

            // returns a list of subject assigned to a teacher
            static List<string> SubjectAssigned(int teacherid)
            {
                var connect = SqlConnection.GetConnection();
                // joins subject and  teacher subject table and return list of subjectname 
                string subjectAssignedQuery = "SELECT s.SubjectName, s.SubjectID " + "FROM Subject s " + "JOIN TeacherSubject ts ON s.SubjectID = ts.SubjectID " + "WHERE ts.TeacherID = @TeacherID";

                var parametersForSubjectAssignedQuery = new { TeacherID = teacherid };

                var subjects = connect.Query<string>(subjectAssignedQuery, parametersForSubjectAssignedQuery);
                return new List<string>(subjects);
            }

            static void SearchStudent(int studentId)
            {
                var connect = SqlConnection.GetConnection();
                string studentSearchQuery = "SELECT StudentID, FirstName, LastName, ContactNumber, Address FROM Student WHERE StudentID = @StudentID";
                var parameterForStudentSearchQuery = new { StudentID = studentId };
                var studentDetails = connect.Query<StudentDetails>(studentSearchQuery, parameterForStudentSearchQuery);
                Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -30} | {4, -30}", "Student ID", "First Name", "Last Name", "Contact Number", "Address");
                Console.WriteLine(new string('-', 109));

                Console.WriteLine();
                foreach (var student in studentDetails)
                {
                    Console.WriteLine("{0, -15} | {1, -15} | {2, -15} | {3, -30} | {4, -30}", student.StudentID, student.FirstName, student.LastName, student.ContactNumber, student.Address);
                    Console.WriteLine();
                }


            }

            // return list of students enrolled in each subject
            static List<Student> DisplayStudents(string subjectname)
            {
                var connect = SqlConnection.GetConnection();

                // join three tables: student, studentsubject and subject
                string displayStudentsQuery = "SELECT s.StudentID, s.Firstname, s.Lastname, s.ContactNumber, s.Address " + "From Student s " + "JOIN StudentSubject ss ON s.StudentID = ss.StudentID " + "JOIN Subject sb ON ss.SubjectID = sb.SubjectID " + "WHERE sb.SubjectName = @Subjectname";
                var parametersForDisplayStudentsQuery = new { Subjectname = subjectname };

                // returns multiple columns of student table and map it to student object
                var students = connect.Query<Student>(displayStudentsQuery, parametersForDisplayStudentsQuery).ToList();

                return students;
            }
        }

        // dummy class to store the list of students returned by DisplayStudents Method
        private class Student
        {
            public int StudentID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ContactNumber { get; set; }
            public string Address { get; set; }


        }

        // dummy class to store the student details returned
        private class StudentDetails
        {
            public int StudentID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ContactNumber { get; set; }
            public string Address { get; set; }


        }

    }

    }









