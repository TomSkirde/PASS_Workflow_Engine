using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using PASS_Workflow_Engine.Server.HelperClasses;
using TestUploadAPI.Records;

namespace PASS_Workflow_Engine.Server.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {

        /// <summary>
        /// Uploads a .owl file into UploadFolder 
        /// </summary>
        [HttpPost]
        public IActionResult UploadFile(IFormFile owlfile)
        {
            return Ok(new UploadHandler().Upload(owlfile));
        }

        /// <summary>
        /// Creates a JSON for every model inside of UploadFolder. 
        /// The JSONs are of type OwlFile record at /Records/OwlFile.cs   
        /// </summary>
        /// <returns>List of JSONs</returns>
        [HttpGet]
        public IActionResult GetOwlFIleJsons()
        {
            return Ok(new DataTransferObjectHelper().GetJsonsOfModels());
        }

        [HttpGet]
        public IActionResult GetSvgFileJsons()
        {
            return Ok(new DataTransferObjectHelper().GetJsonsOfSvgs);
        }



        // TODO: Überlegen, ob man objekte in Controller nutzen kann, um einmal ALPSApi Reader zu
        // initialisieren und später eine LoadModel Methode zu nutzen.
        [HttpPost]
        public IActionResult LoadModel(string OwlPath)
        {
            AlpsParser.GetAlpsParser().ParseModels(OwlPath);
            return Ok(AlpsParser.GetAlpsParser());
        }


        /// <summary>
        /// Extracts the SVG from the provided OWL file path and stores the .svg Files in /SvgFolder.
        /// </summary>
        [HttpPost]
        public IActionResult ExtractSvg(string path)
        {
            new SvgCreater().ExtractSvgFromOwl(path);
            return Ok();
        }
    }
}

