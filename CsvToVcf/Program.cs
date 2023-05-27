// See https://aka.ms/new-console-template for more information
using CsvToVcf;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("CSV to VCF Converter");
        Console.WriteLine("--------------------");

        // Ask the user to enter the path of the CSV file
        Console.Write("Enter the path of the CSV file: ");
        string? csvFilePath = Console.ReadLine();
        Console.Write("Enter the name of the output VCF file: ");
        string? vcfFileName = Console.ReadLine();
        if(csvFilePath != null && vcfFileName != null)
        {
            string vcfFilePath = Path.Combine(Path.GetDirectoryName(csvFilePath) ?? "", $"{vcfFileName}.vcf");

            //try
            {
                ConvertCsvToVcf(csvFilePath, vcfFilePath);

                Console.WriteLine("Conversion completed successfully.");
            }
            //catch (Exception ex)
            {
                //Console.WriteLine($"Error occurred during conversion: {ex.Message}");
            }
        }


        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();



        static void ConvertCsvToVcf(string csvFilePath, string vcfFilePath)
        {
            List<Contact> contacts = ReadContactsFromCsv(csvFilePath);
            WriteContactsToVcf(contacts, vcfFilePath);
        }


        static List<Contact> ReadContactsFromCsv(string csvFilePath)
        {
            List<Contact> contacts = new List<Contact>();

            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string? line;
                bool isFirstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        // Skip the header line
                        isFirstLine = false;
                        continue;
                    }

                    string[] values = line.Split(',');

                    // Create a new contact object and populate its properties from the CSV values
                    Contact contact = new Contact
                    {

                        FirstName = values[0],
                        MiddleName = values[1],
                        LastName = values[2],
                        Title = values[3],
                        Suffix = values[4],
                        Nickname = values[5],
                        GivenYomi = values[6],
                        SurnameYomi = values[7],
                        EmailAddress = values[8],
                        Email2Address = values[9],
                        Email3Address = values[10],
                        HomePhone = values[11],
                        HomePhone2 = values[12],
                        BusinessPhone = values[13],
                        BusinessPhone2 = values[14],
                        MobilePhone = values[15],
                        CarPhone = values[16],
                        OtherPhone = values[17],
                        PrimaryPhone = values[18],
                        Pager = values[19],
                        BusinessFax = values[20],
                        HomeFax = values[21],
                        OtherFax = values[22],
                        CompanyMainPhone = values[23],
                        Callback = values[24],
                        RadioPhone = values[25],
                        Telex = values[26],
                        TtyTddPhone = values[27],
                        IMAddress = values[28],
                        JobTitle = values[29],
                        Department = values[30],
                        Company = values[31],
                        OfficeLocation = values[32],
                        ManagerName = values[33],
                        AssistantName = values[34],
                        AssistantPhone = values[35],
                        CompanyYomi = values[36],
                        BusinessStreet = values[37],
                        BusinessCity = values[38],
                        BusinessState = values[39],
                        BusinessPostalCode = values[40],
                        BusinessCountryRegion = values[41],
                        HomeStreet = values[42],
                        HomeCity = values[43],
                        HomeState = values[44],
                        HomePostalCode = values[45],
                        HomeCountryRegion = values[46],
                        OtherStreet = values[47],
                        OtherCity = values[48],
                        OtherState = values[49],
                        OtherPostalCode = values[50],
                        OtherCountryRegion = values[51],
                        PersonalWebPage = values[52],
                        Spouse = values[53],
                        Schools = values[54],
                        Hobby = values[55],
                        Location = values[56],
                        WebPage = values[57],
                        Birthday = values[58],
                        Anniversary = values[59],
                        Notes = values[60]
                    };
                    contacts.Add(contact);
                }
            }
            return contacts;
        }
        static void WriteContactsToVcf(List<Contact> contacts, string vcfFilePath)
        {
            using (StreamWriter writer = new StreamWriter(vcfFilePath))
            {
                foreach (Contact contact in contacts)
                {
                    writer.WriteLine("BEGIN:VCARD");
                    writer.WriteLine("VERSION:3.0");
                    writer.WriteLine($"N:{EscapeVcfValue(contact.LastName)};{EscapeVcfValue(contact.FirstName??"")}");
                    if (!string.IsNullOrEmpty(contact.MiddleName))
                        writer.WriteLine($"NICKNAME:{EscapeVcfValue(contact.MiddleName)}");
                    if (!string.IsNullOrEmpty(contact.Title))
                        writer.WriteLine($"TITLE:{EscapeVcfValue(contact.Title)}");
                    if (!string.IsNullOrEmpty(contact.Suffix))
                        writer.WriteLine($"SUFFIX:{EscapeVcfValue(contact.Suffix)}");
                    if (!string.IsNullOrEmpty(contact.Nickname))
                        writer.WriteLine($"X-NICKNAME:{EscapeVcfValue(contact.Nickname)}");
                    if (!string.IsNullOrEmpty(contact.GivenYomi))
                        writer.WriteLine($"X-GIVEN-YOMI:{EscapeVcfValue(contact.GivenYomi)}");
                    if (!string.IsNullOrEmpty(contact.SurnameYomi))
                        writer.WriteLine($"X-SURNAME-YOMI:{EscapeVcfValue(contact.SurnameYomi)}");
                    if (!string.IsNullOrEmpty(contact.EmailAddress))
                        writer.WriteLine($"EMAIL;TYPE=INTERNET:{EscapeVcfValue(contact.EmailAddress)}");
                    if (!string.IsNullOrEmpty(contact.Email2Address))
                        writer.WriteLine($"EMAIL;TYPE=INTERNET:{EscapeVcfValue(contact.Email2Address)}");
                    if (!string.IsNullOrEmpty(contact.Email3Address))
                        writer.WriteLine($"EMAIL;TYPE=INTERNET:{EscapeVcfValue(contact.Email3Address)}");
                    if (!string.IsNullOrEmpty(contact.HomePhone))
                        writer.WriteLine($"TEL;TYPE=HOME:{EscapeVcfValue(contact.HomePhone)}");
                    if (!string.IsNullOrEmpty(contact.HomePhone2))
                        writer.WriteLine($"TEL;TYPE=HOME:{EscapeVcfValue(contact.HomePhone2)}");
                    if (!string.IsNullOrEmpty(contact.BusinessPhone))
                        writer.WriteLine($"TEL;TYPE=WORK:{EscapeVcfValue(contact.BusinessPhone)}");
                    if (!string.IsNullOrEmpty(contact.BusinessPhone2))
                        writer.WriteLine($"TEL;TYPE=WORK:{EscapeVcfValue(contact.BusinessPhone2)}");
                    if (!string.IsNullOrEmpty(contact.MobilePhone))
                        writer.WriteLine($"TEL;TYPE=CELL:{EscapeVcfValue(contact.MobilePhone)}");
                    if (!string.IsNullOrEmpty(contact.CarPhone))
                        writer.WriteLine($"TEL;TYPE=CAR:{EscapeVcfValue(contact.CarPhone)}");
                    if (!string.IsNullOrEmpty(contact.OtherPhone))
                        writer.WriteLine($"TEL;TYPE=OTHER:{EscapeVcfValue(contact.OtherPhone)}");
                    if (!string.IsNullOrEmpty(contact.PrimaryPhone))
                        writer.WriteLine($"TEL;TYPE=PREF:{EscapeVcfValue(contact.PrimaryPhone)}");
                    if (!string.IsNullOrEmpty(contact.Pager))
                        writer.WriteLine($"TEL;TYPE=PAGER:{EscapeVcfValue(contact.Pager)}");
                    if (!string.IsNullOrEmpty(contact.BusinessFax))
                        writer.WriteLine($"TEL;TYPE=FAX,WORK:{EscapeVcfValue(contact.BusinessFax)}");
                    if (!string.IsNullOrEmpty(contact.HomeFax))
                        writer.WriteLine($"TEL;TYPE=FAX,HOME:{EscapeVcfValue(contact.HomeFax)}");
                    if (!string.IsNullOrEmpty(contact.OtherFax))
                        writer.WriteLine($"TEL;TYPE=FAX,OTHER:{EscapeVcfValue(contact.OtherFax)}");
                    if (!string.IsNullOrEmpty(contact.CompanyMainPhone))
                        writer.WriteLine($"TEL;TYPE=MAIN:{EscapeVcfValue(contact.CompanyMainPhone)}");
                    if (!string.IsNullOrEmpty(contact.Callback))
                        writer.WriteLine($"TEL;TYPE=CALLBACK:{EscapeVcfValue(contact.Callback)}");
                    if (!string.IsNullOrEmpty(contact.RadioPhone))
                        writer.WriteLine($"TEL;TYPE=RADIO:{EscapeVcfValue(contact.RadioPhone)}");
                    if (!string.IsNullOrEmpty(contact.Telex))
                        writer.WriteLine($"TEL;TYPE=TELEX:{EscapeVcfValue(contact.Telex)}");
                    if (!string.IsNullOrEmpty(contact.TtyTddPhone))
                        writer.WriteLine($"TEL;TYPE=TTY:{EscapeVcfValue(contact.TtyTddPhone)}");
                    if (!string.IsNullOrEmpty(contact.IMAddress))
                        writer.WriteLine($"X-IMADDRESS:{EscapeVcfValue(contact.IMAddress)}");
                    if (!string.IsNullOrEmpty(contact.JobTitle))
                        writer.WriteLine($"TITLE:{EscapeVcfValue(contact.JobTitle)}");
                    if (!string.IsNullOrEmpty(contact.Department))
                        writer.WriteLine($"X-DEPARTMENT:{EscapeVcfValue(contact.Department)}");
                    if (!string.IsNullOrEmpty(contact.Company))
                        writer.WriteLine($"ORG:{EscapeVcfValue(contact.Company)}");
                    if (!string.IsNullOrEmpty(contact.OfficeLocation))
                        writer.WriteLine($"X-OFFICE-LOCATION:{EscapeVcfValue(contact.OfficeLocation)}");
                    if (!string.IsNullOrEmpty(contact.ManagerName))
                        writer.WriteLine($"MANAGER:{EscapeVcfValue(contact.ManagerName)}");
                    if (!string.IsNullOrEmpty(contact.AssistantName))
                        writer.WriteLine($"X-ASSISTANT:{EscapeVcfValue(contact.AssistantName)}");
                    if (!string.IsNullOrEmpty(contact.AssistantPhone))
                        writer.WriteLine($"X-ASSISTANTPHONE:{EscapeVcfValue(contact.AssistantPhone)}");
                    if (!string.IsNullOrEmpty(contact.CompanyYomi))
                        writer.WriteLine($"X-COMPANY-YOMI:{EscapeVcfValue(contact.CompanyYomi)}");
                    if (!string.IsNullOrEmpty(contact.BusinessStreet))
                        writer.WriteLine($"ADR;TYPE=WORK:{EscapeVcfValue(contact.BusinessStreet)}");
                    if (!string.IsNullOrEmpty(contact.BusinessCity))
                        writer.WriteLine($"ADR;TYPE=WORK:{EscapeVcfValue(contact.BusinessCity)}");
                    if (!string.IsNullOrEmpty(contact.BusinessState))
                        writer.WriteLine($"ADR;TYPE=WORK:{EscapeVcfValue(contact.BusinessState)}");
                    if (!string.IsNullOrEmpty(contact.BusinessPostalCode))
                        writer.WriteLine($"ADR;TYPE=WORK:{EscapeVcfValue(contact.BusinessPostalCode)}");
                    if (!string.IsNullOrEmpty(contact.BusinessCountryRegion))
                        writer.WriteLine($"ADR;TYPE=WORK:{EscapeVcfValue(contact.BusinessCountryRegion)}");
                    if (!string.IsNullOrEmpty(contact.HomeStreet))
                        writer.WriteLine($"ADR;TYPE=HOME:{EscapeVcfValue(contact.HomeStreet)}");
                    if (!string.IsNullOrEmpty(contact.HomeCity))
                        writer.WriteLine($"ADR;TYPE=HOME:{EscapeVcfValue(contact.HomeCity)}");
                    if (!string.IsNullOrEmpty(contact.HomeState))
                        writer.WriteLine($"ADR;TYPE=HOME:{EscapeVcfValue(contact.HomeState)}");
                    if (!string.IsNullOrEmpty(contact.HomePostalCode))
                        writer.WriteLine($"ADR;TYPE=HOME:{EscapeVcfValue(contact.HomePostalCode)}");
                    if (!string.IsNullOrEmpty(contact.HomeCountryRegion))
                        writer.WriteLine($"ADR;TYPE=HOME:{EscapeVcfValue(contact.HomeCountryRegion)}");
                    if (!string.IsNullOrEmpty(contact.OtherStreet))
                        writer.WriteLine($"ADR;TYPE=OTHER:{EscapeVcfValue(contact.OtherStreet)}");
                    if (!string.IsNullOrEmpty(contact.OtherCity))
                        writer.WriteLine($"ADR;TYPE=OTHER:{EscapeVcfValue(contact.OtherCity)}");
                    if (!string.IsNullOrEmpty(contact.OtherState))
                        writer.WriteLine($"ADR;TYPE=OTHER:{EscapeVcfValue(contact.OtherState)}");
                    if (!string.IsNullOrEmpty(contact.OtherPostalCode))
                        writer.WriteLine($"ADR;TYPE=OTHER:{EscapeVcfValue(contact.OtherPostalCode)}");
                    if (!string.IsNullOrEmpty(contact.OtherCountryRegion))
                        writer.WriteLine($"ADR;TYPE=OTHER:{EscapeVcfValue(contact.OtherCountryRegion)}");
                    if (!string.IsNullOrEmpty(contact.PersonalWebPage))
                        writer.WriteLine($"URL:{EscapeVcfValue(contact.PersonalWebPage)}");
                    if (!string.IsNullOrEmpty(contact.Spouse))
                        writer.WriteLine($"X-SPOUSE:{EscapeVcfValue(contact.Spouse)}");
                    if (!string.IsNullOrEmpty(contact.Schools))
                        writer.WriteLine($"X-SCHOOLS:{EscapeVcfValue(contact.Schools)}");
                    if (!string.IsNullOrEmpty(contact.Hobby))
                        writer.WriteLine($"X-HOBBY:{EscapeVcfValue(contact.Hobby)}");
                    if (!string.IsNullOrEmpty(contact.Location))
                        writer.WriteLine($"X-LOCATION:{EscapeVcfValue(contact.Location)}");
                    if (!string.IsNullOrEmpty(contact.WebPage))
                        writer.WriteLine($"URL:{EscapeVcfValue(contact.WebPage)}");
                    if (!string.IsNullOrEmpty(contact.Birthday))
                        writer.WriteLine($"BDAY:{EscapeVcfValue(contact.Birthday)}");
                    if (!string.IsNullOrEmpty(contact.Anniversary))
                        writer.WriteLine($"ANNIVERSARY:{EscapeVcfValue(contact.Anniversary)}");
                    if (!string.IsNullOrEmpty(contact.Notes))
                        writer.WriteLine($"NOTE:{EscapeVcfValue(contact.Notes)}");
                    writer.WriteLine("END:VCARD");
                }
            }
        }

        static string EscapeVcfValue(string value)
        {
            // Replace special characters in the VCF value
            return value.Replace(",", "\\,");
        }
    }
}