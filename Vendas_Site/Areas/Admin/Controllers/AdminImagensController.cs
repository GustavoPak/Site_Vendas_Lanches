using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vendas_Site.Models;

namespace Vendas_Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myconfig;
        private readonly IWebHostEnvironment _webHost;

        public AdminImagensController(IWebHostEnvironment hostingEnvironment,
           IOptions<ConfigurationImagens> myConfiguration)
        {
            _webHost = hostingEnvironment;
            _myconfig = myConfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

         public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if(files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Quantidade excessiva de arquivos não permitida";
                return View(ViewData);
            }

            long size = files.Sum(p => p.Length);

            var filesPathName = new List<string>();
            var filePath = Path.Combine(_webHost.WebRootPath, _myconfig.NomePastaImagensProdutos);

            foreach(var formFile in files)
            {
                if(formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".png") ||
                     formFile.FileName.Contains(".gif"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                    
                    filesPathName.Add(fileNameWithPath);

                    using var stream = new FileStream(fileNameWithPath, FileMode.Create);
                    await formFile.CopyToAsync(stream);

                }
            }

            ViewData["Resultado"] = $"{filesPathName.Count} foram enviados ao servidor com sucesso!"
                                      + $"Total de :{size} bytes";

            ViewBag.Arquivos = filesPathName;

            return View(ViewData);
        }

        public IActionResult GetImages()
        {
            FileManagerModel model = new();

            var serverImagesPath = Path.Combine(_webHost.WebRootPath, _myconfig.NomePastaImagensProdutos);

            DirectoryInfo directory = new(serverImagesPath);

            FileInfo[] filer = directory.GetFiles();

            model.PatchImagensProduto = _myconfig.NomePastaImagensProdutos;

            if(filer.Length == 0)
            {
                ViewData["Erro"] = $"Não foram encontrados nenhum arquivo em {serverImagesPath}";
            }

            model.Files = filer;

            return View(model);
        }

        public IActionResult DeleteFile(string fileName)
        {
            string imagemDeleta = Path.Combine(_webHost.WebRootPath, _myconfig.NomePastaImagensProdutos + "\\", fileName);

            if (System.IO.File.Exists(imagemDeleta))
            {
                System.IO.File.Delete(imagemDeleta);

                ViewData["Deletado"] = $"O arquivo {fileName} foi deletado com  sucesso";
            }

            return View("Index");
        }
    }
}
