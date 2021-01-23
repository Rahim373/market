using System;
using Slugify;

namespace Market.Common
{
    public static class StringHelper
    {
        public static string Slugify(this string str)
        {
            if (str is null)
            {
                throw new ArgumentException("Empty string to slugify.");
            }

            SlugHelper helper = new SlugHelper();
            return helper.GenerateSlug(str);
        }
    }
}