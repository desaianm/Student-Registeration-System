using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Data.Common;
using Microsoft.SqlServer;
namespace EntityFrameworkExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice = "";

            while (true)
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                Console.WriteLine("*******************************************************************************************************************\n");
                Console.WriteLine("                     Welcome to the Menu\n");
                Console.WriteLine("         Enter 1 for Crud Operations in Student");
                Console.WriteLine("         Enter 2 for Crud Operations in Courses");
                Console.WriteLine("         Enter 3 for Crud Operations in Teachers");
                Console.WriteLine("         Enter 4 for Using WCF Service");
                Console.WriteLine("         Enter 0 for Exiting\n");
                Console.WriteLine("*******************************************************************************************************************\n");
                
                string input = Console.ReadLine();
                if (input == "1")
                {
                    while (true)
                    {
                        SortStudents(choice);
                        Console.WriteLine(" Enter 1 for Insert " +
                            "\n Enter 2 for Update " +
                            "\n Enter 3 for Delete " +
                            "\n Enter 4 for Searching Student Data" +
                            "\n Enter 5 for Sorting Students Ascending");
                        Console.WriteLine(" Enter 6 for Sorting Students Descending");
                        Console.WriteLine(" Enter 0 to go Back to previous Menu ");
                        string ente = Console.ReadLine();
                        if (ente == "1")
                        {
                            Console.WriteLine(" Enter Name To Insert :");
                            string name = Console.ReadLine();
                            InsertStudent(name);
                        }
                        else if (ente == "2")
                        {
                            Console.WriteLine(" Enter Name To Insert :");
                            string name = Console.ReadLine();
                            UpdateStudent(name);
                        }
                        else if (ente == "3")
                        {
                            DeleteStudent();
                        }
                        else if (ente == "0")
                        {
                            break;
                        }
                        else if (ente == "4")
                        {
                            Console.WriteLine("Enter Name To Search :");
                            string name = Console.ReadLine();
                            GetStudentByName(name);
                        }
                        else if (ente == "5")
                        {
                            choice = "a";
                            SortStudents(choice);

                        }
                        else if (ente == "6")
                        {
                            choice = "d";
                            SortStudents(choice);

                        }
                        else
                        {
                            Console.WriteLine("Wrong Input !!! Please Enter Again");
                        }

                    }

                }
                else if (input == "2")
                {
                    while (true)
                    {
                        Console.WriteLine("******************************************************************************************\n");
                        Console.WriteLine(" Enter 1 for Insert \n Enter 2 for Update \n Enter 3 for Delete");
                        Console.WriteLine(" Enter 0 to go Back to previous Menu ");
                        string ente = Console.ReadLine();
                        if (ente == "1")
                        {
                            Console.WriteLine(" Enter Course To Insert :");
                            string course = Console.ReadLine();
                            InsertCourse(course);
                        }
                        else if (ente == "2")
                        {
                            Console.WriteLine(" Enter Course To Insert :");
                            string course = Console.ReadLine();
                            UpdateCourse(course);
                        }
                        else if (ente == "3")
                        {
                            DeleteCourse();
                            printCourses();
                        }
                        else if (ente == "0")
                        {
                            printCourses();
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Wrong Input !!! Please Enter Again");
                        }

                    }
                }
                else if (input == "3")
                {
                    while (true)
                    {

                        Console.WriteLine(" Enter 1 for Insert \n Enter 2 for Update \n Enter 3 for Delete \n Enter 4 for Searching Teachers " +
                            "\n Enter 5 for Sorting Teachers Ascending");
                        Console.WriteLine(" Enter 6 for Sorting Teachers Descending");
                        Console.WriteLine(" Enter 0 to go Back to previous Menu ");
                        string ente = Console.ReadLine();
                        if (ente == "1")
                        {
                            Console.WriteLine(" Enter Teacher To Insert :");
                            string teacher = Console.ReadLine();
                            InsertTeacher(teacher);
                        }
                        else if (ente == "2")
                        {

                            Console.WriteLine(" Enter Teacher To Update :");
                            string teacher = Console.ReadLine();
                            UpdateTeacher(teacher);
                        }
                        else if (ente == "3")
                        {
                            DeleteTeacher();
                            printTeachers();
                        }
                        else if (ente == "4")
                        {
                            Console.WriteLine("Enter Name To Search :");
                            string name = Console.ReadLine();
                            GetTeacherByName(name);
                        }
                        else if (ente == "5")
                        {
                            choice = "a";
                            SortTeachers(choice);

                        }
                        else if (ente == "6")
                        {
                            choice = "d";
                            SortTeachers(choice);
                        }
                        else if (ente == "0")
                        {
                            printTeachers();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input !!! Please Enter Again");
                        }

                    }
                }
                else if (input == "0")
                {
                    break;
                }
                else if (input == "4")
                {
                    Console.WriteLine("*********------WCF Service---------*******");
                    Console.WriteLine("Enter your Physics Marks:");
                   int phy = int.Parse(Console.ReadLine());
                   Console.WriteLine("Enter your Mathematics Marks:");
                   int mat = int.Parse(Console.ReadLine());
                   Console.WriteLine("Enter your Chemistry Marks:");
                   int chem = int.Parse(Console.ReadLine());
                   int total = client.Add(phy, mat,chem);
                   int average = client.Average(phy, mat, chem);
                   Console.WriteLine(" Total Grades: " + total);
                   Console.WriteLine(" Grade Average : " + average);
                   client.Close();

                }

                else
                {
                    Console.WriteLine("Wrong Input !!! Please Enter Again");
                }
            }
        }

        public static void SearchStudents(string name)
        {
            Console.WriteLine("* Search Results *");

            using (var context = new SchoolDBEntities())
            {
                var students = (from s in context.Students
                                where s.StudentName == name
                                select s).ToList();

                var studentsWithSameName = context.Students
                    .GroupBy(s => s.StudentName)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key);

                Console.WriteLine("Students with same name");
                foreach (var stud in studentsWithSameName)
                {
                    Console.WriteLine(stud);
                }

                //Retrieve students of standard 1
                var standard1Students = context.Students
                    .Where(s => s.StandardId == 1)
                    .ToList();
            }

            Console.WriteLine("******************************************************************************************************************\n");
        }


        public static void InsertTeacher(string name)
        {
            using (var context = new SchoolDBEntities())
            {
                var teach = new Teacher()
                {
                    TeacherName = name
                };
                context.Teachers.Add(teach);
                context.SaveChanges();
            }

        }
        public static void UpdateTeacher(string name)
        {
            using (var context = new SchoolDBEntities())
            {
                var teacher = (from t in context.Teachers
                             orderby t.TeacherId descending
                             select t).FirstOrDefault();                
                teacher.TeacherName = name;
                context.SaveChanges();
            }
        }

        public static void DeleteTeacher()
        {
            using (var context = new SchoolDBEntities())
            {
                var teacher = (from t in context.Teachers
                               orderby t.TeacherId descending
                               select t).FirstOrDefault();
                context.Teachers.Remove(teacher);
                context.SaveChanges();
            }
        }



        public static void InsertCourse(string course)
        {
            using (var context = new SchoolDBEntities())
            {
                var crs = new Course()
                {
                    CourseName = course,
                    TeacherId = 10
                };

                context.Courses.Add(crs);
                context.SaveChanges();
            }

        }
        public static void UpdateCourse(string course)
        {
            using (var context = new SchoolDBEntities())
            {
                var crs = context.Courses.First<Course>();
                crs.CourseName = course;
                context.SaveChanges();
            }
        }
        public static void DeleteCourse()
        {
            using (var context = new SchoolDBEntities())
            {
                var std = context.Courses.First<Course>();
                context.Courses.Remove(std);
                context.SaveChanges();
            }
        }


        public static void InsertStudent(string name)
        {
            using (var context = new SchoolDBEntities())
            {
                var std = new Student()
                {
                    StudentName = name
                };

                context.Students.Add(std);
                context.SaveChanges();
            }

        }
        public static void UpdateStudent(string name)
        {
            using (var context = new SchoolDBEntities())
            {
                var student = (from s in context.Students
                           orderby s.StudentID descending
                           select s).FirstOrDefault(); ;
                student.StudentName = name;
                context.SaveChanges();
            }
        }

        public static void DeleteStudent()
        {
            using (var context = new SchoolDBEntities())
            {
                var student = (from s in context.Students
                           orderby s.StudentID descending
                           select s).FirstOrDefault(); ;
                context.Students.Remove(student);
                context.SaveChanges();
            }
        }

        static void GetStudentByName(string name)
        {
            Console.WriteLine("\n** Searching Student Starts  **");
            using (var context = new SchoolDBEntities())
            {
                
                var student = context.Students
                                     .Where(s => s.StudentName.Contains(name))
                                     .FirstOrDefault();

                Console.WriteLine($"{student.StudentID,5} {student.StudentName,-20}");
            }
            Console.WriteLine("******************************************************************************************\n");
        }
        static void GetTeacherByName(string name)
        {
            Console.WriteLine("\n** Searching Teacher Starts  **");
            using (var context = new SchoolDBEntities())
            {
                // LINQ Query Syntax
                //var student = (from s in context.Students
                //               where s.StudentName == name
                //               select s).FirstOrDefault();

                // LINQ Method Syntax

                var teacher = context.Teachers
                                     .Where(t => t.TeacherName.Contains(name))
                                     .FirstOrDefault();

                Console.WriteLine($"{teacher.TeacherId,5} {teacher.TeacherName,-20}");
            }
            Console.WriteLine("******************************************************************************************\n");
        }

        // method to sort the students by name
        static void SortStudents(string choice)
        {

            using (var context = new SchoolDBEntities())
            {
                var students = from s in context.Students
                               orderby s.StudentName descending
                               select s;

                if (choice == "a")
                {
                    students = from s in context.Students
                               orderby s.StudentName ascending
                               select s;

                }
                else
                {
                    students = from s in context.Students
                               orderby s.StudentName descending
                               select s;


                }
                // LINQ Query Syntax
                foreach (var std in students)
                {
                    Console.WriteLine($"{std.StudentID,5} {std.StudentName,-20}");
                }
            }
            Console.WriteLine("******************************************************************************************\n");
        }

        static void printCourses()
        {
            Console.WriteLine("\n * Courses  *");
            using (var context = new SchoolDBEntities())
            {
                var courses = context.Courses;
                string c1 = "CourseID";
                string c2 = "CourseName";
                string c3 = "TeacherID";
                Console.WriteLine($"{c1,5} {c2,-23} {c3,-25}");
                foreach (var c in courses)
                {
                    Console.WriteLine($"{c.CourseId,5} {c.CourseName,-20} {c.TeacherId,-25}");
                }
            }
            Console.WriteLine("* Courses  Ends *\n");
        }

        static void SortTeachers(string choice)
        {

            using (var context = new SchoolDBEntities())
            {
                var teachers = from t in context.Teachers
                               orderby t.TeacherName descending
                               select t;

                if (choice == "a")
                {
                    teachers = from t in context.Teachers
                               orderby t.TeacherName ascending
                               select t;

                }
                else
                {
                    teachers = from t in context.Teachers
                               orderby t.TeacherName descending
                               select t;


                }

                foreach (var t in teachers)
                {
                    Console.WriteLine($"{t.TeacherId,5} {t.TeacherType,10} {t.TeacherName,-25}");
                }
            }
            Console.WriteLine("******************************************************************************************\n");
        }

        static void printTeachers()
        {
            Console.WriteLine("\n * Teachers  *");
            using (var context = new SchoolDBEntities())
            {
                var teacher = context.Teachers;
                string c1 = "TeacherID";
                string c2 = "TeacherType";
                string c3 = "TeacherName";
                Console.WriteLine($"{c1,5} {c2,13} {c3,-25}");

                foreach (var t in teacher)
                {
                    Console.WriteLine($"{t.TeacherId,5} {t.TeacherType,15} {t.TeacherName,-10} ");
                }
            }
            Console.WriteLine("* Teachers  Ends *\n");
        }



    }
}