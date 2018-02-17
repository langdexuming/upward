﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Reggie.Upward.WebApi.Areas.PlatformAccount.Models
{
    public class Account
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string PasswordHash { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(20)]
        public string PlatformName { get; set; }
        [StringLength(50)]
        public string Remark { get; set; }
    }
}
