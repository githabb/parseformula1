using AngleSharp.Dom.Html;

namespace ParserLogic
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
