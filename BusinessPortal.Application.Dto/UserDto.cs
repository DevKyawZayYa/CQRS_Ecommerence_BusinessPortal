using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? Company { get; set; }
        public string? Abbreviation { get; set; }
        public string Client { get; set; }
        public string Secret { get; set; }
    }
}
