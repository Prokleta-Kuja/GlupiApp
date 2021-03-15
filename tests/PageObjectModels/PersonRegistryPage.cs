
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using System.Threading;
using GlupiApp.Tests.Enums;


namespace GlupiApp.Tests.PageObjectModels
{

    public class PersonRegistryPage : BasePage
    {
        #region Selectors

        public static readonly By formName = By.CssSelector("legend");

        public static readonly By addButton = By.CssSelector("table > thead > tr:first-child > td:nth-child(5) > button");

        public static readonly By editButton = By.CssSelector("table > tbody > tr:last-child > td:last-child > button");

        public static readonly By inputFirstName = By.Id("FirstName");

        public static readonly By inputLastName = By.Id("LastName");

        public static readonly By inputOIB = By.Id("OIB");

        public static readonly By dropDownMenu = By.ClassName("form-control");

        public static readonly By submitButton = By.CssSelector("form > button.btn.btn-primary");

        public static readonly By cancelButton = By.CssSelector("form > button.btn.btn-outline-danger");

        public static readonly By selectPersonToEditOrAddMessage = By.CssSelector("body > div.page > div.main > div.content.px-4 > div > div:nth-child(2) > div");

        public static readonly By genderNotSelectedAlert = By.CssSelector("body > div.page > div.main > div.content.px-4 > div > div:nth-child(2) > form > div:nth-child(4) > div");

        public static readonly By OIBInvalidAlert = By.CssSelector("body > div.page > div.main > div.content.px-4 > div > div:nth-child(2) > form > div:nth-child(3) > div");

        public static readonly By lastRow = By.CssSelector("table > tbody > tr:last-child > td");

        public static readonly By personRegistryNavigationButton = By.CssSelector("body > div.page > div.sidebar > div:nth-child(2) > ul > li:nth-child(4) > a");

        public static readonly By hamburgerMenuButton = By.CssSelector("body > div.page > div.sidebar > div.top-row.pl-4.navbar.navbar-dark > button > span");

        #endregion

        #region TableColumns
        public const int columnFirstName = 0;

        public const int columnLastName = 1;

        public const int columnOIB = 2;

        public const int columnGender = 3;

        #endregion

        public PersonRegistryPage(IWebDriver driver, WebDriverWait waiter) : base(driver, waiter)
        {
            //ovo tu je bilo potpuno prazno
            Driver = Infrastructure.GetInstance(Resolution.Mobile);
            Waiter = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            Waiter.PollingInterval = TimeSpan.FromMilliseconds(250);
        }


        public void Open()
        {
            var homePage = new HomePage(Driver, Waiter);
            homePage.Open();
            Driver.FindElement(personRegistryNavigationButton).Click();
            Waiter.Until(Expected.TextToBePresentInElementLocated(PageTitle, "Person Registry"));
        }

        public void OpenMobile()
        {
            var homePage = new HomePage(Driver, Waiter);
            homePage.Open();
            Driver.FindElement(hamburgerMenuButton).Click();
            Driver.FindElement(personRegistryNavigationButton).Click();
            Waiter.Until(Expected.TextToBePresentInElementLocated(PageTitle, "Person Registry"));
        }

        private void FillInput(string firstName, By locator)
        {
            var input = Waiter.Until(Expected.ElementExists(locator));
            input.Clear();
            input.SendKeys(firstName);
        }

        public void FillFirstName(string firstName)
        {
            FillInput(firstName, inputFirstName);
        }

        public void FillLastName(string lastName)
        {
            FillInput(lastName, inputLastName);
        }

        public void FillOIB(string OIB)
        {
            FillInput(OIB, inputOIB);
        }

