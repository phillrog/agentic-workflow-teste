namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    private User(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public static User Create(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required");
        
        return new User(Guid.NewGuid(), name, email);
    }

    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
    }
}