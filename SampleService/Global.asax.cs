using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SampleService
{
    /// <summary>
    /// The request-response logging code was copied from https://blogs.msdn.microsoft.com/rodneyviana/2014/02/06/logging-incoming-requests-and-responses-in-an-asp-net-or-wcf-application-in-compatibility-mode/
    /// and adapted for log4net.
    /// </summary>

    public class Global : HttpApplication
    {
        //The code samples are provided AS IS without warranty of any kind. 
        // Microsoft disclaims all implied warranties including, without limitation, 
        // any implied warranties of merchantability or of fitness for a particular purpose. 

        /*

        The entire risk arising out of the use or performance of the sample scripts and documentation remains with you. 
        In no event shall Microsoft, its authors, or anyone else involved in the creation, production, or delivery of the scripts 
        be liable for any damages whatsoever (including, without limitation, damages for loss of business profits, business interruption, 
        loss of business information, or other pecuniary loss) arising out of the use of or inability to use the sample scripts 
        or documentation, even if Microsoft has been advised of the possibility of such damages.

        */

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            // Creates a unique id to match Rquests with Responses
            string id = String.Format("Id: {0} Uri: {1}", Guid.NewGuid(), Request.Url);
            FilterSaveLog input = new FilterSaveLog(HttpContext.Current, Request.Filter, id);
            input.SetFilter(false);
            Request.Filter = input;
            FilterSaveLog output = new FilterSaveLog(HttpContext.Current, Response.Filter, id);
            output.SetFilter(true);
            Response.Filter = output;

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }

    class FilterSaveLog : Stream
    {
        protected static object writeLock = null;
        protected Stream sinkStream;
        protected bool inDisk;
        protected bool isClosed;
        protected string id;
        protected bool isResponse;
        protected HttpContext context;
        protected log4net.ILog logger = log4net.LogManager.GetLogger("SampleService");

        public FilterSaveLog(HttpContext Context, Stream Sink, string Id)
        {
            context = Context;
            id = Id;
            sinkStream = Sink;
            inDisk = false;
            isClosed = false;
        }

        public void SetFilter(bool IsResponse)
        {


            isResponse = IsResponse;
            id = (isResponse ? "Reponse " : "Request ") + id;

            //
            // For Request only read the incoming stream and log it as it will not be "filtered" for a WCF request
            //
            if (!IsResponse)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.Debug(id);
                }

                if (context.Request.InputStream.Length > 0)
                {
                    context.Request.InputStream.Position = 0;
                    byte[] rawBytes = new byte[context.Request.InputStream.Length];
                    context.Request.InputStream.Read(rawBytes, 0, rawBytes.Length);
                    context.Request.InputStream.Position = 0;

                    try
                    {
                        var txt = Encoding.UTF8.GetString(rawBytes);
                        logger.Debug(txt);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Error logging request", ex);
                    }
                }
                else
                {
                    logger.Debug("(no body)");
                }
            }

        }

        public override bool CanRead
        {
            get { return sinkStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return sinkStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return sinkStream.CanWrite; }
        }

        public override long Length
        {
            get
            {
                return sinkStream.Length;
            }
        }

        public override long Position
        {
            get { return sinkStream.Position; }
            set { sinkStream.Position = value; }
        }


        //
        public override int Read(byte[] buffer, int offset, int count)
        {
            int c = sinkStream.Read(buffer, offset, count);
            return c;
        }

        public override long Seek(long offset, System.IO.SeekOrigin direction)
        {
            return sinkStream.Seek(offset, direction);
        }

        public override void SetLength(long length)
        {
            sinkStream.SetLength(length);
        }

        public override void Close()
        {

            sinkStream.Close();
            isClosed = true;
        }

        public override void Flush()
        {

            sinkStream.Flush();
        }

        // For streamed responses (i.e. not buffered) there will be more than one Response (but the id will match the Request)
        public override void Write(byte[] buffer, int offset, int count)
        {
            sinkStream.Write(buffer, offset, count);

            if (logger.IsDebugEnabled)
            {
                logger.Debug(id);
                try
                {
                    var txt = Encoding.UTF8.GetString(buffer);
                    logger.Debug(txt);
                }
                catch (Exception ex)
                {
                    logger.Error("Error logging response", ex);
                }
            }
        }
    }
}
