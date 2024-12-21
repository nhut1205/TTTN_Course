// Xử lý đăng nhập
document
  .getElementById("loginForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault(); // Ngăn chặn reload trang

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    const data = { userName: username, password: password };

    try {
      const response = await fetch("https://localhost:7252/api/Auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "*/*",
        },
        body: JSON.stringify(data),
      });

      if (response.ok) {
        const result = await response.json();
        localStorage.setItem("authToken", result.token);
        localStorage.setItem("username", result.username);

        alert("Đăng nhập thành công");
        updateAccountDropdown(); // Cập nhật giao diện tài khoản
        window.location.href = "course_page.html"; // Điều hướng về trang chủ sau khi đăng nhập
      } else {
        const errorData = await response.json();
        alert("Đăng nhập không thành công: " + errorData.message);
      }
    } catch (error) {
      console.error("Error:", error);
      alert("Đã xảy ra lỗi khi đăng nhập. Vui lòng thử lại.");
    }
  });

updateAccountDropdown();
