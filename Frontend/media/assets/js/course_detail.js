// Hàm để lấy thông tin phim theo courseId
function loadMovieDetails() {
  // Lấy courseId từ URL
  const urlParams = new URLSearchParams(window.location.search);
  const courseId = urlParams.get("courseId");

  if (!courseId) {
    alert("Không tìm thấy courseId trong URL!");
    return;
  }

  // Thay đổi liên kết của nút WATCH NOW để giữ lại courseId
  document
    .getElementById("watch-now-link")
    .setAttribute("href", `watch_film.html?courseId=${courseId}`);

  // Gọi API để lấy thông tin phim
  fetch(`https://localhost:7252/api/Home/${courseId}`)
    .then((response) => response.json())
    .then((data) => {
      const rating = data.formattedRating;

      let starHtml = "";
      console.log(data.lessons.length);
      // Kiểm tra nếu phim không có tập nào thì ẩn nút WATCH NOW
      if (!data.lessons || data.lessons.length === 0) {
        var button = document.getElementById("watch-now-link");
        button.classList.add("disabled");
      }

      if (rating === 0) {
        starHtml = "<p>No ratings yet</p>"; // Hiển thị thông báo nếu không có đánh giá
      } else {
        const fullStars = Math.floor(rating);
        const decimalPart = rating - fullStars;
        let halfStar = 0;
        let emptyStars = 0;

        // Xác định số sao nửa đầy và sao trống
        if (decimalPart >= 0.75) {
          halfStar = 0;
          emptyStars = 5 - (fullStars + 1);
        } else if (decimalPart >= 0.25) {
          halfStar = 1;
          emptyStars = 5 - (fullStars + halfStar);
        } else {
          halfStar = 0;
          emptyStars = 5 - fullStars;
        }

        // Tạo chuỗi HTML cho các ngôi sao
        for (let i = 0; i < fullStars; i++) {
          starHtml += '<i class="fa fa-star" style="color: gold;"></i>';
        }
        if (halfStar) {
          starHtml +=
            '<i class="fa fa-star-half-alt" style="color: gold;"></i>';
        }
        for (let i = 0; i < emptyStars; i++) {
          starHtml += '<i class="fa fa-star-o" style="color: gold;"></i>';
        }
      }

      // Gọi API để lấy số lượng người đã đánh giá phim
      fetch(`https://localhost:7252/api/Rating/MovieRatings/${courseId}`)
        .then((response) => {
          if (!response.ok) {
            throw new Error("Không có dữ liệu đánh giá phim.");
          }
          return response.json();
        })
        .then((ratings) => {
          let totalRatings = ratings.length;
          let ratingsMessage =
            totalRatings === 0
              ? "No ratings found for this movie."
              : `${rating} / ${totalRatings} user`;

          // Hiển thị thông tin phim với số người đánh giá
          const filmDetailsHtml = `
    <div class="row">
        <div class="col-lg-8">
          <div class="card mb-4">
            <img
              src="${data.thumbnailUrl}"
              class="card-img-top course-banner"
              alt="Web Development"
              onerror="this.src='${data.thumbnailUrl}'"
            />
            <div class="card-body">
              <div
                class="d-flex justify-content-between align-items-center mb-3" >
                    <div class="badge bg-primary">
                    <div id="genres-list"></div>
                    </div>
                <div>
                  <i class="bi bi-star-fill text-warning"></i>
                  <span>${starHtml} (${ratingsMessage})</span>
                </div>
              </div>
              <h2 class="card-title">${data.title}</h2>
              <p class="card-text">
                ${data.description}
              </p>

              <div class="course-details mt-4">
                <h4>What you'll learn</h4>
                <ul class="list-unstyled">
                  <li>
                    <i class="bi bi-check-circle-fill text-success me-2"></i
                    >Build responsive websites using HTML5 and CSS3
                  </li>
                  <li>
                    <i class="bi bi-check-circle-fill text-success me-2"></i
                    >Master JavaScript and modern ES6+ features
                  </li>
                  <li>
                    <i class="bi bi-check-circle-fill text-success me-2"></i
                    >Create full-stack applications with React and Node.js
                  </li>
                  <li>
                    <i class="bi bi-check-circle-fill text-success me-2"></i
                    >Work with databases and APIs
                  </li>
                </ul>
              </div>

              

          <div class="card">
            <div class="card-body">
              <h4>Instructor</h4>
              <div class="d-flex align-items-center" id="actor-list">
              </div>
            </div>
          </div>
        </div>

        <div class="col-lg-4">
          <div class="card course-sidebar">
            <div class="card-body">
              <h3 class="text-success mb-3">${data.price} VND</h3>
              <button class="btn btn-primary w-100 mb-3" onclick="startPayment()">Enroll Now</button>
              <button class="btn btn-outline-primary w-100 mb-4" id="like-button">
                Add to Wishlist
              </button>

              <div class="course-info">
                <h4>Course Information</h4>
                <ul class="list-unstyled">
                  <li class="mb-2">
                    <i class="bi bi-clock me-2"></i>Duration: ${data.duration} weeks
                  </li>
                  <li class="mb-2">
                    <i class="bi bi-book me-2"></i>36 lessons
                  </li>
                  <li class="mb-2">
                    <i class="bi bi-people me-2"></i>${ratingsMessage} students
                  </li>
                  <li class="mb-2">
                    <i class="bi bi-translate me-2"></i>Language: ${data.language}
                  </li>
                  <li class="mb-2">
                    <i class="bi bi-patch-check me-2"></i>Certificate of
                    completion
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
        `;
          document.getElementById("film-details").innerHTML = filmDetailsHtml;
        })
        .catch((error) => {
          // Xử lý khi không có đánh giá hoặc khi có lỗi
          console.error("Error fetching ratings:", error);
          document.getElementById("film-details").innerHTML = `
              <div class="row">
        <div class="col-lg-8">
          <div class="card mb-4">
            <img
              src="${data.thumbnailUrl}"
              class="card-img-top course-banner"
              alt="Web Development"
              onerror="this.src='${data.thumbnailUrl}'"
            />
            <div class="card-body">
              <div
                class="d-flex justify-content-between align-items-center mb-3" >
                    <div class="badge bg-primary">
                          <div id="genres-list"></div>
                    </div>
                <div>
                  <i class="bi bi-star-fill text-warning"></i>
                </div>
              </div>
              <h2 class="card-title">${data.title}</h2>
              <p class="card-text">
                ${data.description}
              </p>

             </div>
             </div>

          <div class="card">
            <div class="card-body">
              <h4>Instructor</h4>
              <div class="d-flex align-items-center" id="actor-list">
              </div>
            </div>
          </div>
        </div>

        <div class="col-lg-4">
          <div class="card course-sidebar">
            <div class="card-body">
              <h3 class="text-success mb-3">${data.price} VND</h3>
              <button class="btn btn-primary w-100 mb-3" onclick="startPayment()">Enroll Now</button>
              <button class="btn btn-outline-primary w-100 mb-4" id="like-button">
                Add to Wishlist
              </button>
              <div class="course-info">
                <h4>Course Information</h4>
                <ul class="list-unstyled">
                  <li class="mb-2">
                    <i class="bi bi-clock me-2"></i>Duration: ${data.duration} weeks
                  </li>
                  <li class="mb-2">
                    <i class="bi bi-book me-2"></i>36 lessons
                  </li>
                  <li class="mb-2">
                    <i class="bi bi-people me-2"></i> 0 students
                  </li>
                  <li class="mb-2">
                    <i class="bi bi-translate me-2"></i>Language: ${data.language}
                  </li>
                  <li class="mb-2">
                    <i class="bi bi-patch-check me-2"></i>Certificate of
                    completion
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
              </div>`;
        });

        
      // Hiển thị danh sách tập phim
      const episodesHtml = data.lessons
        .map(
          (lesson) => `
      <button class="btn_episode">${lesson.lessonNumber}
      </button>
    `
        )
        .join("");
      document.getElementById("episode-list").innerHTML = episodesHtml;

      // Hiển thị danh sách giảng viên và thêm sự kiện click
      const teachersHtml = data.teacherRoleCourse
        .flatMap((role) =>
          role.actors.map(
            (actor) => `<img
                  src="${actor.profilePictureUrl}"
                  class="rounded-circle me-3"
                  alt="Instructor"
                  style="width: 60px; height: 60px; object-fit: cover"
                  onerror="this.src='${actor.profilePictureUrl}'"
                />
                <div>
                  <h5 class="mb-1">${actor.name}</h5>
                  <p class="mb-0">
                  ${actor.bio}
                  </p></div>
      `
          )
        )
        .join("");
      document.getElementById("actor-list").innerHTML = teachersHtml;

      
      // Hiển thị thể loại khóa học
      const genresHtml = data.courseCate
        .map((movieGenre) => {
          if (movieGenre.cate && movieGenre.cate.name) {
            return `<span class="badge bg-primary">${movieGenre.cate.name}</span>`;
          }
          return ""; // Tránh lỗi nếu cate hoặc name không tồn tại
        })
        .join(" ");

      document.getElementById("genres-list").innerHTML = genresHtml;

      // Hiển thị mô tả phim
      const filmDescription = data.description || "Mô tả không có sẵn"; // Đảm bảo mô tả luôn có giá trị
      document.getElementById("film-description").textContent = filmDescription;
    })
    .catch((error) => {
      console.error("Error fetching movie details:", error);
    });
}


