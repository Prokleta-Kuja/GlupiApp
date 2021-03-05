using System.Collections.Generic;
using GlupiApp.Tests.Enums;
using OpenQA.Selenium;

namespace GlupiApp.Tests.Mappings
{
    public static class NavigationMapping
    {
        static NavigationMapping()
        {
            Mapping = new Dictionary<Navigation, By>
            {
                { Navigation.Home, By.LinkText("Home") },
                { Navigation.Counters, By.LinkText("Counter") },
                { Navigation.FetchData, By.LinkText("Fetch data") },
            };
        }
        private static Dictionary<Navigation, By> Mapping { get; set; }
        public static By GetBySelector(Navigation nav)
        {
            if (Mapping.ContainsKey(nav))
                return Mapping[nav];

            else
                throw new System.Exception();
        }
    }
}