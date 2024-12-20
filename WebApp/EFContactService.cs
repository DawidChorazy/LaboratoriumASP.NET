using Data;
using Data.Entities;
using WebApp.Mappers;
using WebApp.Models;

public class EFContactService : IContactService
{
    private AppDbContext _context;

    public EFContactService(AppDbContext context)
    {
        _context = context;
    }

    public int Add(ContactModel contact)
    {
        var e = _context.Contacts.Add(ContactMapper.ToEntity(contact));
        _context.SaveChanges();
        return e.Entity.Id;
    }

    public void Delete(int id)
    {
        ContactEntity? find = _context.Contacts.Find(id);
        if ( find != null)
        {
            _context.Contacts.Remove(find);
        }
    }

    public List<ContactModel> FindAll()
    {
        return _context.Contacts.Select(e => ContactMapper.FromEntity(e)).ToList(); ;
    }

    public ContactModel? FindById(int id)
    {
        return ContactMapper.FromEntity(_context.Contacts.Find(id));
    }

    public void Update(ContactModel contact)
    {
        _context.Contacts.Update(ContactMapper.ToEntity(contact));
    }
}