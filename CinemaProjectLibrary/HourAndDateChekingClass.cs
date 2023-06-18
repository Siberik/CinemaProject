﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProjectLibrary
{
    public class HourAndDateChekingClass
    {
        public bool IsValidTimeFormat(string input)
        {
            string[] formats = { "h\\:mm", "hh\\:mm", "H\\:mm", "HH\\:mm", "H\\:mm\\:ss", "HH\\:mm\\:ss", "hh\\:mm\\:ss", "h\\:mm\\:ss", "HH\\:mm\\:ss", "H\\:mm\\:ss", "hh\\:mm\\:ss", "H\\:mm\\:ss", "HH\\:mm\\:ss", "h\\:mm\\:ss" };
            TimeSpan timeSpan;

            return TimeSpan.TryParseExact(input, formats, null, out timeSpan);
        }





    }
}
