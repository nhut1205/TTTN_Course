document
  .getElementById("backupBtn")
  .addEventListener("click", async function (event) {
    event.preventDefault();

    try {
      // Gửi yêu cầu backup
      const response = await fetch("https://localhost:7252/api/Backup/backup", {
        method: "POST",
        headers: {
          Accept: "*/*",
          "Content-Type": "application/json",
        },
      });

      // Xử lý kết quả từ API
      if (response.ok) {
        const result = await response.json();
        document.getElementById("backupMessage").innerHTML = `
          <strong>${result.message}</strong><br>
          <a href="${result.filePath}" target="_blank">Tải xuống file backup</a>
        `;
      } else {
        const errorResult = await response.json();
        document.getElementById("backupMessage").innerHTML = `
          <strong style="color: red;">Lỗi: ${errorResult.message}</strong>
        `;
      }
    } catch (error) {
      document.getElementById(
        "backupMessage"
      ).innerHTML = `<strong style="color: red;">Có lỗi xảy ra: ${error.message}</strong>`;
    }
  });

//Hàm restore
document
  .getElementById("restoreBtn")
  .addEventListener("click", async function (event) {
    event.preventDefault();
    const restoreFile = document.getElementById("restoreFile").files[0];

    if (!restoreFile) {
      document.getElementById("restoreMessage").innerHTML =
        "Vui lòng chọn tệp sao lưu.";
      return;
    }

    const formData = new FormData();
    formData.append("backupFile", restoreFile);

    try {
      const response = await fetch(
        "https://localhost:7252/api/Backup/restore",
        {
          method: "POST",
          body: formData,
          headers: {
            Accept: "*/*",
          },
        }
      );

      const result = await response.json();
      document.getElementById("restoreMessage").innerHTML = `
            <strong>${result.message}</strong><br>
            <a href="${result.filePath}" target="_blank">Tải xuống file đã phục hồi</a>
        `;
    } catch (error) {
      document.getElementById(
        "restoreMessage"
      ).innerHTML = `Có lỗi xảy ra: ${error.message}`;
    }
  });

async function loadBackupFiles() {
  try {
    const response = await fetch(
      "https://localhost:7252/api/Backup/list-backups"
    );
    const files = await response.json();

    if (files.length === 0) {
      document.getElementById("backupTable").innerHTML =
        "Không có file backup nào.";
      return;
    }

    let tableHTML = `
        <table border="1" cellspacing="0" cellpadding="5">
          <thead>
            <tr>
              <th>Tên File</th>
              <th>Ngày Tạo</th>
            </tr>
          </thead>
          <tbody>
      `;

    files.forEach((file) => {
      tableHTML += `
          <tr>
            <td>${file.fileName}</td>
            <td>${file.createdAt}</td>
          </tr>
        `;
    });

    tableHTML += `
          </tbody>
        </table>
      `;

    document.getElementById("backupTable").innerHTML = tableHTML;
  } catch (error) {
    document.getElementById(
      "backupTable"
    ).innerHTML = `Có lỗi xảy ra: ${error.message}`;
  }
}

// Gọi hàm khi tải trang
window.onload = loadBackupFiles;
