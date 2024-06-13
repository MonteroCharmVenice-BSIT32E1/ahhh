// Models/Student.cs
using System.ComponentModel.DataAnnotations;

namespace WebApplicationAHH.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Grade7Math { get; set; }
        public int Grade7Science { get; set; }
        public int Grade7English { get; set; }
        public int Grade8Math { get; set; }
        public int Grade8Science { get; set; }
        public int Grade8English { get; set; }
        public int Grade9Math { get; set; }
        public int Grade9Science { get; set; }
        public int Grade9English { get; set; }
        public int Grade10Math { get; set; }
        public int Grade10Science { get; set; }
        public int Grade10English { get; set; }

        public string DetermineStrand()
        {
            double averageMath = (Grade7Math + Grade8Math + Grade9Math + Grade10Math) / 4.0;
            double averageScience = (Grade7Science + Grade8Science + Grade9Science + Grade10Science) / 4.0;
            double averageEnglish = (Grade7English + Grade8English + Grade9English + Grade10English) / 4.0;

            if (averageMath >= 90 && averageScience >= 90)
            {
                return "STEM";
            }
            else if (averageEnglish >= 85)
            {
                return "HUMSS";
            }
            else
            {
                return "GA";
            }
        }
    }
}
