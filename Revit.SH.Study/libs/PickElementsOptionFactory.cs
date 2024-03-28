namespace Revit.SH.Study.libs
{
    /// <summary>
    /// Essa classe e utilizada para criar instancias para PickElementsOptions
    /// </summary>
    public static class PickElementsOptionFactory
    {
        public static CurrentDocumentOption CreateCurrentDocumentOption() => new CurrentDocumentOption();
        public static BothDocumentOption CreateBothDocumentOption() => new BothDocumentOption();
        public static LinkDocumentOption CreateLinkDocumentOption() => new LinkDocumentOption();

    }
}

