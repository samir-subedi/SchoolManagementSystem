using Dapper;


namespace ManagementSystem
{
    class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║          School Management System - Login Page     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Console.WriteLine(" 1. Admin");
            Console.WriteLine(" 2. Teacher");
            Console.WriteLine(" 3. Student");

            Console.Write("Enter your role: ");
            int role = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter your username: ");
            String username = Console.ReadLine();

            Console.Write("Enter your password: ");
            String password = Console.ReadLine();


            //String connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=DETAILS;";

            try
            {
                using (var connect = SqlConnection.GetConnection())
                {
                    connect.Open();

                    if (role == 1)
                    {
                        // returns the number of rows from admin table where username and password matched
                        string query = "SELECT COUNT(*) FROM ADMIN WHERE username = @Username and password = @Password";

                        // assign parameter to @username and @password in query
                        var parameters = new { Username = username, Password = password };

                        // if the match is found count will be 1 else it will be 0
                        int count = connect.QuerySingleOrDefault<int>(query, parameters);

                        if (count > 0)
                        {
                            Admin admin = new Admin();

                        }
                        else
                        {
                            Console.WriteLine("Invalid Username or Password");
                        }
                    }
                    else if (role == 2)
                    {
                        // returns the number of rows from teacher table where username and password matches
                        string query = "SELECT COUNT(*) FROM TEACHER WHERE username = @Username and password = @Password";
                        var parameters = new { Username = username, Password = password };
                        int count = connect.QuerySingleOrDefault<int>(query, parameters);
                        if (count > 0)
                        {
                            Teacher teacher = new Teacher(username);
                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid Username or Password");
                        }
                    }
                    else if (role == 3)
                    {
                        // returns the number of rows from student table where username and password matches
                        string query = "SELECT COUNT(*) FROM STUDENT WHERE username = @Username and password = @Password";
                        var parameters = new { Username = username, Password = password };
                        int count = connect.QuerySingleOrDefault<int>(query, parameters);
                        if (count > 0)
                        {
                            Student student = new Student(username);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Username or Password");
                        }
                    }

                    else
                    {
                        Console.WriteLine("Invalid LogIn Credentials Provided!!");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unexpected Error:");
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine("Press Enter key to exit...");
            Console.ReadLine();
        }


    }
}