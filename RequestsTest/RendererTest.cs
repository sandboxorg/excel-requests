﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Requests;

namespace Request.Test
{
    class RendererTest
    {
        [Test]
        public void Can_Render_Simple_Array()
        {
            var token = Parser.Parse(@"[1,2,3,4,5,6]");            
            var result = ExcelRenderer.Render(token, "http://api.test.com/numbers", true);
            Assert.AreEqual(new object[,] { { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 } }, result);
        }


        [Test]
        public void Can_Render_Array_With_Object()
        {
            var token = Parser.Parse(@"[1,2, {""a"": 5}]");
            var result = ExcelRenderer.Render(token, "http://api.test.com/numbers", true);
            Assert.AreEqual(new object[,] { { 1 }, { 2 }, { "http://api.test.com/numbers#2" } }, result);
        }




        [Test]
        public void Can_Render_Simple_Array_of_Arrays()
        {
            var token = Parser.Parse(@"[[1,2,3],[4,5,6]]");
            var result = ExcelRenderer.Render(token, "http://api.test.com/numbers", true);
            Assert.AreEqual(new object[,] { { 1 , 2 , 3 }, { 4, 5, 6 } }, result);
        }


        [Test]
        public void Can_Render_DateTime()
        {
            var token = Parser.Parse(@"{""created_at"": ""2016-09-13T20:40:41Z""}");
            var result = ExcelRenderer.Render(token["created_at"], "http://api.test.com/numbers", true);
            Assert.AreEqual(new DateTime(2016, 9, 13, 20, 40, 41), result);
        }
    }
}
