namespace SpecflowPlayground.CodeThisNotThat
{
    internal class ServiceProxy
    {
        public Response Post(Customer customer)
        {
            return new Response() {IsSuccessful = true};
        }
    }

    internal class Response
    {
        public bool IsSuccessful { get; set; }
    }
}