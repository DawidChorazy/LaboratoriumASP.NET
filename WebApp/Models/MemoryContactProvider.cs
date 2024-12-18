namespace WebApp.Models;

public class MemoryContactProvider
{
    private readonly IDateTimeProvider _timeProvider;

    private static readonly Dictionary<int, ContactModel> _contacts = new();
    private static int _currentId = 0;

    public MemoryContactProvider(IDateTimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public void Add(ContactModel model)
    {
        model.Id = ++_currentId;
        model.Created = _timeProvider.GetCurrentDateTime(); 
        _contacts[model.Id] = model;
    }

    public void Delete(int id)
    {
        _contacts.Remove(id);
    }

    public List<ContactModel> GetAll()
    {
        return _contacts.Values.ToList();
    }
}