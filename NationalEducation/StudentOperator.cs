using NationalEducation.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalEducation
{
    internal class StudentOperator
    {
        private AppData _appData;

        public StudentOperator(AppData appData)
        {
            _appData = appData;
        }

        // Afficher la liste de tout les étudiants
        public void ListAllStudents() => GenericOperator.ListAll<Student>(_appData.Students, ConstantValue.STUDENTS_LIST_DESCRIPTION, ConstantValue.NO_STUDENTS_LIST_DESCRIPTION);

        // Créer un nouvel étudiant
        public void AddStudent()
        {
            string lastName;
            string firstName;
            DateTime dateOfBirth;
            string promotion;

            Console.WriteLine("Création d'un nouvel étudiant\n");
            // Saisie de l'utilisateur
            lastName = InputValidator.GetAndValidNameInput("Entrez un nom : ");
            firstName = InputValidator.GetAndValidNameInput("Entrez un prénom : ");
            dateOfBirth = InputValidator.GetAndValidDateInput("Entrez une date de naissance (format : jj/mm/aaaa) : ");
            promotion = InputValidator.GetAndValidNameInput("Entrez un nom de promotion : ");
            // Ajout d'un nouvel étudiant dans la liste d'étudiants
            _appData.Students.Add(new Student(GenericOperator.GenerateId<Student>(_appData.Students), lastName, firstName, dateOfBirth, promotion));

            Log.Information($"Ajout de l'étudiant {lastName} {firstName}");

            FileOperator.SaveData(_appData);

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Consulter un élève existant
        public void DisplayStudent()
        {
            if (_appData.Students.Count > 0)
            {
                // Affichage de la liste des étudiants
                ListAllStudents();
                // Selection d'un étudiant
                Student student = GenericOperator.Select<Student>(_appData.Students, ConstantValue.STUDENT_SELECT_DESCRIPTION_DISPLAYSTUDENT);

                List<Grade> gradesOfStudent = student.GetGradesOfStudent(_appData.Grades);

                Console.WriteLine("Informations sur l'étudiant :\n");
                Console.WriteLine(ConstantValue.INDENT_VALUE, "Nom", student.LastName);
                Console.WriteLine(ConstantValue.INDENT_VALUE, "Prénom", student.FirstName);
                Console.WriteLine(ConstantValue.INDENT_VALUE, "Date de naissance", student.DateOfBirth.ToString("dd/MM/yyyy"));
                Console.WriteLine("\nRésultats scolaires :");

                if (gradesOfStudent.Count > 0)
                {
                    foreach (Grade grade in gradesOfStudent)
                    {
                        Console.WriteLine("\n{0}{1, -7} : {2}", ConstantValue.TABULATION, "Cours", GetCourseNameWithId(grade.CourseId));
                        Console.WriteLine("{0, -13} : {1}", "\tNote", grade.Value);
                        Console.WriteLine("{0} : {1}", "\tAppréciation", grade.Observation);
                    }

                    float gradesOfStudentAverage = Student.GetGradesOfStudentAverage(gradesOfStudent);
                    Console.WriteLine("\n{0}{1} : {2}", ConstantValue.TABULATION, "Moyenne", Math.Round(gradesOfStudentAverage, 1));
                }
                else
                {
                    Console.WriteLine("\nAucunes notes à afficher pour le moment.");
                }

                Log.Information($"Consultation des détails de l'étudiant {student.Name}");

                Console.WriteLine(ConstantValue.SEPARATION);
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_STUDENTS_LIST_DESCRIPTION);
            }
        }

        // Retrouver le nom d'un cours à partir de son identifiant uniquement
        // Revoir la fonction pour qu'elle renvoie carrement l'objet
        public string GetCourseNameWithId(uint id)
        {
            foreach (Course course in _appData.Courses)
            {
                if (course.Id == id)
                {
                    return course.Name;
                }
            }
            return string.Empty;
        }
    }
}
