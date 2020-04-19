using ProtectedFiles.Domain.Interfaces;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProtectedFiles.Domain
{
    public class LocalStorageFileManager : IFileManager
    {
        private LocalStorageFileManagerOptions _options;

        public LocalStorageFileManager(LocalStorageFileManagerOptions options)
        {
            _options = options;
        }

        public async Task UploadFileAsync(string fileName, Stream stream)
        {
            var hiddenDirectoryPath = Path.Combine(_options.Directory, "ProtectedFiles");
            DirectoryEnsureCreated(hiddenDirectoryPath);
            var filePath = Path.Combine(hiddenDirectoryPath, fileName);
            var fileNameWOExtension = Path.GetFileNameWithoutExtension(fileName);
            var files = Directory.GetFiles(hiddenDirectoryPath);
            var existingFile = files.SingleOrDefault(
                f => Path.GetFileNameWithoutExtension(f) == fileNameWOExtension);

            if (existingFile != null)
            {
                File.Delete(existingFile);
            }

            using (var destinationStream = File.Create(filePath))
            {
                await stream.CopyToAsync(destinationStream);
            }
        }

        private void DirectoryEnsureCreated(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists) return;

            directoryInfo = Directory.CreateDirectory(path);
            directoryInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
        }
    }
}
