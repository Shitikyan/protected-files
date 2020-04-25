using ProtectedFiles.Data.Entities;
using ProtectedFiles.Data.Interfaces;
using ProtectedFiles.Data.Repositories;
using ProtectedFiles.Domain;
using ProtectedFiles.Domain.Interfaces;
using ProtectedFiles.Web.Enums;
using ProtectedFiles.Web.Filters;
using ProtectedFiles.Web.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProtectedFiles.Web.Controllers
{
    [Route("upload")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UploadController : ApiController
    {
        private readonly IFileManager _fileManager = new LocalStorageFileManager();
        private readonly IItemsRepository _itemsRepository = new ItemsRepository();

        [HttpPost]
        [SessionRoleAuthorizationFilter(Roles.Admin)]
        public async Task<IHttpActionResult> UploadProtectedFile(int? itemId)
        {
            if (!itemId.HasValue)
            {
                return BadRequest("ItemId is Required");
            }

            try
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ?
                           HttpContext.Current.Request.Files[0] : null;
                return await UploadProtectedFileImpl(file, itemId.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<IHttpActionResult> UploadProtectedFileImpl(HttpPostedFile file, int itemId)
        {
            var item = await _itemsRepository.GetItem(itemId);
            var exists = true;

            if (item == null)
            {
                item = new Item { ItemId = itemId };
                exists = false;
            }

            MapItem(file, item);

            using (var stream = file.InputStream)
            {
                string fileName = item.ItemId + item.FileExtension;
                await _fileManager.UploadFileAsync(fileName, stream);
            }

            if (exists)
            {
                _itemsRepository.UpdateItem(item);
            }
            else
            {
                _itemsRepository.AddItem(item);
            }

            await _itemsRepository.SaveChangesAsync();

            var model = new UploadFileResult { Success = true, };

            return Json(model);
        }

        private void MapItem(HttpPostedFile file, Item item)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);

            item.FileName = fileName;
            item.FileExtension = fileExtension;
            item.FileSizeInBytes = file.ContentLength;
        }
    }
}
