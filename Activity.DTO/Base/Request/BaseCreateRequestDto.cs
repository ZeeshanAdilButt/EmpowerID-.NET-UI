﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WaitLess.Core.Application.DTOs.Common
{
    public class BaseCreateRequestDto : BaseRequestDto
    {
        public BaseCreateRequestDto()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }

        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
