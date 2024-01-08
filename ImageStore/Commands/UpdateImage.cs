using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStore.Commands
{
    public class UpdateImage : BaseCommand
    {
        public Guid Id { get; set; }
        public byte[] ImageData { get; set; }
    }
}
