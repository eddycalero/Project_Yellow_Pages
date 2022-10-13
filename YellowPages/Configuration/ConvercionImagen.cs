namespace YellowPages.Configuration
{
	public class ConvercionImagen
	{
        public static async Task<byte[]> ImagenConvertByte(IFormFile iFormFile)
        {
            MemoryStream stream = new MemoryStream();
            await iFormFile.CopyToAsync(stream);
            // 2MB
            stream.Close();
            return stream.ToArray();
        }
    }
}
