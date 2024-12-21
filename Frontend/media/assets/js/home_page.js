// Hàm kiểm tra xem người dùng đã đăng nhập chưa
function isLoggedIn() {
  const authToken = localStorage.getItem("authToken");
  return authToken !== null;
}

// Kiểm tra nếu đã đăng nhập và cập nhật dropdown tài khoản
function updateAccountDropdown() {
  const username = localStorage.getItem("username");
  const accountDropdown = document.querySelector(".dropdown2");

  if (accountDropdown) {
    // Kiểm tra phần tử tồn tại
    if (username) {
      accountDropdown.innerHTML = `
        <a href="#" class="dropbtn"><i class="fas fa-user"></i> ${username}</a>
        <div class="dropdown2-content">
          <a href="/TTTN_Course/Frontend/pages/user/user_detail.html" onclick="navigateToUserDetail()" id="userInfoLink">Thông tin cá nhân</a>
          <a href="#" id="changePasswordLink">Đổi mật khẩu</a>
          <a href="#" id="logoutBtn">Đăng xuất</a>
        </div>
      `;

      // Xử lý sự kiện đăng xuất
      document
        .getElementById("logoutBtn")
        .addEventListener("click", function () {
          localStorage.removeItem("authToken");
          localStorage.removeItem("username");
          localStorage.removeItem("token");
          alert("Bạn đã đăng xuất.");
          updateAccountDropdown();
          window.location.href = "/TTTN_Course/Frontend/pages/Login.html";
        });

      // Xử lý sự kiện đổi mật khẩu
      document
        .getElementById("changePasswordLink")
        .addEventListener("click", function () {
          openChangePasswordModal(username);
        });
    } else {
      accountDropdown.innerHTML = `
        <a href="Login.html" class="dropbtn"><i class="fas fa-user"></i> Tài Khoản</a>
      `;
    }
  }
}

// Hàm mở modal đổi mật khẩu
function openChangePasswordModal(username) {
  // Tạo một form yêu cầu nhập mật khẩu cũ và mật khẩu mới
  const modalContent = `
    <div class="modal" id="changePasswordModal">
      <div class="modal-content">
        <span class="close" onclick="closeChangePasswordModal()">&times;</span>
        <h2>Đổi mật khẩu</h2>
        <form id="changePasswordForm">
          <label for="currentPassword">Mật khẩu cũ:</label>
          <input type="password" id="currentPassword" name="currentPassword" required>
          <label for="newPassword">Mật khẩu mới:</label>
          <input type="password" id="newPassword" name="newPassword" required>
          <label for="confirmNewPassword">Xác nhận mật khẩu mới:</label>
          <input type="password" id="confirmNewPassword" name="confirmNewPassword" required>
          <button type="submit">Đổi mật khẩu</button>
        </form>
      </div>
    </div>
  `;

  // Thêm modal vào body
  document.body.insertAdjacentHTML("beforeend", modalContent);

  // Xử lý form đổi mật khẩu
  document
    .getElementById("changePasswordForm")
    .addEventListener("submit", function (event) {
      event.preventDefault();
      const currentPassword = document.getElementById("currentPassword").value;
      const newPassword = document.getElementById("newPassword").value;
      const confirmNewPassword =
        document.getElementById("confirmNewPassword").value;

      // Kiểm tra mật khẩu mới và xác nhận có trùng khớp không
      if (newPassword !== confirmNewPassword) {
        alert("Mật khẩu mới và xác nhận mật khẩu không trùng khớp.");
        return;
      }

      // Gửi yêu cầu đổi mật khẩu
      changePassword(
        username,
        currentPassword,
        newPassword,
        confirmNewPassword
      );
    });

  // Hiển thị modal
  document.getElementById("changePasswordModal").style.display = "block";
}

// Hàm đóng modal đổi mật khẩu
function closeChangePasswordModal() {
  document.getElementById("changePasswordModal").style.display = "none";
  document.body.removeChild(document.getElementById("changePasswordModal"));
}

// Hàm gửi yêu cầu API để đổi mật khẩu
function changePassword(
  username,
  currentPassword,
  newPassword,
  confirmNewPassword
) {
  fetch("https://localhost:7252/api/Auth/ChangePassword", {
    method: "POST",
    headers: {
      Accept: "*/*",
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username: username,
      currentPassword: currentPassword,
      newPassword: newPassword,
      confirmNewPassword: confirmNewPassword,
    }),
  })
    .then((response) => response.json())
    .then((data) => {
      if (data.success) {
        alert("Đổi mật khẩu thành công!");
        closeChangePasswordModal();
      } else {
        alert(" " + data.message);
      }
    })
    .catch((error) => {
      alert("Có lỗi xảy ra, vui lòng thử lại sau.");
      console.error("Error:", error);
    });
}

