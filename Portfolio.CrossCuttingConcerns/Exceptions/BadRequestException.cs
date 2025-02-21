using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.CrossCuttingConcerns.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException() : base("Bad request exception")
    {
        
    }

    public BadRequestException(string message) : base(message)
    {
        
    }
}