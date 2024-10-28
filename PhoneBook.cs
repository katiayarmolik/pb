using PhoneBookApi.Entities;

namespace PhoneBookApi;

public class PhoneBook
{
    private List<Contact> contacts = new List<Contact>();

    public List<Contact> GetAllContacts() => contacts;

    public Contact GetContactByPhone(string phone) => contacts.FirstOrDefault(c => c.Phone == phone);

    public Contact GetContactByName(string name) => contacts.FirstOrDefault(c => c.Name == name);

    public void AddContact(Contact contact) => contacts.Add(contact);

    public void DeleteContactByPhone(string phone)
    {
        var contact = GetContactByPhone(phone);
        if (contact != null) contacts.Remove(contact);
    }
}
