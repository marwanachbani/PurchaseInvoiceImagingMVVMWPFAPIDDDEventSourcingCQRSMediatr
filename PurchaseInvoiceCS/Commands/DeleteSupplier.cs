﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Commands
{
    public class DeleteSupplier : BaseCommand
    {
        public Guid Id { get; set; }
       
    }
}
