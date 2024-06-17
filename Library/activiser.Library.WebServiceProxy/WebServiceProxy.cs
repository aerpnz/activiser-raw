namespace activiser.Library.WebService
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;


    public partial class activiser : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        public activiser(String url)
        {
            
            this.Url = url;
            this.UserAgent = string.Format(Properties.Resources.UserAgentString, AssemblyVersion.ToString(4));
            // this.EnableDecompression = true;
        }

        private AssemblyName _assemblyName;
        private Version AssemblyVersion
        {
            get
            {
                if (_assemblyName == null)
                {
                    Assembly a = Assembly.GetExecutingAssembly();
                    _assemblyName = a.GetName();
                }
                return _assemblyName.Version;
            }
        }
        
        //protected override System.Net.WebRequest GetWebRequest(Uri uri)
        //{
        //    System.Net.WebRequest result = base.GetWebRequest(uri);
        //    return result;
        //}

        //protected override System.Net.WebResponse GetWebResponse(System.Net.WebRequest request)
        //{
        //    return base.GetWebResponse(request);
        //}

        //protected override System.Net.WebResponse GetWebResponse(System.Net.WebRequest request, IAsyncResult result)
        //{
        //    return base.GetWebResponse(request, result);
        //}

        //public override void Abort()
        //{
        //    base.Abort();
        //}

        
    }

}

//using System;
//using System.Linq;
//using System.Collections.Generic;
//using System.Text;
//using wsProxy = activiser.WebServiceProxy;
//using System.Net;

//namespace activiser.Library
//{
//    public sealed class WebServiceProxy
//    {
//        private static wsProxy.activiser WebService;

//        static WebServiceProxy()
//        {
//            Reset();
//        }

//        public static wsProxy.activiser activiser
//        {
//            get { return WebService; }
//        }

//        public static void Reset()
//        {
//            WebService = new wsProxy.activiser();

//            System.Reflection.Assembly myAss = System.Reflection.Assembly.GetExecutingAssembly();
//            WebService.UserAgent = string.Format(Properties.Resources.UserAgentString, myAss.GetName().Version.ToString(4));

//            WebService.EnableDecompression = true;
//        }

//        public static string ServerUrl
//        {
//            get
//            {
//                return WebService.Url;
//            }
//            set
//            {
//                WebService.Url = value;
//            }
//        }

//        public int Timeout { get { return WebService.Timeout; } set { WebService.Timeout = value; } }

//        public IWebProxy Proxy { get { return WebService.Proxy; } set { WebService.Proxy = value; } }

//        public ICredentials Credentials { get { return WebService.Credentials; } set { WebService.Credentials = value; } }

//    }
//}
