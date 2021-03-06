// <copyright file="SnippetXmlToJsonTest.cs" company="Microsoft">Copyright © Microsoft 2015</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnippetXmlToJson;

namespace SnippetXmlToJson.Tests
{
    [TestClass]
    [PexClass(typeof(global::SnippetXmlToJson.SnippetXmlToJson))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class SnippetXmlToJsonTest
    {

        [PexMethod]
        public global::SnippetXmlToJson.SnippetXmlToJson Constructor(string path)
        {
            global::SnippetXmlToJson.SnippetXmlToJson target
               = new global::SnippetXmlToJson.SnippetXmlToJson(path);
            return target;
            // TODO: アサーションを メソッド SnippetXmlToJsonTest.Constructor(String) に追加します
        }
    }
}
