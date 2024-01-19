namespace Course.Services.Photostock.Services;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

public class FirebaseStorageService
{
    private readonly FirebaseStorage _firebaseStorage;

    public FirebaseStorageService(string firebaseBucket)
    {
        _firebaseStorage = new FirebaseStorage(firebaseBucket);
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        // Dosyayı bir Stream'e çevirin
        using (var stream = file.OpenReadStream())
        {
            // Firebase Storage'a yükleyin
            var task = _firebaseStorage
                .Child("data")
                .Child("random")
                .Child(file.FileName)
                .PutAsync(stream);

            // Yükleme ilerlemesini izleyin
            task.Progress.ProgressChanged += (s, e) =>
            {
                Console.WriteLine($"Progress: {e.Percentage} %");
            };

            // Görevi bekleyin, yükleme tamamlandığında dosyanın URL'sini alın
            return await task;
        }
    }
}