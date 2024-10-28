using PvHttpRouter.Abstractions;
using PvHttpRouter.Attributes;
using System.Net;
using System.Text;
using System.Text.Json;

using PhoneBookApi.Entities;

namespace PhoneBookApi.Handlers
{
    [Route("create")]
    public class CreateContactHandler : IRouteHandler
    {
        private readonly PhoneBook _phoneBook;

        public CreateContactHandler(PhoneBook phoneBook)
        {
            _phoneBook = phoneBook;
        }

        public async Task HandleAsync(HttpListenerContext context)
        {
            var contact = await JsonSerializer.DeserializeAsync<Contact>(context.Request.InputStream);
            _phoneBook.AddContact(contact);

            context.Response.StatusCode = (int)HttpStatusCode.Created;
            await context.Response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes("Contact created"));
            context.Response.OutputStream.Close();
        }
    }
}
