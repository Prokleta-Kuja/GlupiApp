using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GlupiApp.Tests.PageObjectModels;
using GlupiApp.Tests.Enums;


namespace GlupiApp.Tests
{
    public class PersonRegistryMobileTests : IDisposable
    {

        public IWebDriver Driver { get; set; }
        public WebDriverWait Waiter { get; set; }

        public PersonRegistryMobileTests()
        {
            Driver = Infrastructure.GetInstance(Resolution.Mobile);
            Waiter = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            Waiter.PollingInterval = TimeSpan.FromMilliseconds(250);
        }


        [Fact]
        public void ValidDataIsProperlySubmitted()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            var person = new Person("Ime", "Prezime", "63925517388", "Female");
            personRegistry.OpenMobile();
            personRegistry.OpenAddForm();
            personRegistry.FillFirstName(person.firstName);
            personRegistry.FillLastName(person.lastName);
            personRegistry.FillOIB(person.OIB);
            personRegistry.SelectGender(person.gender);
            personRegistry.ClickSubmit();
            personRegistry.ValidPersonIsSavedToRegistryCheck(person);

        }

        [Fact]
        public void DoesNotSubmitWhenFirstNameEmpty()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            var person = new Person("", "Perić", "63925517388", "Male");

            personRegistry.OpenMobile();
            personRegistry.OpenAddForm();
            personRegistry.FillFirstName(person.firstName);
            personRegistry.FillLastName(person.lastName);
            personRegistry.FillOIB(person.OIB);
            personRegistry.SelectGender(person.gender);
            personRegistry.ClickSubmit();
            personRegistry.CheckForFirstNameEmptyAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }

        [Fact]
        public void DoesNotSubmitWhenLastNameEmpty()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            var person = new Person("Ante", "", "63925517388", "Male");

            personRegistry.OpenMobile();
            personRegistry.OpenAddForm();
            personRegistry.FillFirstName(person.firstName);
            personRegistry.FillLastName(person.lastName);
            personRegistry.FillOIB(person.OIB);
            personRegistry.SelectGender(person.gender);
            personRegistry.CheckForLastNameEmptyAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }

        [Fact]
        public void DoesNotSubmitWhenOIBInputEmpty()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            var person = new Person("Ante", "Perić", "", "Male");

            personRegistry.OpenMobile();
            personRegistry.OpenAddForm();
            personRegistry.FillFirstName(person.firstName);
            personRegistry.FillLastName(person.lastName);
            personRegistry.FillOIB(person.OIB);
            personRegistry.SelectGender(person.gender);
            personRegistry.CheckForOIBEmptyAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }


        [Fact]
        public void DoesNotSubmitWhenSelectedGenderInvalid()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            var person = new Person("Ante", "Perić", "63925517388", "Please select");

            personRegistry.OpenMobile();
            personRegistry.OpenAddForm();
            personRegistry.FillFirstName(person.firstName);
            personRegistry.FillLastName(person.lastName);
            personRegistry.FillOIB(person.OIB);
            personRegistry.SelectGender(person.gender);
            personRegistry.ClickSubmit();
            personRegistry.GenderNotSelectedAlertCheck();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);

        }

        [Fact]
        public void DoesNotSubmitWhenOIBTooShort()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            var person = new Person("Ante", "Perić", "56789", "Male");

            personRegistry.OpenMobile();
            personRegistry.OpenAddForm();
            personRegistry.FillFirstName(person.firstName);
            personRegistry.FillLastName(person.lastName);
            personRegistry.FillOIB(person.OIB);
            personRegistry.SelectGender(person.gender);
            personRegistry.ClickSubmit();
            personRegistry.CheckForOIBInvalidAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }

        [Fact]
        public void DoesNotSubmitWhenOIBTooLong()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            var person = new Person("Ante", "Perić", "567890000000000000000", "Male");

            personRegistry.OpenMobile();
            personRegistry.OpenAddForm();
            personRegistry.FillFirstName(person.firstName);
            personRegistry.FillLastName(person.lastName);
            personRegistry.FillOIB(person.OIB);
            personRegistry.SelectGender(person.gender);
            personRegistry.ClickSubmit();
            personRegistry.CheckForOIBInvalidAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);

        }

        [Fact]
        public void DoesNotSubmitWhenOIBInvalid()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            var person = new Person("Ante", "Perić", "12345678910", "Male");
            
            personRegistry.OpenMobile();
            personRegistry.OpenAddForm();
            personRegistry.FillFirstName(person.firstName);
            personRegistry.FillLastName(person.lastName);
            personRegistry.FillOIB(person.OIB);
            personRegistry.SelectGender(person.gender);
            personRegistry.ClickSubmit();
            personRegistry.CheckForOIBInvalidAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }


        [Fact]
        public void EditValidFirstNameAndSubmit()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            
            var person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditFirstName(person, "Banana");
            personRegistry.ClickSubmit();
            personRegistry.ValidPersonIsSavedToRegistryCheck(person);
        }

        [Fact]
        public void EditValidLastNameAndSubmit()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);

            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();

            var person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditLastName(person, "Ćavar");
            personRegistry.ClickSubmit();
            personRegistry.ValidPersonIsSavedToRegistryCheck(person);

        }

        [Fact]
        public void EditValidOIBAndSubmit()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            Person person = personRegistry.GetLastPerson();
            
            personRegistry.OpenEditForm();
            personRegistry.EditOIB(person, "03290525937");
            personRegistry.ClickSubmit();
            personRegistry.ValidPersonIsSavedToRegistryCheck(person);

        }


        [Fact]
        public void EditGenderAndSubmit()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            Person person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditGender(person, "Male");
            personRegistry.ClickSubmit();
            personRegistry.ValidPersonIsSavedToRegistryCheck(person);
        }


        [Fact]
        public void EditFirstNameInvalid()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            var person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditFirstName(person, "");
            personRegistry.ClickSubmit();
            personRegistry.CheckForFirstNameEmptyAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }

        [Fact]
        public void EditLastNameInvalid()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            var person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditLastName(person, "");
            personRegistry.ClickSubmit();
            personRegistry.CheckForLastNameEmptyAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }

        [Fact]
        public void EditOIBEmpty()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            var person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditOIB(person, "");
            personRegistry.ClickSubmit();
            personRegistry.CheckForOIBEmptyAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }


        [Fact]
        public void EditOIBInvalid()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            var person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditOIB(person, "12345678910");
            personRegistry.ClickSubmit();
            personRegistry.CheckForOIBInvalidAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);

        }

        [Fact]
        public void EditOIBTooShort()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            var person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditOIB(person, "12345");
            personRegistry.ClickSubmit();
            personRegistry.CheckForOIBInvalidAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }

        [Fact]
        public void EditOIBTooLong()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            var person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditOIB(person, "12345678910000000000000000");
            personRegistry.ClickSubmit();
            personRegistry.CheckForOIBInvalidAlert();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }



        [Fact]
        public void EditGenderInvalid()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            var person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditGender(person, "Please select");
            personRegistry.ClickSubmit();
            personRegistry.GenderNotSelectedAlertCheck();
            personRegistry.InvalidPersonIsNotSavedToRegistryCheck(person);
        }

        [Fact]
        public void AddDataAndCancel()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            var person = new Person("Ime", "Prezime", "63925517388", "Female");
            personRegistry.OpenMobile();
            
            personRegistry.OpenAddForm();
            personRegistry.FillFirstName(person.firstName);
            personRegistry.FillLastName(person.lastName);
            personRegistry.FillOIB(person.OIB);
            personRegistry.SelectGender(person.gender);
            personRegistry.ClickCancel();
            personRegistry.PersonIsNotSavedToRegistryCheck(person);
        }

        [Fact]
        public void EditDataAndCancel()
        {
            var personRegistry = new PersonRegistryPage(Driver, Waiter);
            personRegistry.OpenMobile();
            personRegistry.CheckIfDataBaseEmpty();
            var person = personRegistry.GetLastPerson();

            personRegistry.OpenEditForm();
            personRegistry.EditFirstName(person, "Novoime");
            personRegistry.EditLastName(person, "Novoprezime");
            personRegistry.EditOIB(person, "63925517388");
            personRegistry.EditGender(person, "Male");
            personRegistry.ClickCancel();
            personRegistry.PersonIsNotSavedToRegistryCheck(person);
        }

        public void Dispose()
        {
            Infrastructure.DisposeDriver(Driver);
        }
    }
    
}

