namespace Web_site1.Domain.Entities
{
    public class UserProfileViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public string AvatarUrl { get; set; }
        public string Bio { get; set; }
        public UserRank Rank { get; set; }
        public int PurchaseCount { get; set; }
        public string Role { get; set; }
    }

}
