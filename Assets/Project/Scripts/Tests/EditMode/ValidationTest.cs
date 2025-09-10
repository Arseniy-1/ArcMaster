using NUnit.Framework;
using Project.Scripts.Tools;

namespace Project.Scripts.Tests.EditMode
{
    public class ValidationTest
    {
        [Test]
        public void NewTestScriptSimplePasses()
        {
            Validator.FindMissingComponents();
            // Use the Assert class to test conditions
        }
    }
}
