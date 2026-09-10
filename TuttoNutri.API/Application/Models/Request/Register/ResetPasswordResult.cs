using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.Request.Register
{
    public class ResetPasswordResult
    {
        public bool Success { get; private set; }
        public IEnumerable<string> Errors { get; private set; } = new List<string>();

        public static ResetPasswordResult Ok()
        {
            return new ResetPasswordResult { Success = true };
        }

        public static ResetPasswordResult Fail(string error)
        {
            return new ResetPasswordResult { Success = false, Errors = new List<string> { error } };
        }

        public static ResetPasswordResult Fail(IEnumerable<string> errors)
        {
            return new ResetPasswordResult { Success = false, Errors = errors };
        }
    }
}