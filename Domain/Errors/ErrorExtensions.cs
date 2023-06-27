using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Error
{
    public static class ErrorExtensions
    {
        public static string GetAllMessages(this Exception exception)
        {
            var sb = new StringBuilder();
            var innerException = exception;
            var exCounter = 1;

            while (innerException != null)
            {
                sb.AppendJoin("\r", exCounter
                                    + (exCounter == 1
                                        ? " - Exception: "
                                        : " - Inner Exception: ")
                                    + (string.IsNullOrEmpty(innerException.Message)
                                        ? string.Empty
                                        : innerException.Message));

                innerException = innerException.InnerException;
                exCounter++;
            }

            return sb.ToString();
        }

        public static string ExtraDebugInfo(this string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var sb = new StringBuilder();

            sb.Append("message: " + message + "\r\n");
            sb.Append("    member name: " + memberName + "\r\n");
            sb.Append("    source file path: " + sourceFilePath + "\r\n");
            sb.Append("    source line number: " + sourceLineNumber + "\r\n");

            //Debug.WriteLine("message: " + message);
            //Debug.WriteLine("    member name: " + memberName);
            //Debug.WriteLine("    source file path: " + sourceFilePath);
            //Debug.WriteLine("    source line number: " + sourceLineNumber);

            return sb.ToString();
        }

    }
}
