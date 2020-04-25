using System.IO;
using System.Threading.Tasks;

namespace ProtectedFiles.Domain.Interfaces
{
    public interface IFileManager
    {
        Task UploadFileAsync(string fileName, Stream stream);
    }
}
