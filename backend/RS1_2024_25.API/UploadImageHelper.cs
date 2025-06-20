using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

public static class UploadImageHelper
{
    public static async Task<string> UploadFile(byte[] base64Image)
    {
        var fileName = Guid.NewGuid().ToString() + ".jpg";
        var folderPath = Path.Combine("Uploads", "Images");
        var filePath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Resize slika na max 300x300
        using (var image = Image.Load(base64Image))
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(350, 350), 
                Mode = ResizeMode.Max
            }));

            await image.SaveAsJpegAsync(filePath);
        }

        string imageUrl = $"/Uploads/Images/{fileName}";
        return imageUrl;
    }
}
