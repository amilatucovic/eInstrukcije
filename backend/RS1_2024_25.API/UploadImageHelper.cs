namespace RS1_2024_2025.API
{
    public static class UploadImageHelper
    {
        public static async Task<string> UploadFile(byte[] base64Image)
        {
            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine("Uploads/Images", fileName);

            File.WriteAllBytes(filePath, base64Image);

            string imageUrl = string.Format("/{0}/{1}", "Uploads/Images", fileName);
            return imageUrl;
        }
    }
}
