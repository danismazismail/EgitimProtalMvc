﻿using ApplicationCore.Consts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.UserEntites.Abstract
{
    public abstract class BaseRoleIdentity : IdentityRole<Guid>
    {
        private DateTime _createdDate = DateTime.Now;
        private Status _status = Status.Active;

        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }

        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public Status Status { get => _status; set => _status = value; }
    }
}
