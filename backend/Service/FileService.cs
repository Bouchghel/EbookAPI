namespace PLivres.Service
{
    public class FileService
    {
        // Le répertoire de téléchargement des fichiers, situé dans wwwroot/uploads
        private readonly string _uploadDirectory = Path.Combine("wwwroot", "uploads");

        // Liste des extensions de fichier autorisées (seulement JPG et PNG)
        private readonly string[] _allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

        // Sauvegarder le fichier d'une manière asynchrone
        public async Task<(bool Success, string FilePath, string ErrorMessage)> SaveFileAsync(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(fileExtension))
                return (false, null, "Invalid file type. Only JPG and PNG are allowed.");

            if (!Directory.Exists(_uploadDirectory))
                Directory.CreateDirectory(_uploadDirectory);

            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_uploadDirectory, fileName);

            try
            {
                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                return (false, null, $"File upload failed: {ex.Message}");
            }

            return (true, $"/uploads/{fileName}", null);
        }
    }
}
