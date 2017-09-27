using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ardalis.GuardClauses
{
    public static class GuardExtensions
    {
        public static void EmptyCollection<T>(this IGuardClause guardClause, ICollection<T> input, string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (input.Count == 0)
                throw new ArgumentException($"Required input {parameterName} cannot be an empty collection.", parameterName);
        }
    }
}
