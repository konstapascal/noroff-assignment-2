﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataReader.Models
{
    internal class CustomerSpender
    {
        public Customer Customer { get; set; }
        public double TotalAmountSpent { get; set; }
    }
}
