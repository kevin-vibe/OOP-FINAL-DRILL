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

// ==========================================
// PROGRAM CLASS
// Handles the main menu and user interaction.
// ==========================================
class Program
{
    static Phonebook phonebook = new Phonebook();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("             ASEAN PHONEBOOK");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Store Student");
            Console.WriteLine("2. Edit Student");
            Console.WriteLine("3. Search by Country");
            Console.WriteLine("4. Exit");
            Console.WriteLine("========================================");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    StoreStudent();
                    break;

                case "2":
                    EditStudent();
                    break;

                case "3":
                    SearchByCountry();
                    break;

                case "4":
                    Console.WriteLine("Thank you for using ASEAN Phonebook!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }


    // ======================================
    // STORE STUDENT
    // Collects information and adds a record.
    // ======================================
    static void StoreStudent()
    {
        Console.WriteLine("\n========== STORE STUDENT ==========");

        string studentNumber = ReadRequired("Student Number: ");

        if (phonebook.IsStudentNumberTaken(studentNumber))
        {
            Console.WriteLine("A student with that number already exists.");
            return;
        }

        string surname = ReadRequired("Surname: ");
        string firstName = ReadRequired("First Name: ");
        string occupation = ReadRequired("Occupation: ");
        string gender = ReadGender();

        Console.WriteLine("\nAvailable Countries:");

        foreach (Country country in Country.GetCountries())
        {
            Console.WriteLine(country.Name + " - " + country.Code);
        }

        string countryCode = ReadCountryCode();
        string areaCode = ReadRequired("Area Code: ");
        string phoneNumber = ReadRequired("Phone Number: ");

        Student newStudent = new Student(
            studentNumber,
            surname,
            firstName,
            occupation,
            gender,
            countryCode,
            areaCode,
            phoneNumber
        );

        if (phonebook.AddStudent(newStudent))
        {
            Console.WriteLine("\nStudent record successfully stored!");
        }
        else
        {
            Console.WriteLine("\nUnable to store student record.");
        }
    }


    // ======================================
    // EDIT STUDENT
    // Finds a student and updates a field.
    // ======================================
    static void EditStudent()
    {
        Console.WriteLine("\n========== EDIT STUDENT ==========");

        string studentNumber = ReadRequired("Enter Student Number: ");

        Student? student = phonebook.FindStudent(studentNumber);

        if (student == null)
        {
            Console.WriteLine("Student record not found.");
            return;
        }

        Console.WriteLine("\nStudent Found:");
        student.DisplayInformation();

        while (true)
        {
            Console.WriteLine("\n========== EDIT MENU ==========");
            Console.WriteLine("1. Student Number");
            Console.WriteLine("2. Surname");
            Console.WriteLine("3. Gender");
            Console.WriteLine("4. Occupation");
            Console.WriteLine("5. Country Code");
            Console.WriteLine("6. Area Code");
            Console.WriteLine("7. Phone Number");
            Console.WriteLine("8. Return");
            Console.WriteLine("===============================");
            Console.Write("Select field to edit: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    string newStudentNumber = ReadRequired("Enter New Student Number: ");

                    if (phonebook.IsStudentNumberTaken(newStudentNumber))
                    {
                        Console.WriteLine("That student number is already being used.");
                    }
                    else
                    {
                        student.StudentNumber = newStudentNumber;
                        Console.WriteLine("Student number updated successfully.");
                    }
                    break;

                case "2":
                    student.Surname = ReadRequired("Enter New Surname: ");
                    Console.WriteLine("Surname updated successfully.");
                    break;

                case "3":
                    student.Gender = ReadGender();
                    Console.WriteLine("Gender updated successfully.");
                    break;

                case "4":
                    student.Occupation = ReadRequired("Enter New Occupation: ");
                    Console.WriteLine("Occupation updated successfully.");
                    break;

                case "5":
                    Console.WriteLine("\nAvailable Countries:");

                    foreach (Country country in Country.GetCountries())
                    {
                        Console.WriteLine(country.Name + " - " + country.Code);
                    }

                    student.CountryCode = ReadCountryCode();
                    Console.WriteLine("Country code updated successfully.");
                    break;

                case "6":
                    student.AreaCode = ReadRequired("Enter New Area Code: ");
                    Console.WriteLine("Area code updated successfully.");
                    break;

                case "7":
                    student.PhoneNumber = ReadRequired("Enter New Phone Number: ");
                    Console.WriteLine("Phone number updated successfully.");
                    break;

                case "8":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nUpdated Student Information:");
            student.DisplayInformation();
        }
    }



}