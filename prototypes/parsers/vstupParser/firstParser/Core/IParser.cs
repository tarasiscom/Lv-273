using AngleSharp.Dom.Html;


namespace firstParser.Core
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
