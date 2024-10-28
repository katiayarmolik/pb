using PvHttpRouter.Abstractions;
using PvHttpRouter.Attributes;
using System.Net;
using System.Text;
using System.Text.Json;

using PhoneBookApi.Entities;

namespace PhoneBookApi.Handlers
{
    [Route("delete")]
    public class DeleteContactHandler : IRouteHandler
    {
        private readonly PhoneBook _phoneBook;

        public DeleteContactHandler(PhoneBook phoneBook)
        {
            _phoneBook = phoneBook;
        }

        public async Task HandleAsync(HttpListenerContext context)
        {
            var requestData = await JsonSerializer.DeserializeAsync<Contact>(context.Request.InputStream);
            _phoneBook.DeleteContactByPhone(requestData.Phone);

            await context.Response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes("Contact deleted"));
            context.Response.OutputStream.Close();
        }
    }
}
