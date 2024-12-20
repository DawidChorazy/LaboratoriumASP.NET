using Data.Entities;
using WebApp.Models;

namespace WebApp.Mappers;

public class ContactMapper
{
    // Map from Entity (database) to Model (application)
    public static ContactModel FromEntity(ContactEntity entity)
    {
        return new ContactModel()
        {
            Id = entity.Id,
            FirstName = entity.Name,
            Email = entity.Email,
            PhoneNumber = entity.Phone,
            Birth = entity.BirthDate,
        };
    }

    // Map from Model (application) to Entity (database)
    public static ContactEntity ToEntity(ContactModel model)
    {
        return new ContactEntity()
        {
            Id = model.Id,
            Name = model.FirstName,
            Email = model.Email,
            Phone = model.PhoneNumber,
            BirthDate = model.Birth,
        };
    }
}