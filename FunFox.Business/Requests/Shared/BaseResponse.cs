﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunFox.Business.Requests.Shared
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
