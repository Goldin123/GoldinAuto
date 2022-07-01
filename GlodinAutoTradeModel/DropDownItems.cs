using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GlodinAutoTradeModel
{
    public static class DropDownItems
    {
        public static List<SelectListItem> ProductCategory()
        {
            List<SelectListItem> options = new List<SelectListItem>()
            {
               new SelectListItem
                {
                    Text = "Engine",
                    Value = "Engine"
                },
               new SelectListItem
                {
                    Text = "Transmission",
                    Value = "Transmission"
                },
               new SelectListItem
                {
                    Text = "Battery",
                    Value = "Battery"
                },
               new SelectListItem
                {
                    Text = "Alternator",
                    Value = "Alternator"
                },
               new SelectListItem
                {
                    Text = "Radiator",
                    Value = "Radiator"
                },
               new SelectListItem
                {
                    Text = "Steering ",
                    Value = "Steering "
                },
               new SelectListItem
                {
                    Text = "Suspension",
                    Value = "Suspension"
                },
               new SelectListItem
                {
                    Text = "Brakes",
                    Value = "Brakes"
                },

            };

            return options;
        }
        public static List<SelectListItem> ProductBrand()
        {
            List<SelectListItem> options = new List<SelectListItem>()
            {
               new SelectListItem
                {
                    Text = "VW",
                    Value = "VW"
                },
               new SelectListItem
                {
                    Text = "Audi",
                    Value = "Audi"
                },
               new SelectListItem
                {
                    Text = "Toyota",
                    Value = "Toyota"
                },
               new SelectListItem
                {
                    Text = "BMW",
                    Value = "BMW"
                },
               new SelectListItem
                {
                    Text = "Mercedes",
                    Value = "Mercedes"
                },
                new SelectListItem
                {
                    Text = "Ford",
                    Value = "Ford"
                },

            };

            return options;
        }





    }
}
