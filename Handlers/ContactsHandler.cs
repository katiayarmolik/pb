using PvHttpRouter.Abstractions;
using PvHttpRouter.Attributes;
using System.Net;
using System.Text;
using System.Text.Json;

namespace PhoneBookApi.Handlers
{
    [Route("all")]
    public class ContactsHandler : IRouteHandler
    {
        private readonly PhoneBook _phoneBook;

        public ContactsHandler(PhoneBook phoneBook)
        {
            _phoneBook = phoneBook;
        }

        public async Task HandleAsync(HttpListenerContext context)
        {
            var contactsJson = JsonSerializer.Serialize(_phoneBook.GetAllContacts());
            var bytes = Encoding.UTF8.GetBytes(contactsJson);

            context.Response.ContentType = "application/json";
            await context.Response.OutputStream.WriteAsync(bytes, 0, bytes.Length);
            context.Response.OutputStream.Close();
        }
    }
}
