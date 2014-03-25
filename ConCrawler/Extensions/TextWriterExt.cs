using NCrawler.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCrawler.Extensions;

namespace ConCrawler.Extensions {
    public static class TextWriterExt {
        public static void WriteLine(this TextWriter writer, ConsoleColor color, string format, params object[] args) {
            AspectF.Define.
                NotNull(writer, "writer").
                NotNull(format, "format");

            Console.ForegroundColor = color;
            writer.WriteLine(format, args);
            Console.ResetColor();
        }
    }
}
