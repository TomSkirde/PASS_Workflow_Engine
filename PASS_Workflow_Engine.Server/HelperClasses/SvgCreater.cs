using System.Xml.Linq;

namespace PASS_Workflow_Engine.Server.HelperClasses
{
    
    public class SvgCreater
    {
        /// <summary>
        /// Method to extract SVG elements from an OWL file and store them as separate SVG for each 
        /// SBD or SID in the OWL file of the PASS model.
        /// </summary>
        /// <param name="owlFilePath"></param>
        public void ExtractSvgFromOwl(string owlFilePath)
        {
            XDocument doc = LoadOwl(owlFilePath);

            // Finds all SVG elements in the XML file that are not nested within other SVG elements.
            var svgElements = doc.Descendants()
                .Where(e => e.Name.LocalName.Equals("svg", StringComparison.OrdinalIgnoreCase) &&
                !e.Ancestors().Any(a => a.Name.LocalName.Equals("svg", StringComparison.OrdinalIgnoreCase)));

            StoreSvgFiles(svgElements);

        }// end ExtractSvgFromOwl


        /// <summary>
        /// Method to load the OWL file from the given path and return it as an XDocument.
        /// </summary>
        /// <param name="owlFilePath">Path of the XML file that contains the PASS model</param>
        /// <returns>XDocument (Class of the System.XML.Linq libary)</returns>
        private XDocument LoadOwl(string owlFilePath)
        {
            try
            {
                return XDocument.Load(owlFilePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Owl can not be loaded");
            }
        }

        /// <summary>
        /// Method to store each SVG from each XElement of the given IEnumerable<XElement> svgElements 
        /// </summary>
        /// <param name="svgElements"></param>
        private void StoreSvgFiles(IEnumerable<XElement> svgElements)
        {
            foreach (var svg in svgElements)
            {
                XNamespace ns = svg.Name.Namespace;
                var titleElement = svg.Descendants(ns + "title").FirstOrDefault();

                if (titleElement == null)
                {
                    throw new Exception("Owl is corrupted: Owl File is missing a SBD or SID Title");
                }

                string title = titleElement.Value;

                // ":" can not be used in file names and is therefore replaced by "-"
                string svgFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "SvgFolder");
                string svgName = String.Concat(title.Replace(":", "-"), ".svg");
                string svgPath = Path.Combine(svgFolderPath, svgName);



                using (StreamWriter sw = File.CreateText(svgPath))
                {
                    sw.Write(svg);
                }


            }//end foreach
        }//end StoreSvgFile
    }
}
