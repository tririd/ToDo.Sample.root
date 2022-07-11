using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.WebAPI.Helper
{
    public static class CommonFunction
    {
        public static ApiResponse GetApiResponse(object data, string message, string errorMessage)
        {
            ApiResponse response = new ApiResponse();
            response.Data = data;
            response.Message = message;
            response.Error = errorMessage;
            return response;
        }
        public static string EscapeXML(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            else
                return value.Replace("\"", "&quot;").Replace("&", "&amp;").Replace("'", "&apos;").Replace("<", "&lt;").Replace(">", "&gt;");
        }
    }
}
