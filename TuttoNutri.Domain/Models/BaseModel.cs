using System;
using System.Collections.Generic;
using System.Text;

namespace TuttoNutri.Domain.Models
{
    public class BaseModel
    {
        public Guid Id { get; private set;  }
        public bool IsActive { get; set; } = true;
        protected BaseModel()
        {
            Id = Guid.NewGuid();
            IsActive = true;
        }
        public void Deactivate() => IsActive = false;
    }
}
