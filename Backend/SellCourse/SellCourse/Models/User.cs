
namespace CNTT36_WebXemPhim.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string MembershipStatus { get; set; } = "Free";

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual DetailUser? DetailUser { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<RatingFilm> RatingFilms { get; set; } = new List<RatingFilm>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
