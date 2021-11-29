namespace NerdStore.Bdd.Tests.Config
{
    public class AutomacaoWebTestsFixture
    {
        public readonly ConfigurationHelper Configuration;

        public SeleniumHelper BrowserHelper;

        public AutomacaoWebTestsFixture()
        {
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Browser.Chrome, Configuration, false);
        }


    }
}
