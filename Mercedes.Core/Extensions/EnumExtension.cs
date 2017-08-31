using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mercedes.Core.Extensions
{
    public static class EnumExtension
    {
        public static SelectList ToSelectList<TEnum> (this TEnum enumItem, bool markAsSelected = true) where TEnum:struct
        {
            if(!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("An Enumeration type is required.", "enumItem");
            }
            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
            select new { ID = Convert.ToInt32(enumValue), Name = enumValue.ToString() };
            object selectedValue = null;
            if(markAsSelected)
            {
                selectedValue = Convert.ToInt32(enumItem);
            }
            return new SelectList(values, "ID", "Name", selectedValue);
        }
    }
}
