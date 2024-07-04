using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using Serilog;
using NationalEducation.Models;
using NationalEducation.Interfaces;
using System.Reflection;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NationalEducation
{
    internal class CampusApp
    {

        private AppData _appData;

        public CampusApp(AppData appData)
        {
            _appData = appData;
        }
        public void ListAllStudents() => GenericOperator.ListAll<Student>(_appData.Students, ConstantValue.STUDENTS_LIST_DESCRIPTION, ConstantValue.NO_STUDENTS_LIST_DESCRIPTION);
        public void ListAllCourses() => GenericOperator.ListAll<Course>(_appData.Courses, ConstantValue.COURSES_LIST_DESCRIPTION, ConstantValue.NO_COURSES_LIST_DESCRIPTION);

        public void ListAllCourses(List<string> promotions)
        {
            if(promotions.Count > 0)
            {
                if (_appData.Courses.Count > 0)
                {
                    Console.WriteLine($"{ConstantValue.COURSES_LIST_DESCRIPTION}\n");

                    int index = 0;
                    foreach (Course course in _appData.Courses)
                    {
                        string temp = "";

                        foreach (string promotion in promotions)
                        {
                            List<Student> students = GetAllStudentsOfPromotions(promotion);
                            string average = GetAverageOfCourseOfPromotion(students, course);

                            temp += $" - {promotion} : {average}";
                        }
                        Console.WriteLine($"{index} - {course.Name}{temp}");
                        index++;
                    }

                    Log.Information($"Consultation de {ConstantValue.COURSES_LIST_DESCRIPTION}");
                }
                else
                {
                    Console.WriteLine(ConstantValue.NO_COURSES_LIST_DESCRIPTION);

                    Log.Information($"Échec de la consultation. {ConstantValue.NO_COURSES_LIST_DESCRIPTION}");
                }
            }
            else
            {
                ListAllCourses();
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

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
            if(_appData.Students.Count > 0)
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

                if(gradesOfStudent.Count > 0)
                {
                    foreach (Grade grade in gradesOfStudent)
                    {
                        Console.WriteLine("\n{0}{1, -7} : {2}", ConstantValue.TABULATION, "Cours",  GetCourseNameWithId(grade.CourseId));
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

        // Ajouter une note et une appréciation pour un cours sur un étudiant existant
        public void AddGradeToStudent()
        {
            float gradeValue;
            string observation;

            if (_appData.Courses.Count > 0)
            {
                Console.WriteLine("Ajout de note à un étudiant\n");

                if (_appData.Students.Count > 0)
                {
                    // Affichage de la liste des étudiants
                    ListAllStudents();
                    // Selection d'un étudiant
                    Student student = GenericOperator.Select<Student>(_appData.Students, ConstantValue.STUDENT_SELECT_DESCRIPTION_ADDGRADE);

                    // Affichage de la liste des cours
                    ListAllCourses();
                    // Selection d'un cours
                    Course course = GenericOperator.Select<Course>(_appData.Courses, ConstantValue.COURSE_SELECT_DESCRIPTION_ADDGRADE);

                    // Saisie de l'utilisateur
                    gradeValue = InputValidator.GetAndValidGradeInput("Entrez la note : ");
                    observation = InputValidator.GetAndValidObservationInput("Entrez une appréciation : ");

                    Console.Write($"Confirme la saisie d'une note pour l'étudiant {student.Name} : {course.Name} {gradeValue} {observation}. Confirmer O pour Oui et N pour Non : ");
                    string reponse = Console.ReadLine();

                    if (reponse.Equals("O"))
                    {
                        // Ajout d'une nouvelle note dans la list de notes
                        _appData.Grades.Add(new Grade(GenericOperator.GenerateId<Grade>(_appData.Grades), course.Id, student.Id, gradeValue, observation));

                        Console.WriteLine("Ajout de note confirmée");

                        Log.Information($"Saisie d'une note pour l'étudiant {student.Name} : {course.Name} {gradeValue} {observation}");

                        FileOperator.SaveData(_appData);
                    }
                    else
                    {
                        Console.WriteLine("Saisie annulée");
                        Log.Information($"Annulation de la saisie d'une note pour l'étudiant {student.Name} : {course.Name} {gradeValue} {observation}");
                    }
                }
                else
                {
                    Console.WriteLine(ConstantValue.NO_STUDENTS_LIST_DESCRIPTION);
                    Log.Information($"Échec de la saisie d'une note. {ConstantValue.NO_STUDENTS_LIST_DESCRIPTION}");
                }
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_COURSES_LIST_DESCRIPTION);
                Log.Information($"Échec de la saisie d'une note. {ConstantValue.NO_COURSES_LIST_DESCRIPTION}");
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Ajouter un nouveau cours au programme
        public void AddCourse()
        {
            string name;

            Console.WriteLine("Création d'un nouveau cours\n");
            // Saisie de l'utilisateur
            name = InputValidator.GetAndValidNameInput("Entrez un nom pour le cour : ");
            // Ajout d'un nouveau cours dans la list de cours
            _appData.Courses.Add(new Course(GenericOperator.GenerateId<Course>(_appData.Courses), name));

            Log.Information($"Ajout du cours {name}");

            FileOperator.SaveData(_appData);

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Supprimer un cours par son identifiant
        // Fonctions DeleteCourseById à créer
        public void DeleteCourse()
        {
            string reponse = "";

            if(_appData.Courses.Count > 0)
            {
                // Affichage de la liste des cours
                ListAllCourses();
                // Selection d'un cours
                Course course = GenericOperator.Select<Course>(_appData.Courses, ConstantValue.COURSE_SELECT_DESCRIPTION_DELETECOURSE);

                Console.Write($"Vous allez supprimer le cours de {course.Name}. Confirmer O pour Oui et N pour Non : ");
                reponse = Console.ReadLine();

                if(reponse.Equals("O"))
                {
                    _appData.Grades.RemoveAll(x => x.CourseId == course.Id);
                    _appData.Courses.Remove(course);
                    Console.WriteLine("Suppression réussie");

                    Log.Information($"Suppression du cours de {course.Name}");

                    FileOperator.SaveData(_appData);
                }
                else
                {
                    Console.WriteLine("Suppression annulée");
                    Log.Information($"Annulation de la suppression du cours de {course.Name}");
                }
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_COURSES_LIST_DESCRIPTION);
                Log.Information($"Échec de la suppression de cours. {ConstantValue.NO_COURSES_LIST_DESCRIPTION}");
            }

            Console.WriteLine(ConstantValue.SEPARATION);
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

        // Promotion
        // Récupérer la liste des promotions
        public List<string> GetPromotions()
        {
            List<string> promotions = new List<string>();

            foreach (Student student in _appData.Students)
            {
                if (!promotions.Contains(student.Promotion))
                {
                    promotions.Add(student.Promotion);
                }
            }

            return promotions;
        }

        // Afficher la liste des promotions
        public void ListAllPromotions(List<string> promotions)
        {
            if (promotions.Count > 0)
            {
                int index = 0;

                Console.WriteLine($"{ConstantValue.PROMOTIONS_LIST_DESCRIPTION}\n");

                foreach (string promotion in promotions)
                {
                    Console.WriteLine($"{index} - {promotion}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine($"{ConstantValue.NO_PROMOTIONS_LIST_DESCRIPTION}\n");
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Récupérer la liste des étudiants d'une promotions
        public List<Student> GetAllStudentsOfPromotions(string promotion)
        {
            return _appData.Students.FindAll(x => x.Promotion == promotion);
        }

        // Afficher la liste des étudiants d'une promotions
        public void ListAllStudentsOfPromotion()
        {
            ListAllPromotions(GetPromotions());

            String promotion = GenericOperator.Select<string>(GetPromotions(), ConstantValue.PROMOTION_SELECT_DESCRIPTION);

            List<Student> students = GetAllStudentsOfPromotions(promotion);

            GenericOperator.ListAll<Student>(students, ConstantValue.STUDENTS_LIST_DESCRIPTION, ConstantValue.NO_STUDENTS_LIST_DESCRIPTION);

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Afficher la liste des moyennes par cours d'une promotion donnée
        public void ListAllAverageOfCoursesOfPromotion()
        {
            ListAllPromotions(GetPromotions());

            String promotion = GenericOperator.Select<string>(GetPromotions(), ConstantValue.PROMOTION_SELECT_DESCRIPTION);

            List<Student> students = _appData.Students.FindAll(x => x.Promotion == promotion);

            foreach (Course course in _appData.Courses)
            {
                Console.WriteLine($"{course.Name} - {GetAverageOfCourseOfPromotion(students, course)}");
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Récupérer la moyenne d'un cours pour une promotion donnée
        public string GetAverageOfCourseOfPromotion(List<Student> students, Course course)
        {
            float sum = 0;
            int counter = 0;
            double moyenne = 0;

            foreach (Student student in students)
            {
                foreach (Grade grade in student.GetGradesOfStudent(_appData.Grades))
                {
                    if (grade.CourseId == course.Id)
                    {
                        sum += grade.Value;
                        counter++;
                    }
                }
            }

            if (counter != 0)
            {
                moyenne = Math.Round((sum / counter), 1);

                return $"{moyenne}";
            }
            else
            {
                return $"Pas de moyenne pour cette promotion le moment";
            }
        }
    }
}
