using System;

namespace UrbanBlimp.Apple
{
    public class AddRegistrationService
    {
        public IRequestBuilder RequestBuilder;

        public void Execute(string deviceToken, Registration registration, Action callback, Action<Exception> exceptionCallback)
        {
            var request = RequestBuilder.Build("https://go.urbanairship.com/api/device_tokens/" + deviceToken);
            request.Method = "PUT";

            var asyncRequest = new AsyncRequest
            {
                WriteToRequest = stream => stream.WriteToStream(registration.Serialize),
                Request = request,
                ReadFromResponse = o => callback(),
                ExceptionCallback = exceptionCallback,
            };

            asyncRequest.Execute(); ;

        }

        public void Execute(string deviceToken, Action callback, Action<Exception> exceptionCallback)
        {
            var request = RequestBuilder.Build("https://go.urbanairship.com/api/device_tokens/" + deviceToken);
            request.Method = "PUT";
            var asyncRequest = new AsyncRequest
            {
                Request = request,
                ReadFromResponse = o => callback(),
                ExceptionCallback = exceptionCallback,
            };

            asyncRequest.Execute(); 
        }
    }
}