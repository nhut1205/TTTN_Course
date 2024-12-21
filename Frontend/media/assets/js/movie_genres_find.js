$(document).ready(function () {
  // Lấy genreId từ URL
  const params = new URLSearchParams(window.location.search);
  const genreId = params.get("genreId");

  if (genreId) {
    // Gọi API lấy danh sách phim theo thể loại
    $.ajax({
      url: `https://localhost:7252/api/Categori/${genreId}`,
      method: "GET",
      success: function (response) {
        console.log(response); // Kiểm tra dữ liệu trả về từ API

        let movieList = $("#genre-movie-list");
        movieList.empty();

        // Kiểm tra xem response có chứa các phim trong thể loại
        if (
          response &&
          Array.isArray(response) &&
          response.length > 0 &&
          response[0].course
        ) {
          let genreName = response[0].cate.name || "Unknown Genre";
          $("h2").text(`Thể loại: ${genreName}`);

          let totalMovies = 0;

          // Lặp qua từng đối tượng trong response
          response.forEach(function (movieItem) {
            if (movieItem.course) {
              let movie = movieItem.course; // Lấy thông tin phim từ course
              // Tạo movie card cho mỗi bộ phim
              let movieCard = `
             <div class="col-md-4">
                  <div class="card h-100">
                    <img
                      src="${movie.thumbnailUrl}"
                      class="card-img-top"
                      alt="UI/UX Design"
                      onerror="this.src='${movie.title}'"
                    />
                    <div class="card-body">
                      <div
                        class="d-flex justify-content-between align-items-center mb-2"
                      >
                        <span class="badge bg-success">Design</span>
                        <span class="text-success">${movie.price}</span>
                      </div>
                      <h5 class="card-title">${movie.title}</h5>
                      <p class="card-text">${movie.description}</p>
                      <div class="d-flex justify-content-between align-items-center">
                        <button class="btn btn-primary" onclick="window.location.href='film_detail.html?courseId=${movie.courseId}'">Enroll Now</button>
                        <div>
                          <i class="bi bi-star-fill text-warning"></i>
                           <span>${movie.formattedRating}</span>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              `;
              movieList.append(movieCard);
              totalMovies++; // Tăng tổng số phim
            }
          });

          // Cập nhật số lượng phim vào phần tử count
          $("#count").text(`Hiển thị ${totalMovies} kết quả.`);
        } else {
          movieList.append("<p>Không có phim nào thuộc thể loại này.</p>");
        }
      },
      error: function (error) {
        console.error("Error fetching movies by genre:", error);
        $("#genre-movie-list").append(
          "<p>Error loading movies. Please try again later.</p>"
        );
      },
    });
  } else {
    $("#genre-movie-list").append("<p>No genre selected.</p>");
  }
});

$(document).ready(function () {
  // Gọi API lấy danh sách thể loại
  $.ajax({
    url: "https://localhost:7252/api/Genre",
    method: "GET",
    success: function (genres) {
      if (Array.isArray(genres)) {
        let genresHtml = genres
          .map(
            (genre) => `
                <a href="course_Genres.html?genreId=${genre.genreId}">${genre.name}</a>
              `
          )
          .join("");
        $("#dropdown-content-genres").html(genresHtml);
      } else {
        console.error("Dữ liệu trả về không phải là mảng:", genres);
      }
    },
    error: function (error) {
      console.error("Error fetching genres:", error);
    },
  });
});
