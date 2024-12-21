// Khai báo các biến phân trang
let currentPage = 1;
const moviesPerPage = 5; // Số lượng phim mỗi trang
let totalMovies = [];

// Lấy thông tin teacherId từ URL
const urlParams = new URLSearchParams(window.location.search);
const teacherId = urlParams.get("teacherId");
const actorName = urlParams.get("actorName");
const actorAvatar = urlParams.get("actorAvatar");

// Hiển thị tên và avatar của diễn viên
document.getElementById("actor-name").textContent =
  decodeURIComponent(actorName);
document.getElementById("actor-avatar").src = decodeURIComponent(actorAvatar);

// Gọi API để lấy danh sách phim của diễn viên
fetch(`https://localhost:7252/api/Teachers/${teacherId}/movies`)
  .then((response) => response.json())
  .then((movies) => {
    if (movies.length === 0) {
      document.getElementById("actor-movie-list").innerHTML =
        "<p>No movies found for this actor.</p>";
      return;
    }

    // Lưu danh sách phim để phân trang
    totalMovies = movies;

    // Hiển thị phim trang đầu tiên
    displayMovies();
  })
  .catch((error) => {
    console.error("Error fetching movies for actor:", error);
    document.getElementById("actor-movie-list").innerHTML =
      "<p>Error loading movies.</p>";
  });

// Hàm hiển thị danh sách phim theo trang
function displayMovies() {
  let filmList = $("#actor-movie-list");
  filmList.empty(); // Xóa danh sách phim hiện tại

  // Tính chỉ số bắt đầu và kết thúc của phim cần hiển thị
  const startIndex = (currentPage - 1) * moviesPerPage;
  const endIndex = startIndex + moviesPerPage;
  const moviesToDisplay = totalMovies.slice(startIndex, endIndex);

  if (moviesToDisplay.length === 0) {
    filmList.append("<p>No movies found</p>");
  } else {
    moviesToDisplay.forEach(function (movie) {
      let filmCard = `
        <div class="card_film" onclick="window.location.href='film_detail.html?courseId=${movie.courseId}'">
          <img src="${movie.thumbnailUrl}" alt="${movie.title}" />
          <h3 style="color: #666;">${movie.title}</h3>
          <p>${movie.language}, ${movie.duration} min</p>
        </div>
      `;
      filmList.append(filmCard);
    });
    $("html, body").animate({ scrollTop: filmList.offset().top }, 1000);
    updatePagination(); // Cập nhật phân trang sau khi hiển thị phim
  }
}

// Hàm cập nhật giao diện phân trang
function updatePagination() {
  let pagination = $("#pagination");
  pagination.empty();

  const totalPages = Math.ceil(totalMovies.length / moviesPerPage);

  for (let i = 1; i <= totalPages; i++) {
    let pageButton = `<button onclick="changePage(${i})">${i}</button>`;
    pagination.append(pageButton);
  }
}

// Hàm thay đổi trang
function changePage(page) {
  currentPage = page;
  displayMovies();
}

// Sự kiện tìm kiếm phim theo tiêu đề
$("#search-form").submit(function (event) {
  event.preventDefault(); // Ngăn không cho trang web tải lại
  let title = $("#search-input").val(); // Lấy từ khóa tìm kiếm (title của phim)
  loadMoviesByTitle(title); // Tải danh sách phim dựa trên tiêu đề tìm kiếm
});

// Hàm để tìm kiếm phim theo tiêu đề
function loadMoviesByTitle(title) {
  if (title.trim() === "") {
    // Nếu ô tìm kiếm trống, hiển thị lại tất cả phim
    totalMovies = [...totalMovies]; // Khôi phục lại danh sách phim gốc
    currentPage = 1; // Reset về trang đầu tiên khi tìm kiếm mới
    displayMovies(); // Hiển thị lại toàn bộ phim
    return;
  }

  // Lọc danh sách phim dựa trên tiêu đề tìm kiếm
  const filteredMovies = totalMovies.filter((movie) =>
    movie.title.toLowerCase().includes(title.toLowerCase())
  );

  if (filteredMovies.length === 0) {
    document.getElementById("actor-movie-list").innerHTML =
      "<p>No movies found with this title.</p>";
    return;
  }

  // Lưu danh sách phim và hiển thị kết quả
  totalMovies = filteredMovies; // Cập nhật danh sách phim
  currentPage = 1; // Reset về trang đầu tiên khi tìm kiếm mới
  displayMovies(); // Hiển thị phim đã lọc
}
