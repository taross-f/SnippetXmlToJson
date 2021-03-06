// <copyright file="FileWriterTest.cs" company="Microsoft">Copyright © Microsoft 2015</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnippetXmlToJson;

namespace SnippetXmlToJson.Tests
{
    [TestClass]
    [PexClass(typeof(FileWriter))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class FileWriterTest
    {

        [PexMethod]
        public void WriteJson([PexAssumeUnderTest]FileWriter target, string json)
        {
            target.WriteJson(json);
            // TODO: アサーションを メソッド FileWriterTest.WriteJson(FileWriter, String) に追加します
        }

        [PexMethod]
        public FileWriter Constructor(string path)
        {
            FileWriter target = new FileWriter(path);
            return target;
            // TODO: アサーションを メソッド FileWriterTest.Constructor(String) に追加します
        }
    }
}
