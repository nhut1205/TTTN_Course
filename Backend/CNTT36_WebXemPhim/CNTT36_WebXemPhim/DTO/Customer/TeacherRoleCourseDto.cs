namespace CNTT36_WebXemPhim.DTO.Customer
{
    public class TeacherRoleCourseDto
    {
        public int ActorRoleFilmId { get; set; }

        //public int? ActorId { get; set; }

        //public int? RoleFilmId { get; set; }

        //public int? MovieId { get; set; }
        
        public List<TeacherDto> Actors { get; set; } = new List<TeacherDto>();
    }
}
