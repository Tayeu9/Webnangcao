using SHOPHVT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOPHVT.Controllers
{
    public class DashboardViewModel
    {
        public int NumSP { get; set; }
        public int NumCate { get; set; }
        public int NumHD { get; set; }
        public int NumHH { get; set; }
        public List<string> NameSP { get; set; }
        public List<int?> NumOfCate { get; set; }
        public List<string> Month_STR { get; set; }
        public List<int?> Month_NUM { get; set; }
    }

    public class DashBoardController : Controller
    {
        DB_HVTShopEntities db = new DB_HVTShopEntities();

        // GET: DashBoard
        public ActionResult Index()
        {
            int numSP = db.Database.SqlQuery<int>("EXEC ProcGetRowCountSP").FirstOrDefault();
            int numCate = db.Database.SqlQuery<int>("EXEC ProcGetRowCountDM").FirstOrDefault();
            int numHD = db.Database.SqlQuery<int>("EXEC ProcGetRowCountHD").FirstOrDefault();
            int numHH = db.Database.SqlQuery<int>("EXEC ProcGetRowCountHH").FirstOrDefault();
            //Chart 1
            var chart1Data = db.Database.SqlQuery<ProcGetNameAndNumSPOfDM_Result>("ProcGetNameAndNumSPOfDM").ToList();
            var nameSP = chart1Data.Select(x => x.tenDanhMuc).ToList();
            var numOfCate = chart1Data.Select(x => x.totalQty).ToList();
            //Chart 2
            var chart2Data = db.Database.SqlQuery<ProcGetNumMonth_Result>("ProcGetNumMonth").ToList();
            var month_str = chart2Data.Select(x => x.month_str).ToList();
            var month_num = chart2Data.Select(x => x.month_num).ToList();

            DashboardViewModel viewModel = new DashboardViewModel
            {
                NumSP = numSP,
                NumCate = numCate,
                NumHD = numHD,
                NumHH = numHH,
                NameSP = nameSP,
                NumOfCate = numOfCate,
                Month_STR = month_str,
                Month_NUM = month_num
            };

            return View(viewModel);
        }
    }
}