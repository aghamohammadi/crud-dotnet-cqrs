using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.Dtos
{
    public class ValidationErrorResponseDto
    {
        public string Message { get; set; }
        public string Errors { get; set; }
    }
}
