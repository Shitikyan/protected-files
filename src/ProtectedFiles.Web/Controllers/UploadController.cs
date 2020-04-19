using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtectedFiles.Data.Entities;
using ProtectedFiles.Data.Interfaces;
using ProtectedFiles.Domain.Interfaces;
using ProtectedFiles.Web.Enums;
using ProtectedFiles.Web.Infrastructure.Filters;
using ProtectedFiles.Web.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProtectedFiles.Web.Controllers
{
    public class UploadController : Controller
    {
        private readonly IFileManager _fileManager;
        private readonly IItemsRepository _itemsRepository;

        public UploadController(
            IFileManager fileManager,
            IItemsRepository itemsRepository)
        {
            _fileManager = fileManager;
            _itemsRepository = itemsRepository;
        }

        [HttpGet]
        // [Authorize(Policy = "Admin")]
        [SessionAdminAuthorizationFilter(Roles.Admin)]
        /// <param name="itemId">
        ///     If not passed, or unable to serialize 
        ///     parameter to int itemId is null.
        /// </param>
        public IActionResult UploadProtectedFile([FromQuery] int? itemId)
        {
            if (!itemId.HasValue)
            {
                return View("Error");
            }

            return View(itemId);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProtectedFile([FromForm]UploadFileViewModel uploadFileViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(UploadProtectedFile),
                                        new { itemId = uploadFileViewModel.ItemId });
            }

            try
            {
                return await UploadProtectedFileImpl(uploadFileViewModel);
            }
            catch (Exception ex)
            {
                var model = new UploadFileResult
                {
                    Success = false,
                    Exception = ex,
                };

                return View("UploadResult", model);
            }
        }

        private async Task<IActionResult> UploadProtectedFileImpl(UploadFileViewModel uploadFileViewModel)
        {
            var item = await _itemsRepository.GetItem(uploadFileViewModel.ItemId);
            var exists = true;

            if (item == null)
            {
                item = new Item { ItemId = uploadFileViewModel.ItemId };
                exists = false;
            }

            MapItem(uploadFileViewModel.File, item);

            using (var stream = uploadFileViewModel.File.OpenReadStream())
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

            var model = new UploadFileResult
            {
                Success = true,
            };

            return View("UploadResult", model);
        }

        private void MapItem(IFormFile file, Item item)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);

            item.FileName = fileName;
            item.FileExtension = fileExtension;
            item.FileSizeInBytes = file.Length;
        }
    }
}