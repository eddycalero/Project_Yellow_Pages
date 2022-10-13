namespace YellowPages.Configuration
{
	public class PathProviders
	{
        // Crear una ruta absoluta para subir los archivos
        private IWebHostEnvironment _webHostEnvironment;

        public PathProviders(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string MapPath(string fileName, Folders folders)
        {
            string carpeta = folders switch
            {
                
                Folders.Images => "images",
             
            };

            // Crear la ruta absoluta
            string path = Path.Combine(this._webHostEnvironment.WebRootPath, carpeta, fileName);
            if (folders == Folders.Temp)
            {
                path = Path.Combine(Path.GetTempPath(), fileName);
            }
            return path;
        }
    }

    public enum Folders : int
    {
         Images = 1, Temp = 3
    }
}
