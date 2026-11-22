using System;
using System.Collections.Generic;
using System.Linq;

// 
// 1. STUDENT CLASS
// 

public class Student
{
    // Student information
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public string Gender { get; set; }
    public string CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }

    
    public Student(string studentNumber, string surname,
        string firstName, string occupation, string gender,
        string countryCode, string areaCode, string phoneNumber)
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

    // Get complete phone number
    public string GetCompletePhoneNumber()
    {
        return CountryCode + "-" + AreaCode + "-" + PhoneNumber;
    }

    // Display student information
    public void DisplayInformation()
    {
        string genderDescription = Gender == "M" ? "male" : "female";

        Console.WriteLine(
            $"{Surname}, {FirstName}, with student number {StudentNumber}, " +
            $"is a {Occupation} ({genderDescription}). " +
            $"His/Her phone number is {GetCompletePhoneNumber()}");
    }
}


// 
// 2. COUNTRY CLASS
// 

public class Country
{
    public string Name { get; set; }
    public string Code { get; set; }

    // Constructor
    public Country(string name, string code)
    {
        Name = name;
        Code = code;
    }

    // Get five supported ASEAN countries
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

    // Get country name using country code
    public static string GetCountryName(string code)
    {
        foreach (Country country in GetCountries())
        {
            if (country.Code == code)
            {
                return country.Name;
            }
        }

        return "Unknown Country";
    }
}