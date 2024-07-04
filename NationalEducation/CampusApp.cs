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
using NationalEducation.Operators;

namespace NationalEducation
{
    internal class CampusApp
    {

        private DataApp _appData;
        public StudentOperator StudentOperator { get; }
        public CourseOperator CourseOperator { get; }
        public PromotionOperator PromotionOperator { get; }

        public CampusApp(DataApp appData)
        {
            _appData = appData;
            StudentOperator = new StudentOperator(appData);
            CourseOperator = new CourseOperator(appData);
            PromotionOperator = new PromotionOperator(appData);
        }

        public void LaunchApp()
        {
            MenuApp.GeneralMenuLoop(this);
        }

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
                            List<Student> students = PromotionOperator.GetAllStudentsOfPromotions(promotion);
                            string average = PromotionOperator.GetAverageOfCourseOfPromotion(students, course);

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
                CourseOperator.ListAllCourses();
            }

            Console.WriteLine(ConstantValue.SEPARATION);
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
                    StudentOperator.ListAllStudents();
                    // Selection d'un étudiant
                    Student student = GenericOperator.Select<Student>(_appData.Students, ConstantValue.STUDENT_SELECT_DESCRIPTION_ADDGRADE);

                    // Affichage de la liste des cours
                    CourseOperator.ListAllCourses();
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
    }
}
