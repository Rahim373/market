using System.Collections.Generic;
using System.Linq;

namespace Market.Application.Models
{
    public enum MessageType
    {
        Success = 1,
        Warning = 2,
        Error = 3
    }

    public class ResponseMessage
    {
        public string Message { get; set; }
        public MessageType Type { get; set; }
    }

    public class ResponseViewModel
    {
        public ResponseViewModel()
        {
            Messages = new List<ResponseMessage>();
        }

        public bool Success { get; private set; }
        public string Message { get => Messages.FirstOrDefault()?.Message; }
        public List<ResponseMessage> Messages { get; private set; }


        public void Succeed() => Success = true;
        public void Failure() => Success = true;
        public void AddMessage(string message, MessageType type) => Messages.Add(new ResponseMessage
        {
            Message = message,
            Type = type
        });
    }

    public class ResponseViewModel<T> : ResponseViewModel
    {
        public T Entity { get; set; }
    }

    public class ResponseViewModel<T1, T2> : ResponseViewModel<T1>
    {
        public T2 AdditionalData { get; set; }
    }

    public class GridResponseViewModel<T>
    {
        /// <summary>
        /// Records
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Total number of records
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Current page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Number of records in a page
        /// </summary>
        public int PageLength { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; set; }
    }
}
