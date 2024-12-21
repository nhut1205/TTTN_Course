using AutoMapper;
using CNTT36_WebXemPhim.DTO;
using CNTT36_WebXemPhim.DTO.Admin.ActorsRoleFilm;
using CNTT36_WebXemPhim.DTO.Admin.Episode;
using CNTT36_WebXemPhim.DTO.Admin.Movie;
using CNTT36_WebXemPhim.DTO.Admin.User;
using CNTT36_WebXemPhim.DTO.Customer;
using CNTT36_WebXemPhim.DTO.Customer.Genre;
using CNTT36_WebXemPhim.DTO.Customer.Course;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.DTO.Customer.Movie;
using CNTT36_WebXemPhim.DTO.Admin.Teacher;
using CNTT36_WebXemPhim.DTO.Admin.Actor;
using CNTT36_WebXemPhim.DTO.Admin.RolesFilm;
using CNTT36_WebXemPhim.DTO.Admin.Subscription;

namespace CNTT36_WebXemPhim.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region course
            // Cấu hình ánh xạ cho Movie và MovieDto
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src => src.Lessons))
                .ForMember(dest => dest.TeacherRoleCourse, opt => opt.MapFrom(src => src.TeacherRoleCourses))
                .ForMember(dest => dest.CourseCate, opt => opt.MapFrom(src => src.CourseCates))
                .ReverseMap();

            // Cấu hình ánh xạ cho Episode và EpisodeDto
            CreateMap<Lesson, LessonDto>().ReverseMap();

            // Cấu hình ánh xạ cho ActorsRoleFilm và ActorsRoleFilmDto
            CreateMap<TeacherRoleCourse, TeacherRoleCourseDto>()
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => new List<TeacherDto>
                {
                new TeacherDto
                {
                    teacherId = src.Teacher.TeacherId,
                    Name = src.Teacher.Name,
                    Bio = src.Teacher.Bio,
                    ProfilePictureUrl = src.Teacher.ProfilePictureUrl
                }
                }))
                .ReverseMap();

            // Cấu hình ánh xạ cho MoviesGenre và MoviesGenreDto
            CreateMap<CourseCate, CourseCateDto>()
                .ForMember(dest => dest.categories, opt => opt.MapFrom(src => new List<CateDto>
                {
                new CateDto { Name = src.Cate.Name }
                }))
                .ReverseMap();

            // Cấu hình ánh xạ cho Genre và GenreDto
            CreateMap<Category, CateDto>().ReverseMap();

            #endregion

            #region Genre
            // Ánh xạ từ MovieGenre sang MovieGenreWithMoviesDto
            CreateMap<CourseCate, CourseCateWithCoursesDto>()
                .ForMember(dest => dest.Course2, opt => opt.MapFrom(src => new List<CourseDto>
                {
                new CourseDto
                {
                    CourseId = src.Course.CourseId,
                    Title = src.Course.Title,
                    Description = src.Course.Description,
                    Duration = src.Course.Duration,
                    Language = src.Course.Language,
                    ThumbnailUrl = src.Course.ThumbnailUrl,
                    ThumbnailUrl2 = src.Course.ThumbnailUrl2
                }
                }))
                .ReverseMap();

            // Ánh xạ từ Genre sang GenreWithMoviesDto
            CreateMap<Category, CateWithCoursesDto>()
                .ForMember(dest => dest.CourseCate2, opt => opt.MapFrom(src => src.CourseCates))
                .ReverseMap();
            #endregion

            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<UserRole, RoleDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<UserRole, UserRoleDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            #region admin
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<CreateMovieDto, Course>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => DateOnly.Parse(src.ReleaseDate)));

            CreateMap<UpdateMovieDto, Course>()
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => DateOnly.Parse(src.ReleaseDate)))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));



            CreateMap<CreateTeacherDto, Teacher>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<CreateTeacherDto, Teacher>()
                          .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.Parse(src.DateOfBirth)));


            CreateMap<RoleFilmDto, RoleCourse>().ReverseMap();

            CreateMap<CreateActorsRoleFilmDto, TeacherRoleCourse>();
            CreateMap<TeacherRoleCourse, ActorsRoleFilm2Dto>();
            CreateMap<TeacherRoleCourse, ActorRoleFilmDetailDto>()
                .ForMember(dest => dest.ActorName, opt => opt.MapFrom(src => src.Teacher.Name))
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleCourse.RoleName));
            CreateMap<UpdateActorRoleFilmDto, TeacherRoleCourse>();
            CreateMap<CreateActorsRoleFilmDto, TeacherRoleCourse>();
            CreateMap<TeacherRoleCourse, ActorsRoleFilm2Dto>();
            CreateMap<TeacherRoleCourse, ActorRoleFilmDetailDto>()
                .ForMember(dest => dest.ActorName, opt => opt.MapFrom(src => src.Teacher.Name))
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleCourse.RoleName));
            CreateMap<UpdateActorRoleFilmDto, TeacherRoleCourse>();

            CreateMap<Lesson, EpisodeAdminDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title));
            CreateMap<CreateEpisodeDto, Lesson>()
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => DateOnly.Parse(src.ReleaseDate)));
            CreateMap<UpdateEpisodeDto, Lesson>()
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => DateOnly.Parse(src.ReleaseDate)));


            CreateMap<ChangePasswordDto, User>();

            CreateMap<Subscription, SubscriptionDto>().ReverseMap();


            //Admin rolesfilm

            CreateMap<RoleCourse, RoleDto>().ReverseMap();
            CreateMap<CreateRolesCourseDto, RoleCourse>();

            CreateMap<UpdateRolesCourseDto, RoleCourse>();

            #endregion

        }
    }
}
