using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var db = new ContosoUniversityEntities())
            {
                Method7(db);
                //db.Person.Where(per => per.HireDate.HasValue == true && per.HireDate.Value.Month == 7).ForEachAsync(WritePerson);


            }

            Thread.Sleep(Timeout.Infinite);
        }

        private static void Method7(ContosoUniversityEntities db)
        {

            //var instructors = db.Person
            //    .Include(person => person.OfficeAssignment)
            //    .Where(person => person.Discriminator.Equals("Instructor"))
            //    .Where(person => person.OfficeAssignment != null);

            var instructorsIds = db.OfficeAssignment.Select(off => off.InstructorID).Distinct().ToArray();


            var instructors = db.Person
                .Where(person => instructorsIds.Contains(person.ID));




            foreach (var instructor in instructors)
            {
                WriteInstructor(instructor);
            }



        }

        private static void Method6(ContosoUniversityEntities db)
        {

            var instructors = db.Person.Where(discri => discri.Discriminator.Equals("Instructor"));

            foreach (var instructor in instructors)
            {
                WriteInstructor(instructor);
            }



        }
        private static void Method5(ContosoUniversityEntities db, String name)
        {
            var xs = db.Person.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name));

            foreach (var item in xs)
            {
                WritePerson(item);
            }

        }

        private static void Method4(ContosoUniversityEntities db, int instructorId)
        {
            var departments = db.Department.Where(x => x.InstructorID == instructorId);

            foreach (var item in departments)
            {
                WriteDepart(item);
            }

        }

        private static void Method3(ContosoUniversityEntities db)
        {
            var persons = db.Person.Where(person => person.Enrollment.Any(enrol => enrol.Grade == 1));

            foreach (var item in persons)
            {
                WritePerson(item);
            }
        }

        private static void Method2(ContosoUniversityEntities db)
        {
            //int[] vals = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //var pares = vals.Where(val => val % 2 == 0);

            //var paresList = pares.ToList();


            var instructors = from person in db.Person
                              where person.Discriminator == "Instructor"
                              select person;

            var instructors2 = db.Person.Where(person => person.Discriminator == "Instructor");

            foreach (var item in instructors2)
            {
                WritePerson(item);
            }
        }

        private static bool OnlyInstructors(Person person)
        {
            return person.Discriminator == "Instructor";
        }

        private static void Method1(ContosoUniversityEntities db)
        {
            Person[] list = db.Person.ToArray();
            foreach (var item in list)
            {
                if (item.Discriminator == "Instructor")
                {
                    WritePerson(item);
                }
            }
        }

        static void WritePerson(Person p)
        {
            Console.WriteLine($"Id: {p.ID}, FirstName: {p.FirstName}, LastName: {p.LastName}, Discriminator: {p.Discriminator}");
        }
        static void WriteDepart(Department d)
        {
            Console.WriteLine($"DepartmentID: {d.DepartmentID}, Name: {d.Name}, Budget: {d.Budget}, StartDate: {d.StartDate}, InstructorID: {d.InstructorID}");
        }
        static void WriteInstructor(Person p)
        {
            Console.WriteLine($"Name: {p.FirstName}, Location: {p.OfficeAssignment?.Location}");
        }


    }
}
