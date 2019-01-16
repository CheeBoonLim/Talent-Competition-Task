using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace Talent.Core
{
    public static class XmlUtil
    {
        /// <summary>
        /// Deserializes XML to object.
        /// </summary>
        /// <typeparam name="TObject">Type of the object to deserialize.</typeparam>
        /// <param name="xml">xml string.</param>
        /// <returns>Object of TObject type .</returns>
        public static TObject XmlToObject<TObject>(string xml)
        {
            var xs = new XmlSerializer(typeof(TObject));
            var stringReader = new StringReader(xml);

            return (TObject)xs.Deserialize(stringReader);
        }

        /// <summary>
        /// Deserializes XML to object.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="xml">The XML.</param>
        /// <param name="rootName">Name of the root.</param>
        /// <returns>Object of TObject type .</returns>
        public static TObject XmlToObject<TObject>(string xml, string rootName)
        {
            var xs = new XmlSerializer(typeof(TObject), new XmlRootAttribute(rootName));
            var stringReader = new StringReader(xml);

            return (TObject)xs.Deserialize(stringReader);
        }

        /// <summary>
        /// Deserializes XML to object.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="xml">The XML.</param>
        /// <param name="defaultNamespace">The default namespace.</param>
        /// <returns>The object of TObject
        /// </returns>
        /// 24/06/2009
        public static TObject XmlToObject2<TObject>(string xml, string defaultNamespace)
        {
            var xs = new XmlSerializer(typeof(TObject), defaultNamespace);
            var stringReader = new StringReader(xml);

            return (TObject)xs.Deserialize(stringReader);
        }

        /// <summary>
        /// Deserializes XML to object.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="xml">The XML.</param>
        /// <param name="rootName">Name of the root.</param>
        /// <param name="defaultNamespace">The default namespace.</param>
        /// <returns>The object of <see cref="TObject"/>
        /// </returns>
        /// 2/09/2009
        public static TObject XmlToObject<TObject>(string xml, string rootName, string defaultNamespace)
        {
            var xs = new XmlSerializer(typeof(TObject), new XmlAttributeOverrides(), new Type[] { }, new XmlRootAttribute(rootName), defaultNamespace);

            var stringReader = new StringReader(xml);

            return (TObject)xs.Deserialize(stringReader);
        }

        /// <summary>
        /// Deserializes the XMLFile to object
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="path">The path.</param>
        /// <returns>The object of TObject
        /// </returns>
        public static TObject XmlFileToObject<TObject>(string path)
        {
            try
            {
                using (var reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    var xml = reader.ReadToEnd();
                    return XmlToObject<TObject>(xml);
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.ToString());
            }

            return default(TObject);
        }

        /// <summary>
        /// Deserializes the XMLFile to object.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="path">The path.</param>
        /// <param name="rootName">Name of the root.</param>
        /// <returns>The object of TObject
        /// </returns>
        /// 24/07/2009
        public static TObject XmlFileToObject<TObject>(string path, string rootName)
        {
            try
            {
                using (var reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    var xml = reader.ReadToEnd();
                    return XmlToObject<TObject>(xml, rootName);
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.ToString());
            }

            return default(TObject);
        }

        /// <summary>
        /// Deserializes the XMLFile to object.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="path">The path.</param>
        /// <param name="defaultNamespace">The default namespace.</param>
        /// <returns>The object of <see cref="TObject"/>
        /// </returns>
        /// 24/07/2009
        public static TObject XmlFileToObject2<TObject>(string path, string defaultNamespace)
        {
            try
            {
                using (var reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    var xml = reader.ReadToEnd();
                    return XmlToObject2<TObject>(xml, defaultNamespace);
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.ToString());
            }

            return default(TObject);
        }

        /// <summary>
        ///  Serializes the object to XML.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns>The object of <see cref="String"/>
        /// </returns>
        /// 2/07/2009
        public static string ObjectToXml<TObject>(TObject obj)
        {
            return ObjectToXml<TObject>(obj, false, false, false);
        }

        /// <summary>
        ///  Serializes the object to indented XML.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns>The object of <see cref="String"/>
        /// </returns>
        /// 2/07/2009
        public static string ObjectToIndentedXml<TObject>(TObject obj)
        {
            return ObjectToXml(obj, false, false, true);
        }

        /// <summary>
        /// Object to XElement.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns>The object of <see cref="XElement"/>
        /// </returns>
        /// 21/05/2009
        public static XElement ObjectToXElement<TObject>(TObject obj)
        {
            return XElement.Parse(ObjectToXml(obj));
        }

        /// <summary>
        ///  Serializes the object to XML file.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="path">The path.</param>
        /// <returns>Xml string</returns>
        public static string ObjectToXmlFile<TObject>(TObject obj, string path)
        {
            try
            {
                var xml = ObjectToXml(obj);
                using (var writer = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
                {
                    writer.WriteLine(xml);
                    writer.Flush();
                }
                return xml;
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.ToString());
            }

            return null;
        }

        /// <summary>
        ///  Serializes the object of TObject to Xml string by using XmlSerializer.
        /// </summary>
        /// <typeparam name="TObject">>Type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="omitXmlDeclaration">Ignore Xml declaration e.g. ?xml version="1.0" encoding="utf-16"?</param>
        /// <param name="ignoreDefaultRootNamespaces">Ingore namespaces e.g. xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
        /// xmlns:xsd="http://www.w3.org/2001/XMLSchema"</param>
        /// <param name="defaultRootNamespace">Specifies one default root namespace.</param>
        /// <param name="defaultRootNamespacePrefix">Specifies one default root namespace prefix. e.g. xmlns:prefix</param>
        /// <param name="isIndent">If the return string requires indenting.</param>
        /// <returns>Serialized Xml string of the object.</returns>
        public static string ObjectToXml<TObject>(TObject obj, bool omitXmlDeclaration, bool ignoreDefaultRootNamespaces,
            string defaultRootNamespace, string defaultRootNamespacePrefix, bool isIndent)
        {
            XmlSerializerNamespaces ns;
            var xs = new XmlSerializer(typeof(TObject));

            var xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = omitXmlDeclaration;
            xmlWriterSettings.Indent = isIndent;
            xmlWriterSettings.Encoding = Encoding.UTF8;
            var stringWriter = new StringWriter();
            var xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings);

            if (ignoreDefaultRootNamespaces)
            {
                ns = new XmlSerializerNamespaces();
                ns.Add(String.Empty, string.Empty);
                if (!string.IsNullOrEmpty(defaultRootNamespace) && !string.IsNullOrEmpty(defaultRootNamespacePrefix))
                {
                    ns.Add(defaultRootNamespacePrefix, defaultRootNamespace);
                }

                if (xmlWriter != null)
                {
                    xs.Serialize(xmlWriter, obj, ns);
                }
            }
            else
            {
                if (xmlWriter != null)
                {
                    xs.Serialize(xmlWriter, obj);
                }
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Objects to XML2.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="omitXmlDeclaration">if set to <c>true</c> [omit XML declaration].</param>
        /// <param name="isIndent">if set to <c>true</c> [is indent].</param>
        /// <returns></returns>
        public static string ObjectToXml2(object obj, bool omitXmlDeclaration, bool isIndent)
        {
            var xs = new XmlSerializer(obj.GetType());

            var xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = omitXmlDeclaration;
            xmlWriterSettings.Indent = isIndent;
            xmlWriterSettings.Encoding = Encoding.UTF8;
            var stringWriter = new StringWriter();
            var xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings);

            if (xmlWriter != null)
            {
                xs.Serialize(xmlWriter, obj);
            }

            return stringWriter.ToString();
        }


        /// <summary>
        /// Serializes object Object to XML.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="omitXmlDeclaration">if set to <c>true</c> [omit XML declaration].</param>
        /// <param name="ignoreDefaultRootNamespaces">if set to <c>true</c> [ignore default root namespaces].</param>
        /// <param name="defaultRootNamespace">The default root namespace.</param>
        /// <param name="defaultRootNamespacePrefix">The default root namespace prefix.</param>
        /// <param name="isIndent">if set to <c>true</c> [is indent].</param>
        /// <returns></returns>
        public static string ObjectToXml2(object obj, bool omitXmlDeclaration, bool ignoreDefaultRootNamespaces,
            string defaultRootNamespace, string defaultRootNamespacePrefix, bool isIndent)
        {
            XmlSerializerNamespaces ns;
            var xs = new XmlSerializer(obj.GetType());

            var xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = omitXmlDeclaration;
            xmlWriterSettings.Indent = isIndent;
            xmlWriterSettings.Encoding = Encoding.UTF8;
            var stringWriter = new StringWriter();
            var xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings);

            if (ignoreDefaultRootNamespaces)
            {
                ns = new XmlSerializerNamespaces();
                ns.Add(String.Empty, string.Empty);

                if (!string.IsNullOrEmpty(defaultRootNamespace) && !string.IsNullOrEmpty(defaultRootNamespacePrefix))
                {
                    ns.Add(defaultRootNamespacePrefix, defaultRootNamespace);
                }

                if (xmlWriter != null)
                {
                    xs.Serialize(xmlWriter, obj, ns);
                }
            }
            else
            {
                if (xmlWriter != null)
                {
                    xs.Serialize(xmlWriter, obj);
                }
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Serializes the object to XML.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="omitXmlDeclaration">if set to <c>true</c> [omit XML declaration].</param>
        /// <param name="ignoreDefaultRootNamespaces">if set to <c>true</c> [ignore default root namespaces].</param>
        /// <param name="isIndent">if set to <c>true</c> [is indent].</param>
        /// <returns>The object of <see cref="String"/>
        /// </returns>
        /// 2/07/2009
        public static string ObjectToXml<TObject>(TObject obj, bool omitXmlDeclaration, bool ignoreDefaultRootNamespaces, bool isIndent)
        {
            return ObjectToXml(obj, omitXmlDeclaration, ignoreDefaultRootNamespaces, null, null, isIndent);
        }

        /// <summary>
        /// Serializes the objects to XML (UTF8)
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>XML string</returns>
        public static string ObjectToXmlUTF8<TObject>(TObject obj)
        {
            try
            {
                var xs = new XmlSerializer(typeof(TObject));
                using (MemoryStream ms = new MemoryStream())
                {
                    var settings = new XmlWriterSettings();
                    settings.Encoding = Encoding.UTF8;
                    settings.Indent = true;
                    settings.NewLineChars = Environment.NewLine;
                    settings.ConformanceLevel = ConformanceLevel.Document;

                    using (XmlWriter writer = XmlTextWriter.Create(ms, settings))
                    {
                        xs.Serialize(writer, obj);
                    }

                    string xml = Encoding.UTF8.GetString(ms.ToArray());

                    if (xml.Length > 0 && xml[0] != '<')
                    {
                        xml = xml.Substring(1, xml.Length - 1);
                    }

                    return xml;
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
                Debug.WriteLine(x);
            }

            return null;
        }

        /// <summary>
        /// Clones the object by XML serialization.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns>The object of T
        /// </returns>
        /// 7/07/2009
        public static T CloneByXml<T>(this T obj) where T : class, new()
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "obj must be defined.");
            }

            return XmlToObject<T>(ObjectToXml(obj));
        }

        /// <summary>
        /// Tries mapping the objects of different types by XML serialization.
        /// </summary>
        /// <typeparam name="TFrom">The type of TFrom.</typeparam>
        /// <typeparam name="TTo">The type of TTo.</typeparam>
        /// <param name="from">The TFrom object as source object for the mapping.</param>
        /// <param name="errors">The errors while performing the mapping.</param>
        /// <returns>
        /// The object of type TTo that has been mapped.
        /// </returns>
        /// 28/07/2009
        public static TTo TryMapObjectByXml<TFrom, TTo>(this TFrom from, out string errors)
            where TFrom : class, new()
            where TTo : class, new()
        {
            errors = null;

            try
            {
                return XmlToObject<TTo>(ObjectToXml(from));
            }
            catch (Exception x)
            {
                errors = x.Message;
                Debug.WriteLine(x.Message);
            }

            return null;
        }

        /// <summary>
        /// Tries mapping the objects of different types by XML serialization.
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <param name="from">From.</param>
        /// <param name="rootName">Name of the root.</param>
        /// <param name="defaultRootNamespace">The default root namespace.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>The object of <see cref="TTo"/></returns>
        /// 2/09/2009
        public static TTo TryMapObjectByXml<TFrom, TTo>(this TFrom from, string rootName, string defaultRootNamespace, out string errors)
            where TFrom : class, new()
            where TTo : class, new()
        {
            errors = null;

            try
            {
                return XmlToObject<TTo>(ObjectToXml(from), rootName, defaultRootNamespace);
            }
            catch (Exception x)
            {
                errors = x.Message;
                Debug.WriteLine(x.Message);
            }

            return null;
        }

        /// <summary>
        /// To the XML from XmlDocument object.
        /// </summary>
        /// <param name="xmlDoc">The XML doc.</param>
        /// <returns>XML string</returns>
        public static string ToXml(this XmlDocument xmlDoc)
        {
            if (xmlDoc == null)
                throw new ArgumentNullException("xmlDoc", "xmlDoc must be defined.");

            var stringWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlDoc.WriteTo(xmlWriter);

            return stringWriter.ToString();
        }

        /// <summary>
        /// Determines whether the specified text is XML.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is XML; otherwise, <c>false</c>.
        /// </returns>
        /// 22/07/2009
        public static bool IsXml(this string text)
        {
            try
            {
                XElement.Parse(text, LoadOptions.None);
                return true;
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message);
                return false;
            }
        }

        /// <summary>
        /// Indents the string as XML.
        /// </summary>
        /// <param name="xmlString">The XML string.</param>
        /// <returns>Indented XML string</returns>
        public static string IndentAsXml(this string xmlString)
        {
            var doc = new XmlDocument();

            try
            {
                doc.LoadXml(xmlString);
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message);
                return xmlString;
            }

            // create a stream buffer that can be read as a string
            using (var sw = new StringWriter())
            {
                // create a specialized writer for XML code
                using (var xtw = new XmlTextWriter(sw))
                {
                    // set the writer to use indented (hierarchical) elements
                    xtw.Formatting = Formatting.Indented;

                    // write the XML document to the stream
                    doc.WriteTo(xtw);

                    // return the stream as a string
                    return sw.ToString();
                }
            }
        }

        /// <summary>
        /// Validates the XML.
        /// </summary>
        /// <param name="xmlSource">The XML source.</param>
        /// <param name="xsdSource">The XSD source.</param>
        /// <returns>The object of <see cref="String"/> containing the error message;  Returns empty string
        /// indicates no errors.
        /// </returns>
        /// 12/08/2009
        public static string ValidateXml(string xmlSource, string xsdSource)
        {
            // NOTE, null string is more descriptive when no errors
            string errorMessage = string.Empty;

            var xmlSettings = new XmlReaderSettings();
            xmlSettings.Schemas.Add(null, xsdSource);
            xmlSettings.ValidationType = ValidationType.Schema;

            xmlSettings.ValidationEventHandler += (object sender, ValidationEventArgs e) =>
            {

                if (e.Severity == XmlSeverityType.Warning)
                {
                    errorMessage = e.Message;
                }
                else if (e.Severity == XmlSeverityType.Error)
                {
                    errorMessage = e.Message;
                }
            };

            using (var xmlRd = XmlReader.Create(xmlSource, xmlSettings))
            {
                while (xmlRd.Read()) { }
            }

            return errorMessage;
        }

        /// <summary>
        /// Validates the XML.
        /// </summary>
        /// <param name="xmlSource">The XML source.</param>
        /// <param name="xsdSource">The XSD source.</param>
        /// <returns>The object of <see cref="String"/> containing the error message; Returns empty string
        /// indicates no errors.
        /// </returns>
        /// 12/08/2009
        public static string ValidateXml(XmlReader xmlSource, XmlReader xsdSource)
        {
            // NOTE, null string is more descriptive when no errors
            string errorMessage = string.Empty;

            var xmlSettings = new XmlReaderSettings();
            xmlSettings.Schemas.Add(null, xsdSource);
            xmlSettings.ValidationType = ValidationType.Schema;

            xmlSettings.ValidationEventHandler += (object sender, ValidationEventArgs e) =>
            {

                if (e.Severity == XmlSeverityType.Warning)
                {
                    errorMessage = e.Message;
                }
                else if (e.Severity == XmlSeverityType.Error)
                {
                    errorMessage = e.Message;
                }
            };

            using (var xmlRd = XmlReader.Create(xmlSource, xmlSettings))
            {
                while (xmlRd.Read()) { }
            }

            return errorMessage;
        }

        public static void Stub()
        { 
            // do nothing
        }

    }
}
