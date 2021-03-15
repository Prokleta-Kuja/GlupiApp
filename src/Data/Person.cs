using System;
using System.Collections.Generic;
using System.Linq;
using GlupiApp.Pages;

namespace GlupiApp.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OIB { get; set; }
        public int Gender { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public Dictionary<string, string> Validate()
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(FirstName))
                errors.Add(nameof(FirstName), "Required");

            if (string.IsNullOrWhiteSpace(LastName))
                errors.Add(nameof(LastName), "Required");

            if (!IsOibValid())
                errors.Add(nameof(OIB), "Invalid");

            if (!PersonRegistry.Genders.ContainsKey(Gender) || Gender == 0)
                errors.Add(nameof(Gender), "You must select a valid option");

            if (errors.Any())
                return errors;

            return null;
        }
        internal bool IsOibValid()
        {
            if (OIB == null || OIB.Length != 11)
                return false;

            long b;
            if (!long.TryParse(OIB, out b))
                return false;

            int a = 10;
            for (int i = 0; i < 10; i++)
            {
                a = a + Convert.ToInt32(OIB.Substring(i, 1));
                a = a % 10;
                if (a == 0) a = 10;
                a *= 2;
                a = a % 11;
            }
            int controlNum = 11 - a;
            if (controlNum == 10) controlNum = 0;

            return controlNum == Convert.ToInt32(OIB.Substring(10, 1));
        }
    }
}