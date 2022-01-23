using JsonDiffPatchDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace WebAppTestCZ.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Welcome", "André Balconi Test" };
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public string Post(
            [FromForm(Name = "left")] string left,
            [FromForm(Name = "right")] string right
            )
        {
            try
            {
                string returnLeft = DecodeTo64(left).Replace(@"\", "");
                string returnRight = DecodeTo64(right).Replace(@"\", "");
                var result = "";
                //Case inputs are equal
                if (returnLeft == returnRight)
                {
                    result = @"{ ""result"" : ""inputs were equal"" }";
                }
                //Case inputs are not equal size
                else if (returnRight.Length != returnLeft.Length)
                {
                    result = @"{ ""result"" : ""inputs are of different size"" }";
                }
                //Case inputs has the same size
                else if (returnRight.Length == returnLeft.Length)
                {

                    var diffObj = new JsonDiffPatch();

                    var returnOne = JToken.Parse(returnLeft);
                    var returnTwo = JToken.Parse(returnRight);

                    JToken patch = diffObj.Diff(returnOne, returnTwo);


                    result = $"{patch.ToString()}{left.Length + " Caracters Left "}{right.Length + " Caracters Right "}";
                }
                else
                {
                    return BadRequest().ToString();
                }

                return result;

            }
            catch (Exception ex)
            {

                return ex.Message;
            }

            
        }

        //Function to Convert from Base64 to String(json)
        static public string DecodeTo64(string decodeData)
        {

            byte[] dadosAsBytes = System.Convert.FromBase64String(decodeData);

            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(dadosAsBytes);

            return returnValue;


        }

    }

}