// Hàm gọi API để lấy danh sách phim của diễn viên
function fetchMoviesByActor(actorId) {
  fetch(`https://localhost:7252/api/Actors/${teacherId}/movies`)
    .then((response) => response.json())
    .then((movies) => {
      if (movies.length === 0) {
        document.getElementById("actor-movies").innerHTML =
          "<p>No movies found for this actor.</p>";
        return;
      }

      // Hiển thị danh sách phim của diễn viên
      const moviesHtml = movies
        .map(
          (movie) => `
          <div class="movie">
            <img src="${movie.thumbnailUrl}" alt="${movie.title}" class="movie-poster" />
            <div class="movie-info">
              <h3>${movie.title}</h3>
              <p>${movie.description}</p>
              <p>${movie.duration} min</p>
              <p>${movie.language}</p>
              <p>Release Date: ${movie.release_Date}</p>
            </div>
          </div>
        `
        )
        .join("");

      document.getElementById("actor-movies").innerHTML = moviesHtml;
    })
    .catch((error) => {
      console.error("Error fetching movies for actor:", error);
      document.getElementById("actor-movies").innerHTML =
        "<p>Error loading movies.</p>";
    });
}

// Hàm để lấy danh sách thể loại
function loadGenres() {
  fetch("https://localhost:7252/api/Categori")
    .then((response) => response.json())
    .then((genres) => {
      if (Array.isArray(genres)) {
        const genresHtml = genres
          .map(
            (genre) => `
              <a href="course_Genres.html?genreId=${genre.cateId}">${genre.name}</a>
            `
          )
          .join("");
        document.getElementById("dropdown-content-genres").innerHTML =
          genresHtml;
      } else {
        console.error("Dữ liệu trả về không phải là mảng:", genres);
      }
    })
    .catch((error) => {
      console.error("Error fetching genres:", error);
    });
}

