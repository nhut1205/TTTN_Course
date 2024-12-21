using CNTT36_WebXemPhim.DTO.Admin;
using CNTT36_WebXemPhim.Repository;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNTT36_WebXemPhim.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly IDatabaseBackupRepository _backupRepository;
        private readonly IWebHostEnvironment _environment;

        public BackupController(IDatabaseBackupRepository backupRepository, IWebHostEnvironment environment)
        {
            _backupRepository = backupRepository;
            _environment = environment;
        }

        [HttpPost("backup")]
        public async Task<IActionResult> BackupDatabase()
        {
            try
            {
                // Xác định đường dẫn tuyệt đối đến thư mục Backups
                var backupDirectory = Path.Combine(_environment.WebRootPath, "Backups");

                // Log đường dẫn thư mục backup
                Console.WriteLine($"Backup directory: {backupDirectory}");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(backupDirectory))
                {
                    Directory.CreateDirectory(backupDirectory);
                    Console.WriteLine("Created backup directory.");
                }

                // Tạo tên tệp backup với timestamp
                var fileName = $"Backup_{DateTime.Now:yyyyMMddHHmmss}.bak";
                var backupPath = Path.Combine(backupDirectory, fileName);

                // Log đường dẫn tệp backup
                Console.WriteLine($"Backup file path: {backupPath}");

                // Gọi repository để thực hiện backup
                await _backupRepository.BackupDatabaseAsync(backupPath);

                return Ok(new
                {
                    message = "Database backup completed successfully.",
                    filePath = $"/Backups/{fileName}" // Đường dẫn file để tải trong frontend
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Backup error: {ex.Message}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("restore")]
        public async Task<IActionResult> RestoreDatabase(IFormFile backupFile)
        {
            try
            {
                if (backupFile == null || backupFile.Length == 0)
                {
                    return BadRequest("No file selected for restore.");
                }

                // Xác định đường dẫn tuyệt đối đến thư mục Restore
                var restoreDirectory = Path.Combine(_environment.WebRootPath, "Restores");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(restoreDirectory))
                {
                    Directory.CreateDirectory(restoreDirectory);
                }

                // Tạo tên tệp restore với timestamp
                var fileName = $"Restore_{DateTime.Now:yyyyMMddHHmmss}_{backupFile.FileName}";
                var restorePath = Path.Combine(restoreDirectory, fileName);

                // Lưu file backup vào thư mục Restore
                using (var stream = new FileStream(restorePath, FileMode.Create))
                {
                    await backupFile.CopyToAsync(stream);
                }

                // Gọi repository để thực hiện restore
                await _backupRepository.RestoreDatabaseAsync(restorePath);

                return Ok(new
                {
                    message = "Database restore completed successfully.",
                    filePath = $"/Restores/{fileName}"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("list-backups")]
        public IActionResult GetBackupFiles()
        {
            try
            {
                var backupDirectory = Path.Combine(_environment.WebRootPath, "Backups");

                Console.WriteLine($"Backup directory: {backupDirectory}");

                // Kiểm tra thư mục tồn tại
                if (!Directory.Exists(backupDirectory))
                {
                    return Ok(new List<string>()); // Trả về danh sách rỗng nếu thư mục không tồn tại
                }

                // Lấy danh sách file trong thư mục
                var files = Directory.GetFiles(backupDirectory)
                    .Select(file => new
                    {
                        FileName = Path.GetFileName(file),
                        FilePath = $"/Backups/{Path.GetFileName(file)}",
                        CreatedAt = System.IO.File.GetCreationTime(file).ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToList();

                // Log danh sách file
                Console.WriteLine($"Files found: {files.Count}");
                foreach (var file in files)
                {
                    Console.WriteLine($"File: {file.FileName}, CreatedAt: {file.CreatedAt}");
                }

                return Ok(files);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving backups: {ex.Message}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}