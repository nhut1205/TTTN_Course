using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNTT36_WebXemPhim.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToAdvertisement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    banner_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    banner_img = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    banner_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Banner__10373C349082E415", x => x.banner_id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    cate_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__34EAD17357C420E7", x => x.cate_id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    country_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Countrie__7E8CD055C0C20FCB", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    release_date = table.Column<DateOnly>(type: "date", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<double>(type: "float", nullable: true),
                    language = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    thumbnail_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    thumbnail_url2 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Courses__8F1EF7AEEE180E08", x => x.course_id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    news_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    decriptions = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    thumnail_url = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__News__4C27CCD8212ABE04", x => x.news_id);
                });

            migrationBuilder.CreateTable(
                name: "Role_Course",
                columns: table => new
                {
                    role_course_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role_Cou__C4703ABA5B34798C", x => x.role_course_id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__760965CCC6281E9B", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    teacher_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    bio = table.Column<string>(type: "text", nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    profile_picture_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teacher__03AE777EAB9D5E69", x => x.teacher_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    membership_status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__F3DBC57395CAAA61", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "Course_cate",
                columns: table => new
                {
                    course_cate_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    cate_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Course_c__8D434246D2AAB075", x => x.course_cate_id);
                    table.ForeignKey(
                        name: "FK__Course_ca__cate___6A30C649",
                        column: x => x.cate_id,
                        principalTable: "Categories",
                        principalColumn: "cate_id");
                    table.ForeignKey(
                        name: "FK__Course_ca__cours__693CA210",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id");
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    lesson_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    lesson_number = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    duration = table.Column<int>(type: "int", nullable: false),
                    video_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    release_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lessons__6421F7BE4AD20A7B", x => x.lesson_id);
                    table.ForeignKey(
                        name: "FK__Lessons__course___208CD6FA",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher_Role_Course",
                columns: table => new
                {
                    teacher_role_course_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teacher_id = table.Column<int>(type: "int", nullable: true),
                    role_course_id = table.Column<int>(type: "int", nullable: true),
                    course_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teacher___5B69A6359BEC912E", x => x.teacher_role_course_id);
                    table.ForeignKey(
                        name: "FK__Teacher_R__cours__73BA3083",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "FK__Teacher_R__role___72C60C4A",
                        column: x => x.role_course_id,
                        principalTable: "Role_Course",
                        principalColumn: "role_course_id");
                    table.ForeignKey(
                        name: "FK__Teacher_R__teach__71D1E811",
                        column: x => x.teacher_id,
                        principalTable: "Teacher",
                        principalColumn: "teacher_id");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    cart_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    added_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__2EF52A275E0A37BB", x => x.cart_id);
                    table.ForeignKey(
                        name: "FK__Cart__course_id__2EDAF651",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "FK__Cart__username__2DE6D218",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comments__E79576879CD3DAB6", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK__Comments__course__18EBB532",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "FK__Comments__userna__17F790F9",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "Detail_Users",
                columns: table => new
                {
                    detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    gender = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    avatar_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detail_U__38E9A224B8FF4ECC", x => x.detail_id);
                    table.ForeignKey(
                        name: "FK__Detail_Us__usern__534D60F1",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    enrollment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    enrollment_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    progress = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    completion_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Enrollme__6D24AA7A8F8F3B07", x => x.enrollment_id);
                    table.ForeignKey(
                        name: "FK__Enrollmen__cours__66603565",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "FK__Enrollmen__usern__656C112C",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    history_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__History__096AA2E948266086", x => x.history_id);
                    table.ForeignKey(
                        name: "FK__History__course___02084FDA",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "FK__History__usernam__02FC7413",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    like_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Likes__992C79307D1EEE27", x => x.like_id);
                    table.ForeignKey(
                        name: "FK__Likes__course_id__08B54D69",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "FK__Likes__username__07C12930",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "Rating_Film",
                columns: table => new
                {
                    rating_film_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    rating = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rating_F__CA972B1C1453A190", x => x.rating_film_id);
                    table.ForeignKey(
                        name: "FK__Rating_Fi__cours__2A164134",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Rating_Fi__usern__29221CFB",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    subscription_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    plan = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PaymentLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrip__863A7EC1DD87DB5F", x => x.subscription_id);
                    table.ForeignKey(
                        name: "FK__Subscript__usern__7E37BEF6",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "User_Roles",
                columns: table => new
                {
                    user_role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Rol__B8D9ABA2C191CA5A", x => x.user_role_id);
                    table.ForeignKey(
                        name: "FK__User_Role__role___571DF1D5",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "role_id");
                    table.ForeignKey(
                        name: "FK__User_Role__usern__5629CD9C",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "Watchlist",
                columns: table => new
                {
                    watchlist_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    added_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Watchlis__36A90B31AB1E9318", x => x.watchlist_id);
                    table.ForeignKey(
                        name: "FK__Watchlist__cours__787EE5A0",
                        column: x => x.course_id,
                        principalTable: "Courses",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "FK__Watchlist__usern__778AC167",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "Payment_Transactions",
                columns: table => new
                {
                    transaction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subscription_id = table.Column<int>(type: "int", nullable: false),
                    transaction_date = table.Column<DateOnly>(type: "date", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment___85C600AF11C41AE6", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK__Payment_T__subsc__25518C17",
                        column: x => x.subscription_id,
                        principalTable: "Subscriptions",
                        principalColumn: "subscription_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_course_id",
                table: "Cart",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_username",
                table: "Cart",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_course_id",
                table: "Comments",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_username",
                table: "Comments",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "UQ__Countrie__F70188942829FAC8",
                table: "Countries",
                column: "country_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_cate_cate_id",
                table: "Course_cate",
                column: "cate_id");

            migrationBuilder.CreateIndex(
                name: "IX_Course_cate_course_id",
                table: "Course_cate",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Detail_U__F3DBC572365486A0",
                table: "Detail_Users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_course_id",
                table: "Enrollments",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_username",
                table: "Enrollments",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_History_course_id",
                table: "History",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_History_username",
                table: "History",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_course_id",
                table: "Lessons",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_course_id",
                table: "Likes",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_username",
                table: "Likes",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Transactions_subscription_id",
                table: "Payment_Transactions",
                column: "subscription_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_Film_course_id",
                table: "Rating_Film",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_Film_username",
                table: "Rating_Film",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__783254B17DFB5884",
                table: "Roles",
                column: "role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_username",
                table: "Subscriptions",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_Role_Course_course_id",
                table: "Teacher_Role_Course",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_Role_Course_role_course_id",
                table: "Teacher_Role_Course",
                column: "role_course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_Role_Course_teacher_id",
                table: "Teacher_Role_Course",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_role_id",
                table: "User_Roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_username",
                table: "User_Roles",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__AB6E616451A79735",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Watchlist_course_id",
                table: "Watchlist",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Watchlist_username",
                table: "Watchlist",
                column: "username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Course_cate");

            migrationBuilder.DropTable(
                name: "Detail_Users");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Payment_Transactions");

            migrationBuilder.DropTable(
                name: "Rating_Film");

            migrationBuilder.DropTable(
                name: "Teacher_Role_Course");

            migrationBuilder.DropTable(
                name: "User_Roles");

            migrationBuilder.DropTable(
                name: "Watchlist");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Role_Course");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
