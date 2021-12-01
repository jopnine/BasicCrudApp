using System;

public class User
{
	public User()
	{
	}
	 
	public User(int id)
    {
		this.ID = id;
    }

	public User(string name, email, password, char sex)
    {
		this.Name = name;
		this.Email = email;
		this.Password = password;
		this.Sex = sex;
    }

	public int ID { get; set; }

	public char Sex { get; set; }
	
	public string Email { get; set; }

	public string Password { get; set; }

	public string Name { get; set; }

}
