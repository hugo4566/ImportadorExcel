using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;

namespace CoreApi.Utils
{
    [Serializable]
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public List<string> Errors { get; }

        public List<ValidationFailure> Failures { get; }

        public ApiException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            : base(message) => StatusCode = httpStatusCode;

        public ApiException(string message, List<ValidationFailure> failures, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            : base(message) 
        { 
            StatusCode = httpStatusCode;
            Errors = failures?.Select(f => f.ErrorMessage).ToList();
        }

        public ApiException(string message, List<string> errors, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = httpStatusCode;
            Errors = errors;
        }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
