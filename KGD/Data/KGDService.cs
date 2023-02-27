using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using GridCore.Server;
using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Primitives;

namespace KGD.Data
{
    public class KGDService
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public ItemsDTO<WeatherForecast> GetKGDAsync(Action<IGridColumnCollection<WeatherForecast>> columns,
                QueryDictionary<StringValues> query)
        {
            var date = DateOnly.FromDateTime(DateTime.Now);
            var data = Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = date.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray());
            
            //var repository = new OrdersRepository(_context);
            //repository.GetAll()
            var server = new GridCoreServer<WeatherForecast>(data.Result, query,
                true, "ordersGrid", columns, 10).Sortable().Filterable().WithMultipleFilters();
            return server.ItemsToDisplay;
        }
    }
}