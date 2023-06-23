namespace Vendas_Site.Models
{
    public class FileManagerModel
    {
        
        public FileInfo[] Files { get; set; }
        public IFormFile IFormFile { get; set; }
        public List<FileInfo> IFormfiles { get; set; }

        public string PatchImagensProduto { get; set; }
    }
    
}
