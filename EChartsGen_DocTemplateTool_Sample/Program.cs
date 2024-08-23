using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using DocTemplateTool.Word;
using DocTemplateTool.Word.Test.Model;
using ECharts.Entities;
using NReco.PhantomJS;

namespace WordTemplateTool_EChartsSample
{
    internal class Program
    {
        private static readonly string basePath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
        private static EChartsGen.Exporter exporter;

        static void Main(string[] args)
        {
            var docinfo = GetHealthReportDocInfo();

            exporter = new EChartsGen.Exporter();
            string bloodPressureTestGraphicPath = GetPngGraphic("血压监测结果分析",docinfo.BloodPressureTestPassCount, docinfo.BloodPressureTestFailedCount);
            docinfo.BloodPressureTestGraphic = bloodPressureTestGraphicPath;

            string bloodGlucoseTestGraphicPath = GetPngGraphic("血糖监测结果分析", docinfo.BloodGlucoseTestPassCount, docinfo.BloodGlucoseTestFailedCount);
            docinfo.BloodGlucoseTestGraphic = bloodGlucoseTestGraphicPath;

            string ecgTestGraphicPath = GetPngGraphic("心电监测结果分析", docinfo.ECGTestPassCount, docinfo.ECGTestFailedCount);
            docinfo.ECGTestGraphic = ecgTestGraphicPath;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine("Generator begin");
            byte[] docFileContent;


            var result = DocTemplateTool.Word.Exporter.ExportDocxByObject(Path.Combine(basePath, "Assets", $"ReportTemplate2.docx"), docinfo, (s) => s);

            using (var memoryStream = new MemoryStream())
            {
                result.Write(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                docFileContent = memoryStream.ToArray();
            }


            File.WriteAllBytes(Path.Combine(basePath, $"Report2.docx"), docFileContent);


        }

        private static string GetPngGraphic(string title, int passCount, int failedCount)
        {
            return exporter.ExportAsync(new EChartsGen.ExportOption()
            {
                Height = 400,
                Width = 580,
                ChartOption = new ChartOption()
                {
                    title = new List<Title>()
                    {
                        new Title (){
                        text=title, left="center"}
                    },
                    tooltip = new ToolTip(),
                    legend = new Legend()
                    {
                        orient = OrientType.vertical,
                        left = "left"
                    },
                    series = new object[]
                    {
                       new {
                            name= "Access From",
                            type="pie",
                            data=new object[]
                                {
                                    new  { value= failedCount, name="异常" },
                                    new  { value= passCount, name="正常" },
                                }
                       }
                    }
                },
                OutputPath="D:/out"

            }).Result;
        }

        protected static HealthReportDocInfo GetHealthReportDocInfo()
        {
            var docinfo = new HealthReportDocInfo();

            docinfo.ClientName = "XX科技股份有限公司";
            docinfo.Title = "健康企业员工健康管理周报";
            docinfo.TimeSpan = "2023年10月19日-10月29日";
            docinfo.Count1 = 2; docinfo.Count2 = 3;
            docinfo.TotalMemberCount = 60;
            docinfo.BloodPressureTestMemberCount = 42;
            docinfo.BloodGlucoseTestMemberCount = 43;
            docinfo.ECGTestMemberCount = 30;

            docinfo.BloodPressureTestPassCount = 40;
            docinfo.BloodGlucoseTestPassCount = 39;
            docinfo.ECGTestPassCount = 28;
            docinfo.BloodPressureAnalysis = new List<AnalysisList>()
            {
                 new AnalysisList()
                 {

                      Dept="技术部", Count=8
                 },
                  new AnalysisList()
                 {

                      Dept="总经办", Count=1
                 },
                   new AnalysisList()
                 {

                      Dept="客户部", Count=2
                 }


            };

            docinfo.BloodGlucoseAnalysis = new List<AnalysisList>()
            {
                 new AnalysisList()
                 {

                      Dept="技术部", Count=4
                 },
                  new AnalysisList()
                 {

                      Dept="总经办", Count=1
                 },
                   new AnalysisList()
                 {

                      Dept="客户部", Count=1
                 }


            };

            docinfo.ECGAnalysis = new List<AnalysisList>()
            {
                 new AnalysisList()
                 {

                      Dept="技术部", Count=4
                 },
                  new AnalysisList()
                 {

                      Dept="总经办", Count=1
                 },
                   new AnalysisList()
                 {

                      Dept="客户部", Count=1
                 }


            };

            docinfo.BloodPressureList = new List<DetailList> { new DetailList() {
            Dept="技术部",
             Name="张三",
             Value="144/66",
             Result="↑"
            },
            new DetailList() {
            Dept="技术部",
             Name="李四",
             Value="150/86",
             Result="↑"
            },
             new DetailList() {
            Dept="技术部",
             Name="张伟",
             Value="149/86",
             Result="↑"
            },
            new DetailList() {
            Dept="技术部",
             Name="李青",
             Value="128/92",
             Result="↑"
            }
            };
            docinfo.BloodGlucoseList = new List<DetailList> { new DetailList() {
            Dept="技术部",
             Name="张伟",
             Value="6.3",
             Result="↑"
            },
            new DetailList() {
            Dept="技术部",
             Name="王芳",
             Value="6.5",
             Result="↑"
            }
            };
            docinfo.ECGList = new List<DetailList> { new DetailList() {
            Dept="技术部",
             Name="张敏",
             Value="122",
             Result="↑"
            },
            new DetailList() {
            Dept="技术部",
             Name="陈婷",
             Value="55",
             Result="↓"
            }
            };
            return docinfo;
        }

    }
}