$(document).ready(function () {
  // Lấy danh sách thể loại
  $.ajax({
    url: "https://localhost:7252/api/Categori",
    method: "GET",
    success: function (genres) {
      if (Array.isArray(genres)) {
        let genresHtml = genres
          .map(
            (genre) =>
              `<a href="course_Genres.html?genreId=${genre.genreId}">${genre.name}</a>`
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

  // Lấy thông tin phim theo courseId từ URL
  const urlParams = new URLSearchParams(window.location.search);
  const courseId = urlParams.get("courseId");

  if (courseId) {
    // Lấy thông tin phim
    $.ajax({
      url: `https://localhost:7252/api/Home/${courseId}`,
      method: "GET",
      success: function (data) {
        const firstEpisodeUrl =
          data.lessons.length > 0 ? data.lessons[0].videoUrl : "";

        let filmDetailsHtml = `
          <div class="row video-section">
             <div class="col-8">
              <h1>${data.title}</h1>
               <p>Năm sản xuất: ${data.release_Date}</p>
              <p>Ngôn ngữ: ${data.language}</p>
              <p>Ngày cập nhật: ${data.created_At}</p><iframe
                src="${firstEpisodeUrl}"
                width="800"
                height="533"
                allowfullscreen
                frameborder="0"
                id="videoPlayer"></iframe>
              <br><br>
              <h5>Danh sách tập phim:</h5>
              <div class="movie-list" id="episode-list"></div>

              <br>
              <div class="rating" data-movie-id="${courseId}">
                <h5>Đánh giá:</h5>
                <span class="star" data-value="1">&#9733;</span>
                <span class="star" data-value="2">&#9733;</span>
                <span class="star" data-value="3">&#9733;</span>
                <span class="star" data-value="4">&#9733;</span>
                <span class="star" data-value="5">&#9733;</span>
              </div>
              
              <div class="comment-section">
                <h5>Bình luận về phim:</h5>
                <div class="comment-box">
                  <textarea id="commentInput" rows="3" placeholder="Nhập bình luận của bạn..."></textarea>
                  <button id="submitComment">Gửi bình luận</button>
                </div>
                <div id="commentList">
                  <div class="comment-list" id="commentList-sm"></div>
                </div>
              </div>
            </div>
            <div class="col-4">
              <div class="payment">
                <h3>Nâng cấp VIP</h3>
                <stripe-buy-button
                  buy-button-id="buy_btn_1QOBjJ2NGY4X6iQWzXxYfFe5"
                  publishable-key="pk_test_51QCVud2NGY4X6iQWhhO3rVJl3Gi2zxRNCGa79w3E1dVSExvObSNAvt8IQJjdNya5FsXOIikYPlDxzSnLwPvpcGje00d1jQknKr"
                >
                </stripe-buy-button>
              </div>
              <h3 style="text-align: center">Khóa học nổi bật</h3>
              <br>
              <div id="hot-movies-list" class="grid_container_watch"></div>  
            </div>
            
          </div>
        `;
        $("#film-details").html(filmDetailsHtml);

        showEpisode(courseId);

        // Lấy thông tin và hiển thị tập phim khi người dùng có username
        function showEpisode(courseId) {
          const username = localStorage.getItem("username");

          // Kiểm tra nếu không có username
          if (!username) {
            return;
          }

          // Gửi yêu cầu AJAX để lấy dữ liệu tập phim
          $.ajax({
            url: `https://localhost:7252/api/CheckSub/episodes/${courseId}?username=${username}`,
            method: "GET",
            success: function (data) {
              console.log(data); // In ra dữ liệu trả về để kiểm tra

              // Kiểm tra nếu có dữ liệu và trường episodes tồn tại
              if (
                data &&
                data.episodes &&
                Array.isArray(data.episodes) &&
                data.episodes.length > 0
              ) {
                let episodeListHtml = "";

                // Duyệt qua từng tập phim
                data.episodes.forEach(function (episode) {
                  if (episode.isAccessible) {
                    episodeListHtml += `
                  <button class="btn_episode" data-video-url="${episode.videoUrl}">${episode.episodeNumber}</button>
                `;
                  } else {
                    episodeListHtml += `
                  <button class="btn_episode disabled" data-video-url="">${episode.episodeNumber}</button>
                `;
                  }
                });

                // Cập nhật phần tử HTML với id "episode-list" bằng HTML vừa tạo
                $("#episode-list").html(episodeListHtml);
              } else {
                $("#episode-list").html("<p>Không có tập phim nào.</p>");
              }
            },
            error: function (error) {
              console.error("Error fetching episodes:", error);
              $("#episode-list").html(
                "<p>Đã có lỗi xảy ra khi tải danh sách tập phim.</p>"
              );
            },
          });
        }

        $(document).on("click", ".btn_episode", function () {
          const videoUrl = $(this).data("video-url");

          $("#videoPlayer").attr("src", `${videoUrl}?autoplay=1&q=480`);
        });
        // Tải bình luận
        loadComments(courseId);

        // Gọi hàm để kiểm tra đánh giá khi trang được tải
        $(document).ready(function () {
          $(".rating").each(function () {
            const courseId = $(this).data("movie-id");
            checkUserRating(courseId);
            showEpisode(courseId, username);
          });
        });
        $("#submitComment").click(function () {
          const commentContent = $("#commentInput").val();
          const username = localStorage.getItem("username");

          if (commentContent) {
            $.ajax({
              url: `https://localhost:7252/api/Comments`,
              method: "POST",
              contentType: "application/json",
              data: JSON.stringify({
                username: username, // Lấy từ localStorage
                movieId: courseId, // Đảm bảo courseId là số nguyên
                content: commentContent,
              }),
              success: function () {
                // Tải lại bình luận sau khi gửi thành công
                loadComments(courseId);
                $("#commentInput").val(""); // Xóa nội dung nhập
              },
              error: function () {
                alert("Gửi bình luận không thành công.");
              },
            });
          } else {
            alert("Vui lòng nhập bình luận.");
          }
        });
      },
      error: function (error) {
        console.error("Error fetching movie details:", error);
      },
    });
  } else {
    console.error("courseId không có trong URL.");
  }

  // Hàm kiểm tra đánh giá người dùng
  function checkUserRating(courseId) {
    const username = localStorage.getItem("username");
    console.log("Username from localStorage:", username);
    console.log("Movie ID:", courseId);

    if (username && courseId) {
      $.ajax({
        url: `https://localhost:7252/api/Rating/CheckRating?courseId=${courseId}&username=${username}`,
        method: "GET",
        success: function (ratingData) {
          console.log("Rating data từ API:", ratingData);
          if (ratingData && ratingData.rating) {
            const selectedRating = ratingData.rating;
            console.log("Selected rating:", selectedRating);

            $(".rating").each(function () {
              if ($(this).data("movie-id") === courseId) {
                // Reset tất cả sao về trạng thái trống
                $(this)
                  .find(".star")
                  .each(function () {
                    $(this).html("&#9734;"); // Sao trống
                  });

                // Cập nhật các sao dựa trên đánh giá
                $(this)
                  .find(".star")
                  .each(function () {
                    if ($(this).data("value") <= selectedRating) {
                      $(this).html("&#9733;"); // Sao đầy
                    }
                  });

                // Vô hiệu hóa click để tránh thay đổi sau khi tải
                $(this).find(".star").off("click");
              }
            });
          } else {
            console.log(
              "Không tìm thấy rating hoặc dữ liệu không đúng định dạng."
            );
          }
        },
        error: function (error) {
          console.error("Error fetching rating:", error);
        },
      });
    } else {
      console.log("Thiếu thông tin username hoặc courseId.");
    }
  }
});

// Gọi hàm để kiểm tra đánh giá khi trang được tải và tải phim hot
$(document).ready(function () {
  $(".rating").each(function () {
    const courseId = $(this).data("movie-id");
    checkUserRating(courseId);
  });
  showHotFilm();
});

// Hiển thị phim hot
function showHotFilm() {
  $.ajax({
    url: "https://localhost:7252/api/Home",
    method: "GET",
    success: function (movies) {
      if (Array.isArray(movies)) {
        movies.sort((a, b) => new Date(b.created_At) - new Date(a.created_At));
        const hotMovies = movies.slice(0, 6);
        let hotMoviesHtml = hotMovies
          .map(
            (movie) =>
              `<div class="card_film_watch" onclick="window.location.href='course_detail.html?courseId=${movie.courseId}'">
                <img src="${movie.thumbnailUrl}" alt="${movie.title}" />
                <h3 style="color: #666;">${movie.title}</h3>
                <p>${movie.language}, ${movie.duration} min</p>
              </div>`
          )
          .join("");
        $("#hot-movies-list").html(hotMoviesHtml);
      } else {
        console.error("Dữ liệu trả về không phải là mảng:", movies);
      }
    },
    error: function (error) {
      console.error("Error fetching hot movies:", error);
    },
  });
}

// Xử lý sự kiện nút "like"
const likeButton = document.getElementById("like-button");

if (likeButton) {
  const username = localStorage.getItem("username");
  const authToken = localStorage.getItem("authToken");

  if (username && courseId && authToken) {
    checkLikeStatus();
  } else {
    console.log("User not logged in or missing movie ID.");
  }

  likeButton.addEventListener("click", function () {
    if (!username || !authToken) {
      alert("Please log in to like this movie.");
      return;
    }
    toggleLike();
  });

  function toggleLike() {
    fetch("https://localhost:7252/api/Watchlist/Add", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${authToken}`,
      },
      body: JSON.stringify({ username, courseId: parseInt(courseId) }),
    })
      .then((response) => {
        if (response.ok) {
          likeButton.classList.add("liked");
          alert("khóa học đã được thêm vào watchlist!");
        } else {
          likeButton.classList.remove("liked");
          alert("Có lỗi xảy ra khi thêm vào watchlist.");
        }
      })
      .catch((error) => console.error("Lỗi khi thêm vào watchlist:", error));
  }

  function checkLikeStatus() {
    fetch(
      `https://localhost:7252/api/Watchlist/CheckLikeStatus?username=${username}&courseId=${courseId}`,
      {
        headers: {
          Authorization: `Bearer ${authToken}`,
        },
      }
    )
      .then((response) => response.json())
      .then((data) => {
        if (data.liked) {
          likeButton.classList.add("liked");
        }
      })
      .catch((error) => console.error("Error checking like status:", error));
  }

  // Xử lý sự kiện cho liên kết "Watch Now"
  if (courseId && username) {
    document
      .getElementById("watch-now-link")
      .addEventListener("click", function (event) {
        event.preventDefault();

        // Tạo đối tượng request
        const requestData = {
          courseId: parseInt(courseId),
          username: username,
          startTime: new Date().toISOString(), // Lấy thời gian hiện tại
        };

        // Gửi yêu cầu POST tới API để thêm lịch sử xem phim
        fetch("https://localhost:7252/api/History/AddHistory", {
          method: "POST",
          headers: {
            Accept: "*/*",
            "Content-Type": "application/json",
          },
          body: JSON.stringify(requestData),
        })
          .then((response) => response.json())
          .then((data) => {
            // Xử lý phản hồi từ API
            console.log("History added or updated:", data); // In dữ liệu trả về
            if (
              data &&
              data.message === "History added or updated successfully."
            ) {
              window.location.href = this.href;
            }
          })

          .catch((error) => {
            // Xử lý lỗi nếu có
            console.error("Error adding history:", error);
          });
      });
  }
}

// Xử lý sự kiện cho sao đánh giá
let selectedRating = 0;

$(document).on("click", ".rating .star", function () {
  if (selectedRating > 0) return; // Nếu đã chọn đánh giá, không cho phép nhấp lại

  selectedRating = $(this).data("value");

  // Cập nhật hiển thị sao
  $(".star").each(function () {
    if ($(this).data("value") <= selectedRating) {
      $(this).html("&#9733;"); // Sao đầy
    } else {
      $(this).html("&#9734;"); // Sao rỗng
    }
  });

  // Gửi đánh giá
  const courseId = $(".rating").data("movie-id");
  const ratingData = {
    courseId: courseId,
    username: localStorage.getItem("username") || "unknown_user",
    rating: selectedRating,
  };

  $.ajax({
    url: "https://localhost:7252/api/Rating",
    method: "POST",
    contentType: "application/json",
    data: JSON.stringify(ratingData),
    success: function (response) {
      alert(response.message);
    },
    error: function (error) {
      console.error("Error adding rating:", error);
      alert("Vui lòng đăng nhập để đánh giá.");
    },
  });
});

// Hàm lấy username từ local storage
function getUsernameFromLocalStorage() {
  return localStorage.getItem("username");
}

let currentCommentCount = 5; // Số bình luận hiện tại đang hiển thị
// Hàm tải bình luận từ server
function loadComments(courseId) {
  $.ajax({
    url: `https://localhost:7252/api/Comments/movie/${courseId}`,
    method: "GET",
    success: function (comments) {
      // $("#commentList").empty(); // Xóa danh sách bình luận hiện tại
      displayComments(comments); // Hiển thị bình luận ban đầu
      // Thêm nút "Tải thêm" nếu có nhiều bình luận hơn
      if (comments.length > currentCommentCount) {
        $("#commentList").append(
          '<button class="btn btn-secondary" id="loadMoreBtn">Tải thêm</button>'
        );
        $("#loadMoreBtn").on("click", function () {
          loadMoreComments(comments); // Tải thêm bình luận
        });
      }
    },
    error: function () {
      alert("Không thể tải bình luận.");
    },
  });
}

// Hàm hiển thị bình luận
function displayComments(comments) {
  // Lấy bình luận để hiển thị
  const commentsToShow = comments.slice(0, currentCommentCount);
  commentsToShow.forEach(function (comment) {
    const username = getUsernameFromLocalStorage();
    const isOwner = username === comment.username; // Kiểm tra xem người dùng có phải là chủ sở hữu bình luận hay không

    const commentDiv = $(
      `<div id="comment-${comment.id}">${comment.fullName}: ${comment.content}</div>`
    ); // Hiển thị bình luận
    $("#commentList-sm").append(commentDiv);
  });
}

// Hàm tải thêm bình luận
function loadMoreComments(comments) {
  const nextComments = comments.slice(
    currentCommentCount,
    currentCommentCount + 5
  ); // Lấy 5 bình luận tiếp theo
  nextComments.forEach(function (comment) {
    const username = getUsernameFromLocalStorage();
    const isOwner = username === comment.username;

    const commentDiv = $(
      `<div id="comment-${comment.id}">${comment.fullName}: ${comment.content}</div>`
    ); // Hiển thị bình luận
    $("#commentList-sm").append(commentDiv);
  });
  currentCommentCount += nextComments.length; // Tăng số lượng bình luận hiện tại

  // Ẩn nút "Tải thêm" nếu không còn bình luận nào để hiển thị
  if (currentCommentCount >= comments.length) {
    $("#loadMoreBtn").remove(); // Xóa nút nếu đã tải hết bình luận
  }
}

// Hàm xóa bình luận
function deleteComment(commentId) {
  $.ajax({
    url: `https://localhost:7252/api/Comments/${commentId}`,
    method: "DELETE",
    success: function () {
      alert("Bình luận đã được xóa.");
      $(`#comment-${commentId}`).remove(); // Xóa bình luận khỏi danh sách hiển thị
      loadComments(currentcourseId); // Tải lại bình luận
    },
    error: function () {
      alert("Không thể xóa bình luận.");
    },
  });
}

loadComments(currentcourseId); // Tải bình luận khi trang được tải
