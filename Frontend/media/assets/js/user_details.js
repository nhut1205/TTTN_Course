//load danh sách rating film
const apiBaseUrl = "https://localhost:7252";
const username = localStorage.getItem("username");
///////////////////////////////////////////////////////////////////////////////////////////////
//INFO
// Lấy thông tin người dùng khi trang tải
window.onload = async function () {
  const username = localStorage.getItem("username");
  if (username) {
    try {
      const response = await fetch(
        `${apiBaseUrl}/api/User/detail/${username}`,
        {
          method: "GET",
          headers: {
            Authorization: `Bearer ${localStorage.getItem("authToken")}`,
            "Content-Type": "application/json",
          },
        }
      );

      if (response.ok) {
        const userDetails = await response.json();
        document.getElementById("fullName").textContent =
          userDetails.fullName || "";
        document.getElementById("gender").textContent =
          userDetails.gender || "";
        document.getElementById("dob").textContent =
          userDetails.dateOfBirth || "";
        document.getElementById("phoneNumber").textContent =
          userDetails.phoneNumber || "";
        document.getElementById("address").textContent =
          userDetails.address || "";

        // Hiển thị ảnh với đường dẫn đầy đủ
        document.getElementById("avatar").src = userDetails.avatarUrl
          ? `${apiBaseUrl}/${userDetails.avatarUrl}`
          : `${apiBaseUrl}/default-avatar.png`;

        // Cập nhật form với thông tin người dùng
        document.getElementById("fullNameInput").value =
          userDetails.fullName || "";
        document.getElementById("genderInput").value = userDetails.gender || "";
        document.getElementById("dobInput").value =
          userDetails.dateOfBirth || "";
        document.getElementById("phoneNumberInput").value =
          userDetails.phoneNumber || "";
        document.getElementById("addressInput").value =
          userDetails.address || "";
      } else {
        alert("Không thể tải thông tin người dùng.");
      }
    } catch (error) {
      console.error("Error fetching user details:", error);
    }
  } else {
    window.location.href = "login.html";
  }

  ///////////////////////////////////////////////////////////////////////////////////////////////
  //DATA
  if (username) {
    try {
      // Lấy dữ liệu đánh giá của người dùng
      const ratingsResponse = await fetch(
        `${apiBaseUrl}/api/Rating/UserRatings/${username}`
      );
      if (ratingsResponse.ok) {
        const ratedMovies = await ratingsResponse.json();
        const ratedMoviesList = document.getElementById("ratedMoviesList");
        ratedMoviesList.innerHTML = "";

        // Lấy thông tin chi tiết từng phim và hiển thị
        for (const movie of ratedMovies) {
          const movieResponse = await fetch(
            `https://localhost:7252/api/Home/${movie.courseId}`
          );
          if (movieResponse.ok) {
            const data = await movieResponse.json();
            // const rate = await ratingsResponse.json();
            const li = document.createElement("li");
            li.innerHTML = `
              <div class="card_film" onclick="window.location.href='../film_detail.html?courseId=${data.courseId}'">
                <img src="${data.thumbnailUrl}" alt="${data.title}" />
                <h3 style="color: #666;">${data.title}</h3>
                <p>Tổng đánh giá : ${data.rating}</p>
              </div>  
            `;
            ratedMoviesList.appendChild(li);
          } else {
            console.error(
              `Không thể lấy chi tiết phim cho movieId: ${movie.courseId}`
            );
          }
        }
      } else {
        console.error("Không thể lấy danh sách đánh giá phim của người dùng");
      }

      // Lấy dữ liệu watchlist của người dùng
      const watchlistResponse = await fetch(
        `${apiBaseUrl}/api/Watchlist/UserWatchlist/${username}`
      );
      if (watchlistResponse.ok) {
        const watchlistMovies = await watchlistResponse.json();
        const watchlistMoviesList = document.getElementById(
          "watchlistMoviesList"
        );
        watchlistMoviesList.innerHTML = "";

        // Lấy thông tin chi tiết từng phim và hiển thị
        for (const movie of watchlistMovies) {
          const movieResponse = await fetch(
            `https://localhost:7252/api/Home/${movie.courseId}`
          );
          if (movieResponse.ok) {
            // Đổi múi giờ
            const addedAt = new Date(movie.addedAt);
            addedAt.setHours(addedAt.getHours() + 7);

            const data = await movieResponse.json();
            const li = document.createElement("li");
            li.innerHTML = `
              <div class="card_film" id="user_card_film" onclick="window.location.href='../film_detail.html?courseId=${
                movie.courseId
              }'">
                <img src="${data.thumbnailUrl}" alt="${data.title}" />
                <h3 style="color: #666;">${data.title}</h3>

                <p>Thêm vào lúc: ${addedAt.toLocaleString()}</p>
              `;
            watchlistMoviesList.appendChild(li);
          } else {
            console.error(
              `Không thể lấy chi tiết phim cho movieId: ${movie.courseId}`
            );
          }
        }
      } else {
        console.error("Không thể lấy đánh giá của người dùng");
      }
    } catch (error) {
      console.error("Lỗi khi lấy dữ liệu:", error);
    }
  }
};
