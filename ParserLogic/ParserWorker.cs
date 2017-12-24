using System;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;

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
            var source = await loader.GetPageSource();
            var domParser = new HtmlParser();

            var document = await domParser.ParseAsync(source);

            var result = Parser.Parse(document);
            return result;
        }


    }


}
