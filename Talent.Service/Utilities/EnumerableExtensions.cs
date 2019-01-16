using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Talent.Service.Utilities
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> values, Func<T, string> value, Func<T, string> text, Predicate<T> selectedItem = null, bool includeEmptyOption = false, string emptyOptionDisplayText = "", bool allowSort = false, bool selectFirst = false)
            where T : class
        {
            var items = new List<SelectListItem>();

            if (includeEmptyOption)
            {
                items.Add(new SelectListItem
                {
                    Value = string.Empty,
                    Text = emptyOptionDisplayText,
                    Selected = selectedItem == null
                });
            }

            if (allowSort)
            {
                values = values.OrderBy(text);
            }



            items.AddRange(values.Select(v => new SelectListItem
            {
                Value = value(v),
                Text = text(v),
                Selected = selectedItem != null && selectedItem(v)
            }));

            if (selectFirst && items.Any())
            {
                items.First().Selected = true;
            }


            return items;
        }
    }
}
