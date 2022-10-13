namespace YellowPages.Configuration
{
	public class HelperUploadFile
	{
        private PathProviders _pathPrivider;

        public HelperUploadFile(PathProviders pathProviders)
        {
            _pathPrivider = pathProviders;
        }
        public async Task<String> UploadFilesAsync(IFormFile file, string title, Folders folder)
        {
            string path = this._pathPrivider.MapPath(title, folder);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }
    }
}
