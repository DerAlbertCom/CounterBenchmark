using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CounterBenchmark.Benchmarks;
using CounterBenchmark.Models;

namespace CounterBenchmark.Controllers
{
    public class BenchmarkController : Controller
    {
        // GET: Benchmark
        public ActionResult Index()
        {
            var dict = (Dictionary<string, BenchmarkModel>)(Session["Results"] ?? new Dictionary<string, BenchmarkModel>());
            return View(dict);
        }

        public async Task<ActionResult> ClientSide()
        {
            var benchmark = new EntityFrameworkClientSide();
            await Run(benchmark);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Redis()
        {
            var benchmark = new RedisIncrement();
            await Run(benchmark);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> StoredProcedure()
        {
            var benchmark = new EntityFrameworkStoredProcedure();
            await Run(benchmark);
            return RedirectToAction("Index");
        }

        public async Task<BenchmarkModel> Run(IBenchmark benchmark)
        {
            const long durations = 1000;
            var sw = new Stopwatch();
            sw.Start();
            var lastValue = await benchmark.Run(1000);
            sw.Stop();
            var model =  new BenchmarkModel(sw, durations, benchmark.GetType().Name, lastValue);
            var dict = (Dictionary<string, BenchmarkModel>) (Session["Results"] ?? new Dictionary<string, BenchmarkModel>());
            Session["Results"] = dict;
            dict[model.Name] = model;
            return model;
        }

    }
}