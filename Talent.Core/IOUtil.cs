using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Talent.Core
{
    /// <summary>
    /// Represents File Utilities
    /// </summary>
    public static class IOUtil
    {

        /// <summary>
        /// Counts the lines.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="bufferSize">Size of the buffer of the reader.</param>
        /// <returns>
        /// The number of lines within the underlying stream.
        /// </returns>
        /// <remarks>
        /// It resets the stream position to the beginning after
        /// performing the line count. The reader and underlying
        /// stream will not be closed within this method.
        /// </remarks>
        public static int CountLines(this StreamReader reader, int? bufferSize)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader", "reader can not be null.");
            }

            if (reader.BaseStream == null)
            {
                throw new ArgumentNullException("reader", "The underlying stream can not be null.");
            }

            if (!reader.BaseStream.CanRead)
            {
                throw new ArgumentException("The underlying stream can not be read.");
            }

            // set default buffer size
            if (bufferSize == null)
            {
                bufferSize = 0x1000;
            }

            // initialize buffer
            char[] buffer = new char[bufferSize.Value];

            int lineCount = 0;
            int readCount;

            while ((readCount = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                for (int i = 0; i < readCount; i++)
                {
                    if (buffer[i] == '\n')
                    {
                        lineCount++;
                    }
                }
            }

            // reset the position to the beginning of the stream
            reader.BaseStream.Position = 0;

            return lineCount;
        }

    }
}
