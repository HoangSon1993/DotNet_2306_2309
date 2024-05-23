namespace Buoi2_BaiTapDungTemplate.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime DayOfBirth { get; set; }
    public short Status { get; set; }
}