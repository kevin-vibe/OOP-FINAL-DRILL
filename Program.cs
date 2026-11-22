using System;
using System.Collections.Generic;
using System.Linq;

// ==========================================
// STUDENT CLASS
// Handles the information of each student.
// ==========================================
class Student
{
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public string Gender { get; set; }
    public string CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }

    public Student(
        string studentNumber,
        string surname,
        string firstName,
        string occupation,
        string gender,
        string countryCode,
        string areaCode,
        string phoneNumber)
    {
        StudentNumber = studentNumber;
        Surname = surname;
        FirstName = firstName;
        Occupation = occupation;
        Gender = gender;
        CountryCode = countryCode;
        AreaCode = areaCode;
        PhoneNumber = phoneNumber;
    }

    public string GetCompletePhoneNumber()
    {
        return "+" + CountryCode + " " + AreaCode + " " + PhoneNumber;
    }

    public void DisplayInformation()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Student Number : " + StudentNumber);
        Console.WriteLine("Name           : " + FirstName + " " + Surname);
        Console.WriteLine("Occupation     : " + Occupation);
        Console.WriteLine("Gender         : " + Gender);
        Console.WriteLine("Country        : " + Country.GetCountryName(CountryCode));
        Console.WriteLine("Phone Number   : " + GetCompletePhoneNumber());
    }
}


// ==========================================
// COUNTRY CLASS
// Handles the supported ASEAN countries.
// ==========================================
class Country
{
    public string Name { get; set; }
    public string Code { get; set; }

    public Country(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public static List<Country> GetCountries()
    {
        return new List<Country>
        {
            new Country("Malaysia", "60"),
            new Country("Indonesia", "62"),
            new Country("Philippines", "63"),
            new Country("Singapore", "65"),
            new Country("Thailand", "66")
        };
    }

    public static string GetCountryName(string code)
    {
        Country? country = GetCountries()
            .FirstOrDefault(c => c.Code == code);

        return country == null ? "Unknown Country" : country.Name;
    }

    public static bool IsValidCode(string code)
    {
        return GetCountries().Any(c => c.Code == code);
    }
}


// ==========================================
// PHONEBOOK CLASS
// Manages the collection of student records.
// ==========================================
class Phonebook
{
    private List<Student> students = new List<Student>();

    // Add a new student.
    public bool AddStudent(Student student)
    {
        if (FindStudent(student.StudentNumber) != null)
        {
            return false;
        }

        students.Add(student);
        return true;
    }

    // Find a student using their student number.
    public Student? FindStudent(string studentNumber)
    {
        return students.FirstOrDefault(
            s => s.StudentNumber.Equals(
                studentNumber,
                StringComparison.OrdinalIgnoreCase));
    }

    // Check whether a student number is already used.
    public bool IsStudentNumberTaken(string studentNumber)
    {
        return FindStudent(studentNumber) != null;
    }

    // Retrieve all students alphabetically by surname.
    public List<Student> GetAllStudents()
    {
        return students
            .OrderBy(s => s.Surname)
            .ThenBy(s => s.FirstName)
            .ToList();
    }

    // Retrieve students from selected countries.
    public List<Student> GetStudentsByCountryCodes(List<string> countryCodes)
    {
        return students
            .Where(s => countryCodes.Contains(s.CountryCode))
            .OrderBy(s => s.Surname)
            .ThenBy(s => s.FirstName)
            .ToList();
    }
}