// Hàm cập nhật dropdown và kiểm tra trạng thái đăng nhập khi trang được tải
$(document).ready(function () {
  updateAccountDropdown();

  // Khởi tạo khi trang được tải
  const moviesPerPage = 10; // Số lượng phim trên mỗi trang
  let currentPage = 1; // Trang hiện tại
  let totalMovies = []; // Mảng lưu trữ tất cả phim

  // Hàm tải danh sách phim từ API theo tiêu đề
  function loadMoviesByTitle(title = "") {
    $.ajax({
      url: `https://localhost:7252/api/Home?movieName=${encodeURIComponent(
        title
      )}`,
      method: "GET",
      success: function (data) {
        totalMovies = data; // Lưu trữ tất cả phim vào mảng
        displayMovies(); // Hiển thị phim cho trang hiện tại
      },
      error: function (error) {
        console.error("Error fetching movies:", error);
        let filmList = $("#film-list");
        filmList.empty(); // Xóa danh sách phim hiện tại
        filmList.append("<p>Error loading movies. Please try again later.</p>");
      },
    });
  }

  // Hàm hiển thị phim cho trang hiện tại
  function displayMovies() {
    let filmList = $("#film-list");
    filmList.empty(); // Xóa danh sách phim hiện tại

    // Tính chỉ số bắt đầu và kết thúc của phim cần hiển thị
    const startIndex = (currentPage - 1) * moviesPerPage;
    const endIndex = startIndex + moviesPerPage;
    const moviesToDisplay = totalMovies.slice(startIndex, endIndex);

    if (moviesToDisplay.length === 0) {
      filmList.append("<p>Không tìm thấy khóa học phù hợp.</p>");
    } else {
      moviesToDisplay.forEach(function (movie) {
        let filmCard = `
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
                <span class="badge bg-success">Võ thuật</span>
                <span class="text-success">${movie.price}</span>
              </div>
              <h5 class="card-title">${movie.title}</h5>
              <p class="card-text">${movie.description}</p>
              <div class="d-flex justify-content-between align-items-center">
                <button class="btn btn-primary" onclick="window.location.href='course_detail.html?courseId=${movie.courseId}'">Enroll Now</button>
                <div>
                  <i class="bi bi-star-fill text-warning"></i>
                  <span>${movie.formattedRating}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
        `;
        filmList.append(filmCard);
      });
      $("html, body").animate({ scrollTop: filmList.offset().top }, 500);
      updatePagination();
    }
  }

  // Hàm cập nhật điều khiển phân trang
  function updatePagination() {
    let pagination = $("#pagination");
    pagination.empty(); // Xóa điều khiển phân trang hiện tại
    const totalPages = Math.ceil(totalMovies.length / moviesPerPage);

    for (let i = 1; i <= totalPages; i++) {
      let pageLink = "";
      if (currentPage === i) {
        pageLink = $(`<a href="#" class="page-link active">${i}</a>`);
      } else {
        pageLink = $(`<a href="#" class="page-link">${i}</a>`);
      }
      // Xử lý sự kiện click
      pageLink.click(function (event) {
        event.preventDefault(); // Ngăn không cho trang web tải lại
        currentPage = i; // Cập nhật trang hiện tại
        displayMovies(); // Hiển thị phim cho trang mới
      });
      pagination.append(pageLink);
    }
  }

  // Tải tất cả các phim khi trang được load lần đầu
  loadMoviesByTitle();

  // Xử lý sự kiện tìm kiếm khi người dùng nhấn nút Search
  $("#search-form").submit(function (event) {
    event.preventDefault(); // Ngăn không cho trang web tải lại
    let title = $("#search-input").val(); // Lấy từ khóa tìm kiếm (title của phim)
    loadMoviesByTitle(title); // Tải danh sách phim dựa trên tiêu đề tìm kiếm
  });

  // Tải danh sách thể loại phim
  $.ajax({
    url: "https://localhost:7252/api/Categori",
    method: "GET",
    success: function (genres) {
      if (Array.isArray(genres)) {
        let genresHtml = genres
          .map(
            (genre) => `
            <a href="course_Genres.html?genreId=${genre.cateId}">${genre.name}</a>
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

  // Lắng nghe sự kiện thay đổi của dropdown
  $("#sortMovies").change(function () {
    const selectedValue = $(this).val();
    sortMovies(selectedValue); // Sắp xếp phim theo tiêu chí đã chọn
  });

  // Hàm sắp xếp phim theo tên, năm, hoặc rating
  function sortMovies(criteria) {
    if (criteria === "alpha") {
      totalMovies.sort((a, b) => a.title.localeCompare(b.title));
    } else if (criteria === "year") {
      totalMovies.sort(
        (a, b) => new Date(a.release_Date) - new Date(b.release_Date)
      );
    } else if (criteria === "rating") {
      // Sắp xếp phim theo trọng số rating
      sortMoviesByWeightedRating(totalMovies);
    }
    displayMovies();
  }

  function calculateWeightedRating(movie, totalRatings) {
    const R = movie.formattedRating;
    const v = totalRatings;

    // Tính trọng số WR
    const weightedRating = R * v;
    return weightedRating;
  }

  // Hàm để sắp xếp phim theo trọng số rating
  function sortMoviesByWeightedRating(totalMovies) {
    const moviePromises = totalMovies.map((movie) => {
      return fetch(
        `https://localhost:7252/api/Rating/MovieRatings/${movie.courseId}`
      )
        .then((response) => {
          if (!response.ok) {
            throw new Error("Không có dữ liệu đánh giá khóa học.");
          }
          return response.json();
        })
        .then((ratings) => {
          const totalRatings = ratings.length;
          const weightedRating = calculateWeightedRating(movie, totalRatings);
          movie.weightedRating = weightedRating;
        })
        .catch((error) => {
          console.error(
            `Lỗi khi lấy dữ liệu đánh giá cho phim ID ${movie.courseId}:`,
            error
          );
        });
    });

    Promise.all(moviePromises).then(() => {
      // Sắp xếp danh sách phim dựa trên trọng số rating
      totalMovies.sort((a, b) => b.weightedRating - a.weightedRating);
      displayMovies();
    });
  }
});
