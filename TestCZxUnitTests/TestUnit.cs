using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTestCZ.Controllers;
using Xunit;

namespace TestCZxUnitTests
{
    public class TestUnit
    {
        [Fact]
        public void Post_Return_Result_OK()
        {
            var controller = new TestController();

            var left = @"IntLZXkgOiAnS2F0aWEnLCBNZXNzYWdlOiAnV2VsY29tZSBDemVjaCd9Ig ==";
            var right = @"IntLZXkgOiAnS2F0aWEnLCBNZXNzYWdlOiAnV2VsY29tZSBDemVjaCd9Ig ==";

            var postData = controller.Post(left, right);

            Assert.IsType<String>(postData);
        }

        [Fact]
        public void Post_ReturnDiff_Result_OK()
        {
            var controller = new TestController();

            var left = @"IntLZXkgOiAnS2F0aWEnLCBNZXNzYWdlOiAnV2VsY29tZSBDemVjaCd9Ig ==";
            var right = @"IntLZXkgOiAnQW5kcsOpJywgTWVzc2FnZTogJ1dlbGNvbWUgQ3plY2gnfSI=";

            var postData = controller.Post(left, right);

            Assert.IsType<Boolean>(postData == @"{ ""result"" : ""inputs are of different size"" }" ? true : null);
        }
        [Fact]
        public void Post_ReturnEqual_Result_OK()
        {
            var controller = new TestController();

            var left = @"IntLZXkgOiAnS2F0aWEnLCBNZXNzYWdlOiAnV2VsY29tZSBDemVjaCd9Ig ==";
            var right = @"IntLZXkgOiAnS2F0aWEnLCBNZXNzYWdlOiAnV2VsY29tZSBDemVjaCd9Ig ==";

            var postData = controller.Post(left, right);

            Assert.IsType<Boolean>(postData == @"{ ""result"" : ""inputs were equal"" }" ? true : null);
        }
    }
    
    
}
