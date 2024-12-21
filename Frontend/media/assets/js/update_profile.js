// Xử lý sự kiện submit form cập nhật thông tin
document.getElementById("updateForm").onsubmit = async function (event) {
  event.preventDefault();
  const formData = new FormData();
  formData.append("fullName", document.getElementById("fullNameInput").value);
  formData.append("dateOfBirth", document.getElementById("dobInput").value);
  formData.append("gender", document.getElementById("genderInput").value);
  formData.append(
    "phoneNumber",
    document.getElementById("phoneNumberInput").value
  );
  formData.append("address", document.getElementById("addressInput").value);

  const avatarFile = document.getElementById("avatarFile").files[0];
  if (avatarFile) {
    formData.append("avatarFile", avatarFile);
  }

  try {
    const response = await fetch(`${apiBaseUrl}/api/User/${username}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      body: formData,
    });
    if (response.ok) {
      alert("Cập nhật thông tin thành công!");
      window.location.reload();
    } else {
      alert("Cập nhật thông tin không thành công.");
    }
  } catch (error) {
    console.log(username);
    console.error("Error updating user info:", error);
  }
};
