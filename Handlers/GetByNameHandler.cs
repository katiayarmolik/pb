using PvHttpRouter.Abstractions;
using PvHttpRouter.Attributes;
using System.Net;
using System.Text;
using System.Text.Json;

namespace PhoneBookApi.Handlers
{
    [Route("by-name")]
    public class GetByNameHandler : IRouteHandler
    {
        private readonly PhoneBook _phoneBook;

        public GetByNameHandler(PhoneBook phoneBook)
        {
            _phoneBook = phoneBook;
        }

        public async Task HandleAsync(HttpListenerContext context)
        {
            var query = context.Request.QueryString["name"];
            var contact = _phoneBook.GetContactByName(query);

            context.Response.ContentType = "application/json";
            if (contact != null)
            {
                var responseJson = JsonSerializer.Serialize(contact);
                var bytes = Encoding.UTF8.GetBytes(responseJson);
                await context.Response.OutputStream.WriteAsync(bytes, 0, bytes.Length);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var errorMessage = Encoding.UTF8.GetBytes("Contact not found");
                await context.Response.OutputStream.WriteAsync(errorMessage, 0, errorMessage.Length);
            }

            context.Response.OutputStream.Close();
        }
    }
}
