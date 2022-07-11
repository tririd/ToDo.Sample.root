using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.WebAPI.Helper
{
    public class ApiResponse
    {
        /// <summary>
        /// API Data response.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// The Message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        ///  Error
        /// </summary>
        public string Error { get; set; } = string.Empty;
    }
}
