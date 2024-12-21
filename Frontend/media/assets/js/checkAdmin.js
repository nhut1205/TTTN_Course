function checkAdmin() {
  fetch("https://localhost:7252/api/admin/CheckAdmin", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${authToken}`,
    },
    body: JSON.stringify({ token: authToken }),
  })
    .then((response) => {
      if (response.ok) {
        likeButton.classList.add("admin");
        window.location.href = "/admin/admin_crud.html"; // Điều hướng về trang CRUD
      } else {
        likeButton.classList.remove("admin");
        window.location.href = "/pages/home_page.html"; // Điều hướng về trang chủ
      }
    })
    .catch((error) => console.error("Lỗi khi kiểm tra quyền admin:", error));
}
