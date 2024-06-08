namespace BookStreetConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sourceDirectory = @"D:\Code\BookStreetAPI\Domain\Entities\"; // Thư mục chứa các tệp tin ban đầu
            string targetDirectory = @"D:\Code\BookStreetAPI\Application\DTO\"; // Thư mục gốc để tạo các thư mục mới

            try
            {
                // Lấy danh sách các tệp tin trong thư mục nguồn
                string[] fileEntries = Directory.GetFiles(sourceDirectory);

                foreach (string filePath in fileEntries)
                {
                    // Lấy tên tệp tin mà không có phần mở rộng
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

                    // Tạo đường dẫn đầy đủ cho thư mục mới
                    string newDirectoryPath = Path.Combine(targetDirectory, fileNameWithoutExtension);

                    // Kiểm tra xem thư mục đã tồn tại chưa
                    if (Directory.Exists(newDirectoryPath))
                    {
                        Console.WriteLine($"Directory already exists: {newDirectoryPath}");
                        continue;
                    }

                    // Tạo thư mục mới
                    Directory.CreateDirectory(newDirectoryPath);

                    // thêm file có hậu tố DTO.cs vào thư mục mới
                    string newFilePath = Path.Combine(newDirectoryPath, $"{fileNameWithoutExtension}DTO.cs");

                    // copy file từ thư mục nguồn sang thư mục mới
                    File.Copy(filePath, newFilePath);


                    Console.WriteLine($"Created directory: {newDirectoryPath}");
                }

                Console.WriteLine("All directories created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
    }
}
