namespace PASS_Workflow_Engine.Server.Records
{
    public class SvgFile
    {
        public string Title { get; private set; }
        public string SvgFilePath { get; private set; }

        public SvgFile(string title, string svgFilePath)
        {
            Title = title;
            SvgFilePath = svgFilePath;
        }

    }
}
