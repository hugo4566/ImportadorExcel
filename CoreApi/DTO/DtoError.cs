using System;
using System.Collections.Generic;

namespace CoreApi.DTO
{
    public class DtoError
    {
        public string Message { get; set; }
        public List<string> ErrorList { get; set; }
    }
}
