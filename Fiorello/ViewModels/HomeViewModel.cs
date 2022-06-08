﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorello.Models;

namespace Fiorello.ViewModels
{
    public class HomeViewModel
    {
        public List<Slide> Slides { get; set; }
        public Summary Summary { get; set; }
    }
}