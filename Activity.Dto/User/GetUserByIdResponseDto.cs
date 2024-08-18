using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity.Dto
{
    public class GetUserByIdResponseDto
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
    }
}
