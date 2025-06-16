using PASS_Workflow_Engine.Server.Records;
using TestUploadAPI.Records;

namespace PASS_Workflow_Engine.Server.HelperClasses
{
    public class DataTransferObjectHelper
    {
       
        /// <summary>
        /// Geathers all model file paths of type .owl inside of /UploadFolder and creates a list of JSONs that contains the modelPath and modelName of each model 
        /// </summary>
        public string GetJsonsOfModels()
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadFolder");
            string[] modelFiles = Directory.GetFiles(directoryPath);
            var owlFiles = new List<OwlFile>();

            for (int i = 0; i < modelFiles.Length; i++)
            {
                string modelPath = modelFiles[i];
                string modelName = Path.GetFileNameWithoutExtension(modelPath);

                owlFiles.Add(new OwlFile(modelName, modelPath));
            }

            string json = System.Text.Json.JsonSerializer.Serialize(owlFiles);
            return json;
        }


        /// <summary>
        /// Similar to GetJsonsOfModels, but for SVG files.
        /// </summary>
        public string GetJsonsOfSvgs()
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadFolder");
            string[] modelFiles = Directory.GetFiles(directoryPath);
            var svgFiles = new List<SvgFile>();

            for (int i = 0; i < modelFiles.Length; i++)
            {
                string svgPath = modelFiles[i];
                string title = Path.GetFileNameWithoutExtension(svgPath);

                svgFiles.Add(new SvgFile(title, svgPath));
            }

            string json = System.Text.Json.JsonSerializer.Serialize(svgFiles);
            return json;

        }


    }
}
