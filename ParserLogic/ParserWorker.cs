using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Threading.Tasks;

namespace ParserLogic
{
    public class ParserWorker<T> where T : class
    {
        HtmlLoader loader;

        #region Properties

        public IParser<T> Parser { get; set; }

        public ParserSettings Settings { get; set; }

        #endregion

        public ParserWorker(IParser<T> parser)
        {
            this.Parser = parser;
        }

        public ParserWorker(IParser<T> parser, ParserSettings parserSettings) : this(parser)
        {
            this.Settings = parserSettings;
        }

        public async Task<T> Start()
        {
            loader = new HtmlLoader(Settings);
            T result = await Worker();
            return result;
        }

        private async Task<T> Worker()
        {
            string source;
            try
            {
                source = await loader.GetPageSource();
            }
            catch
            {
                throw new Exception("Ошибочная ссылка на страницу");
            }

            if (source == null)
                throw new Exception("Страница не существует");

            var domParser = new HtmlParser();

            IHtmlDocument document;
            try
            {
                document = await domParser.ParseAsync(source);
            }
            catch
            {
                throw new Exception("Ошибочный формат страницы по ссылке");
            }

            T result;
            try
            {
                result = Parser.Parse(document);
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при парсинге страницы");
            }
            return result;
        }

    }

}
