using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GlupiApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GlupiApp.Pages
{
    public partial class PersonRegistry
    {
        [Inject] private AppDbContext db { get; set; }
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
        public IList<Person> Persons = new List<Person>();
        public static readonly Dictionary<int, string> Genders = new Dictionary<int, string>
        {
            { 0, "Please select"},
            { 1, "Male" },
            { 2, "Female" },
            { 3, "Other" },
        };
        public Person Selected { get; set; }
        private Dictionary<string, string> _errors;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Persons = await db.Persons.Include(p => p.CreatedBy).ToListAsync();
                StateHasChanged();
            }
        }

        private void AddClicked()
        {
            Selected = new Person();
        }
        private void EditClicked(Person item)
        {
            var edit = new Person
            {
                Id = item.Id,
                CreatedBy = item.CreatedBy,
                CreatedById = item.CreatedById,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Gender = item.Gender,
                OIB = item.OIB
            };

            Selected = edit;
        }
        private void CancelClicked() => Selected = null;
        private async Task<EventCallback<EventArgs>> SaveCreateClicked()
        {
            if (Selected == null)
                return default;

            _errors = Selected.Validate();
            if (_errors != null)
                return default;

            var state = await authenticationStateTask;
            var idStr = state.User.FindFirst(ClaimTypes.Sid);
            Selected.CreatedById = Convert.ToInt32(idStr.Value);

            db.Persons.Add(Selected);
            await db.SaveChangesAsync();

            Persons.Add(Selected);

            Selected = null;

            StateHasChanged();

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

            var original = Persons.SingleOrDefault(p => p.Id == Selected.Id);
            original.FirstName = Selected.FirstName;
            original.LastName = Selected.LastName;
            original.OIB = Selected.OIB;
            original.Gender = Selected.Gender;

            await db.SaveChangesAsync();

            Selected = null;

            return default;
        }
    }
}