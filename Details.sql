DROP DATABASE IF EXISTS DETAILS;
CREATE DATABASE IF NOT EXISTS DETAILS;
USE DETAILS;

-- Create Teacher table
CREATE TABLE IF NOT EXISTS Teacher (
    TeacherID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    ContactNumber VARCHAR(20),
    Address VARCHAR(100),
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(50)
);

-- Create Admin table
CREATE TABLE IF NOT EXISTS Admin (
    AdminID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    ContactNumber VARCHAR(20),
    Address VARCHAR(100),
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(50)
);

-- Create Student table
CREATE TABLE IF NOT EXISTS Student (
    StudentID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    ContactNumber VARCHAR(20),
    Address VARCHAR(100),
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(50)
);

-- Create Class table
CREATE TABLE IF NOT EXISTS Class (
    ClassID INT PRIMARY KEY,
    ClassName VARCHAR(50),
    GradeLevel INT
);

-- Create Subject table
CREATE TABLE IF NOT EXISTS Subject (
    SubjectID INT PRIMARY KEY,
    SubjectName VARCHAR(100)
);

-- Create TeacherSubject junction table
CREATE TABLE IF NOT EXISTS TeacherSubject (
    TeacherSubjectID INT AUTO_INCREMENT PRIMARY KEY,
    TeacherID INT,
    SubjectID INT,
    FOREIGN KEY (TeacherID) REFERENCES Teacher(TeacherID),
    FOREIGN KEY (SubjectID) REFERENCES Subject(SubjectID)
);

-- Create StudentSubject junction table
CREATE TABLE IF NOT EXISTS StudentSubject (
    StudentSubjectID INT AUTO_INCREMENT PRIMARY KEY,
    StudentID INT,
    SubjectID INT,
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (SubjectID) REFERENCES Subject(SubjectID)
);

-- Create Attendance table
CREATE TABLE IF NOT EXISTS Attendance (
    AttendanceID INT AUTO_INCREMENT PRIMARY KEY,
    StudentID INT,
    SubjectID INT,
    Date DATE,
    Status VARCHAR(10),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (SubjectID) REFERENCES Subject(SubjectID)
);

-- Create Marks table
CREATE TABLE IF NOT EXISTS Marks (
    MarkID INT AUTO_INCREMENT PRIMARY KEY,
    StudentID INT,
    SubjectID INT,
    MarkValue DECIMAL(5,2),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (SubjectID) REFERENCES Subject(SubjectID)
);


-- Insert values into Teacher table
INSERT INTO Teacher (TeacherID, FirstName, LastName, ContactNumber, Address, Username, Password)
VALUES
    (1, 'John', 'Doe', '0412345678', '123 Main St, Springfield', 'john_doe', 'password1'),
    (2, 'Jane', 'Smith', '0423456789', '456 Elm St, Springfield', 'jane_smith', 'password2'),
    (3, 'Bob', 'Johnson', '0434567890', '789 Oak St, Springfield', 'bob_johnson', 'password3');

-- Insert values into Admin table
INSERT INTO Admin (AdminID, FirstName, LastName, ContactNumber, Address, Username, Password)
VALUES
    (1, 'Admin', 'Admin', '0455555555', '1 Admin St, Adminville', 'admin', 'adminpassword');

-- Insert values into Student table
INSERT INTO Student (StudentID, FirstName, LastName, ContactNumber, Address, Username, Password)
VALUES
    (1, 'Alice', 'Jones', '0411111111', '111 Maple St, Springfield', 'alice_jones', 'password4'),
    (2, 'Bob', 'Smith', '0422222222', '222 Pine St, Springfield', 'bob_smith', 'password5'),
    (3, 'Charlie', 'Brown', '0433333333', '333 Cedar St, Springfield', 'charlie_brown', 'password6');

-- Insert values into Class table 
-- grade level means semester
INSERT INTO Class (ClassID, ClassName, GradeLevel)
VALUES
    (1, 'Class 1', 1),
    (2, 'Class 2', 2),
    (3, 'Class 3', 3);

-- Insert values into Subject table
INSERT INTO Subject (SubjectID, SubjectName)
VALUES
    (1, 'Mathematics'),
    (2, 'Science'),
    (3, 'English'),
    (4, 'History');

-- Insert values into Marks table
INSERT INTO Marks (StudentID, SubjectID, MarkValue)
VALUES
    (1, 1, 85.50), (1, 2, 78.00), (1, 3, 92.30),
    (2, 2, 88.75), (2, 3, 69.50), (2, 4, 74.25),
    (3, 3, 81.00), (3, 4, 77.25);

-- Insert values into Attendance table
INSERT INTO Attendance (StudentID, SubjectID, Date, Status)
VALUES
    (1, 1, '2024-05-01', 'Present'), (1, 2, '2024-05-02', 'Absent'), (1, 3, '2024-05-03', 'Present'),
    (2, 2, '2024-05-01', 'Present'), (2, 3, '2024-05-02', 'Present'), (2, 4, '2024-05-03', 'Absent'),
    (3, 3, '2024-05-01', 'Absent'), (3, 4, '2024-05-02', 'Present');

-- Insert valus into TeacherSubject table
INSERT INTO TeacherSubject (TeacherID, SubjectID)
VALUES
    (1, 1), (1, 2), (1, 3),
    (2, 2), (2, 3),-- Insert valus into TeacherSubject table (continued)
    (2, 4),
    (3, 3), (3, 4);

-- Insert values into StudentSubject table
INSERT INTO StudentSubject (StudentID, SubjectID)
VALUES
    (1, 1), (1, 2), (1, 3),
    (2, 2), (2, 3), (2, 4),
    (3, 3), (3, 4);


