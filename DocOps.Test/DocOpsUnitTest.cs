using DocOps.Helpers;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DocOps.Test
{
    [TestClass]
    public class DocOpsUnitTest
    {
        [TestMethod]
        public void CliTestMethod()
        {
            DocOps.Cli.Program.ExecuteCommandLine(new string[] { "init" });
            DocOps.Cli.Program.ExecuteCommandLine(new string[] { "-init" });
            DocOps.Cli.Program.ExecuteCommandLine(new string[] { "--init" });
        }
    }
}