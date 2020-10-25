using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WTProgrammingTestShihab.Models;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace WTProgrammingTestShihab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //deserialize JSON from file data
            string file = Server.MapPath(@"~/App_Data/data.json");  
            string Json = System.IO.File.ReadAllText(file);

            //deserialize JSON from file data2 
            string file2 = Server.MapPath(@"~/App_Data/data2.json"); 
            string Json2 = System.IO.File.ReadAllText(file2);

            //Merging data from both json file
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var rawData = ser.Deserialize<List<MyArray>>(Json);
            var rawData2 = ser.Deserialize<List<MyArray>>(Json2);
            rawData.AddRange(rawData2);

            List<MyArray> tempData = rawData.ToList();
            
            //Getting total number of row
            int count = rawData.Count();
            
            int i = 0, p = 0;

            //Applying tradition bubble sort to sort the list by value
            for (p = 0; p <= count - 2; p++)
            {
                for (i = 0; i <= count - 2; i++)
                {
                    if (tempData[i].value > tempData[i + 1].value)
                    {
                        var t = tempData[i + 1];
                        tempData[i + 1] = tempData[i];
                        tempData[i] = t;
                    }
                }

            }

            //Assigning the sorted data by value to the viewData 
            ViewData["sortedByValue"] = tempData;
            
            //Applying traditional bubble sort to sort the list by location
            for (p = 0; p <= count - 2; p++)
            {
                for (i = 0; i <= count - 2; i++)
                {
                    byte[] asciiBytes = Encoding.ASCII.GetBytes(rawData[i].data[0].location);
                    byte[] asciiBytes2 = Encoding.ASCII.GetBytes(rawData[i + 1].data[0].location);
                    int length = asciiBytes.Length;
                    if (length > asciiBytes2.Length)
                        length = asciiBytes2.Length;

                    //This extra loop is used if the locations have one or more same charcters at the begining of the string
                    for (int j = 0; j < length; j++)
                    {
                        if ((int)asciiBytes[j] > (int)asciiBytes2[j])
                        {
                            var q = rawData[i + 1];
                            rawData[i + 1] = rawData[i];
                            rawData[i] = q;
                            j=length;
                        }
                        else if ((int)asciiBytes[j] < (int)asciiBytes2[j])
                        {
                            j=length;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

          
            //Assigning the sorted data by Location to the view data
            ViewData["sortedByLocation"] = rawData;
            
            return View();
        }

        
    }
}