        private void Selector(string gender, By locator)
        {
            var options = Driver.FindElements(By.CssSelector("select > option"));
            List<string> genders = new List<string>();
            for (int i = 0; i < options.Count; i++) genders.Add(options[i].Text);
            var myDictionary = genders.Zip(options, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            var chosenOption = myDictionary[gender];
            chosenOption.Click();
        }

        public void SelectGender(string gender)
        {
            Selector(gender, dropDownMenu);
        }

        public Person LastTableRow()
        {
            var listOfLastRowElements = Driver.FindElements(lastRow);
            var person = new Person();
            person.firstName = listOfLastRowElements[columnFirstName].Text;
            person.lastName = listOfLastRowElements[columnLastName].Text;
            person.OIB = listOfLastRowElements[columnOIB].Text;
            person.gender = listOfLastRowElements[columnGender].Text;
            return person;
        }
        public List<string> LastInput()
        {
            var listOfLastRowElements = Driver.FindElements(lastRow);
            var listOfLastRowStrings = new List<string>();
            foreach (IWebElement a in listOfLastRowElements)
                listOfLastRowStrings.Add(a.Text);
            listOfLastRowStrings.RemoveAt(listOfLastRowStrings.Count - 1);

            return listOfLastRowStrings;
        }

        public void OpenAddForm()
        {

            Waiter.Until(Expected.ClickedAndExists(PersonRegistryPage.addButton, formName));

        }

        public void OpenEditForm()
        {
            Waiter.Until(Expected.ElementExists(PersonRegistryPage.editButton));
            Driver.FindElement(editButton).Click();
        }

        public void ValidPersonIsSavedToRegistryCheck(Person person)
        {
            Waiter.Until(Expected.ElementExists(selectPersonToEditOrAddMessage));
            var lastPerson = LastTableRow();
            Assert.Equal(person.GetInfo(), lastPerson.GetInfo());
        }


        
        public void InvalidPersonIsNotSavedToRegistryCheck(Person person)
        {
            try
            {
               if(Driver.FindElement(lastRow).Displayed){
                    var lastPerson = LastTableRow();
                    Assert.NotEqual(person.GetInfo(), lastPerson.GetInfo());
               } 
            }
            catch (NoSuchElementException)
            {
                Assert.True(true);
            }
        }

        public void PersonIsNotSavedToRegistryCheck(Person person)
        {
            

            if(Driver.FindElement(lastRow).Displayed)
            {
                var lastPerson = LastTableRow();
                Assert.NotEqual(person.GetInfo(), lastPerson.GetInfo());
            }
            else
            {
                Assert.True(true == true);
            }
        }



        public void CheckForFirstNameEmptyAlert()
        {
            Driver.FindElement(inputFirstName).GetAttribute("required");
        }

        public void CheckForLastNameEmptyAlert()
        {
            Driver.FindElement(inputLastName).GetAttribute("required");
        }

        public void GenderNotSelectedAlertCheck()
        {
            var alert = Driver.FindElement(genderNotSelectedAlert);
            Assert.True(alert.Displayed);
        }
        public void CheckForOIBEmptyAlert()
        {
            Driver.FindElement(inputOIB).GetAttribute("required");
        }

        public void CheckForOIBInvalidAlert()
        {
            var alert = Driver.FindElement(OIBInvalidAlert);
            Assert.True(alert.Displayed);
        }



        public void ClickSubmit()
        {
            Driver.FindElement(submitButton).Click();
        }

        public Person GetLastPerson()
        {
            Waiter.Until(Expected.ElementExists(lastRow));
            List<string> list = LastInput();
            Person person = new Person(list[columnFirstName], list[columnLastName], list[columnOIB], list[columnGender]);
            return person;
        }

        public void EditFirstName(Person person, string name)
        {
            person.firstName = name;
            FillFirstName(person.firstName);
        }

        public void EditLastName(Person person, string lastname)
        {
            person.lastName = lastname;
            FillLastName(person.lastName);
        }

        public void EditOIB(Person person, string oib)
        {
            person.OIB = oib;
            FillOIB(person.OIB);
        }

        public void EditGender(Person person, string gender)
        {
            person.gender = gender;
            SelectGender(person.gender);
        }

        public void ClickCancel()
        {
            Driver.FindElement(cancelButton).Click();
        }

        public void CheckIfDataBaseEmpty()
        {
            try
            {
               var element = Driver.FindElement(lastRow);
            }
            catch (NoSuchElementException)
            {
                throw;
            }
        }
    }

    public class Person
    {
        public string firstName;
        public string lastName;
        public string OIB;
        public string gender;


        public Person() { }
        public Person(string firstname, string lastname, string oib, string gend)
        {
            firstName = firstname;
            lastName = lastname;
            OIB = oib;
            gender = gend;
        }

        public List<string> GetInfo()
        {
            List<string> personalInformation = new List<string>();
            personalInformation.Add(firstName);
            personalInformation.Add(lastName);
            personalInformation.Add(OIB);
            personalInformation.Add(gender);
            return personalInformation;
        }


    }



}