using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; } //data sent to user
        public bool Success { get; set; } = true; //success prompt sent to user
        public string Message { get; set; } = string.Empty; //message prompt in case of an error sent to user
    }
}