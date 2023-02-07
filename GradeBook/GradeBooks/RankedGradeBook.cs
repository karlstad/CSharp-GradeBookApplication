using System;
using System.Collections.Generic;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks;

public class RankedGradeBook : BaseGradeBook
{
    public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
    {
        Type = GradeBookType.Ranked;
    }

    public override void CalculateStatistics()
    {
        if (Students.Count < 5)
        {
            Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            return;
        }
        base.CalculateStatistics();
    }

    public override void CalculateStudentStatistics(string name)
    {
        if (Students.Count < 5)
        {
            Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            return;
        }
        base.CalculateStudentStatistics(name);
    }

    public override char GetLetterGrade(double averageGrade)
    {
        if (Students.Count < 5)
        {
            throw new InvalidOperationException();
        }

        var numberOfStudentsToDropGrade = Students.Count * 0.2;
        var studentsSortedByGrades = Students.OrderByDescending(student => student.AverageGrade).ToList();
        var gradeIndex = studentsSortedByGrades.FindIndex(student => student.AverageGrade.Equals(averageGrade));
        return (gradeIndex / numberOfStudentsToDropGrade) switch
        {
            0 => 'A',
            1 => 'B',
            2 => 'C',
            3 => 'D',
            _ => 'F'
        };
    }
}