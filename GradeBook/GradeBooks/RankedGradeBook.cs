using System;
using System.Collections.Generic;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks;

public class RankedGradeBook : BaseGradeBook
{
    public RankedGradeBook(string name) : base(name)
    {
        Type = GradeBookType.Ranked;
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