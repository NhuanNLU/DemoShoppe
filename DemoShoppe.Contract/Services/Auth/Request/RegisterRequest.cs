using DemoShoppe.Contract.Abstractions.Message;
using System.ComponentModel.DataAnnotations;

namespace DemoShoppe.Contract.Services.Auth.Request
{
    public record RegisterRequest : ICommand
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string PasswordHash { get; set; }

    }
}
