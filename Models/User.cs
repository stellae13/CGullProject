using System.ComponentModel.DataAnnotations;

namespace CGullProject;

public class User {

    [Required]
    public Guid Id { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string PasswordHash { get; set; }

    [Required]
    [StringLength(32, MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(32, MinimumLength = 2)]
    public string LastName { get; set; }

    [Required]
    [StringLength(256, MinimumLength = 2)]
    public string Address { get; set; }

    public ICollection<CartItem > CartItems { get; set; } = new List<CartItem>();
}