
namespace CNTT36_WebXemPhim.Models;

public partial class RatingFilm
{
    public int RatingFilmId { get; set; }

    public int CourseId { get; set; }

    public string Username { get; set; } = null!;

    public double? Rating { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
