using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Collaboration.Web.UI.Utilities
{
    public class ImageUtility
    {
        public Stream GetProfileImage(object theImg)
        {
            try
            {
                return new MemoryStream((byte[])theImg);
            }
            catch
            {
                return null;
            }

        }
    }
}