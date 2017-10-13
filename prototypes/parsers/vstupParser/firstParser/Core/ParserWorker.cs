using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstParser.Core
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParcerSettings parserSettings;
        HtmlLoader loader;
        bool isActive;

        #region Properties

        public IParser<T> Parser
        {
            get { return parser; }
            set { parser = value; }
        }
        public IParcerSettings ParserSettings
        {
            get { return parserSettings; }
            set {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }
        public bool IsActive { get { return isActive; } }
        #endregion 

        public event Action<object, T> OnNewData;
        public event Action<object> OnComplited;
        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }
        public ParserWorker(IParser<T> parser, IParcerSettings parserSettings):this(parser)
        {
            this.ParserSettings = parserSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Worker()
        {

            if (!isActive)
            {
                OnComplited?.Invoke(this);
                return;
            }

            var source = await loader.GetSourceByPageId();
            var domParser = new HtmlParser();

            var document = await domParser.ParseAsync(source);

            var resultPars = parser.Parse(document);
            
            

            OnNewData?.Invoke(this, resultPars);

            OnComplited?.Invoke(this);
        }
    }
}
