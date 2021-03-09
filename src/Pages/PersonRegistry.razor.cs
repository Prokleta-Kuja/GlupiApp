using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlupiApp.Data;
using Microsoft.AspNetCore.Components;

namespace GlupiApp.Pages
{
    public partial class PersonRegistry
    {
        public static readonly IList<Person> Persons = new List<Person>();
        public static readonly Dictionary<int, string> Genders = new Dictionary<int, string>
        {
            { 0, "Please select"},
            { 1, "Male" },
            { 2, "Female" },
            { 3, "Other" },
        };
        public Person Selected { get; set; }
        private Dictionary<string, string> _errors;

        private void AddClicked()
        {
            Selected = new Person();
        }
        private void EditClicked(Person item)
        {
            Selected = item;
        }
        private void CancelClicked() => Selected = null;
        private async Task<EventCallback<EventArgs>> SaveCreateClicked()
        {
            await Task.CompletedTask;

            if (Selected == null)
                return default;

            _errors = Selected.Validate();
            if (_errors != null)
                return default;

            var max = Persons.Any() ? Persons.Max(p => p.Id) + 1 : 1;
            Selected.Id = max;
            Persons.Add(Selected);

            Selected = null;

            return default;
        }
        private async Task<EventCallback<EventArgs>> SaveEditClicked()
        {
            await Task.CompletedTask;

            if (Selected == null)
                return default;

            _errors = Selected.Validate();
            if (_errors != null)
                return default;

            Selected = null;

            return default;
        }
    }
}