// Gọi các hàm khi trang đã tải
document.addEventListener("DOMContentLoaded", function () {
  loadMovieDetails();
  loadGenres();
});

function startPayment() {
  fetch("https://localhost:7252/Start", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      plan: "VIP", // Gói thanh toán
      price: 90000, // Số tiền
    }),
  })
    .then((response) => {
      console.log(response);
      return response.json();
    })
    .then((data) => {
      console.log(data);
      if (data.url) {
        window.location.href = data.url;
      } else {
        console.error("Không tìm thấy URL trong phản hồi");
      }
    });
}

document.addEventListener("DOMContentLoaded", function () {
  const likeButton = document.getElementById("like-button");
  const username = localStorage.getItem("username");
  const urlParams = new URLSearchParams(window.location.search);
  const courseId = urlParams.get("courseId");

  // Check like status when page loads
  if (username && courseId) {
    checkLikeStatus();
  }

  // Add event listener for the like button
  likeButton.addEventListener("click", function () {
    if (!username) {
      alert("Vui lòng đăng nhập để thực hiện chức năng này.");
      return;
    }

    toggleLike();
  });

  // Function to toggle like status
  function toggleLike() {
    const authToken = localStorage.getItem("token");

    fetch("https://localhost:7252/api/Watchlist/ToggleLike", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${authToken}`,
      },
      body: JSON.stringify({
        username: username, // Sử dụng username là chuỗi
        courseId: parseInt(courseId), // Đảm bảo courseid là số
      }),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => {
        if (data.liked) {
          likeButton.classList.add("liked");
          alert("Movie added to your likes!");
        } else {
          likeButton.classList.remove("liked");
          alert("Movie removed from your likes.");
        }
      })
      .catch((error) => console.error("Error toggling like status:", error));
  }

  // Function to check initial like status
  function checkLikeStatus() {
    const authToken = localStorage.getItem("token");

    fetch(
      `https://localhost:7252/api/Watchlist/CheckLikeStatus?username=${username}&courseId=${courseId}`,
      {
        headers: {
          Authorization: `Bearer ${authToken}`,
        },
      }
    )
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => {
        if (data.liked) {
          likeButton.classList.add("liked");
        }
      })
      .catch((error) => console.error("Error checking like status:", error));
  }
});
