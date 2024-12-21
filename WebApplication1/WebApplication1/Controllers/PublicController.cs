using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    //DOCUMENT: https://www.devbridge.com/sourcery/components/jquery-autocomplete/
    public class PublicController : Controller
    {
        private readonly HopAmChuan2Context _context;

        public PublicController(HopAmChuan2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public string AutoCompleteHopAm(string query)
        {
            var result = new AutoCompleteResult();

            var queryUnsign = StringHelper.ToUnsignString(query).ToLower();
            var search = _context.Songs
                                .Where(p => p.Url.ToLower().Contains(queryUnsign))
                                .Select(s => s.Name)
                                .ToArray();

            result.suggestions = search;
            var resultReturn = JsonConvert.SerializeObject(result);
            return resultReturn;
        }

        [HttpGet]
        public string AutoCompleteNgheSi(string query) //Includes singers and composers
        {
            var result = new AutoCompleteResult();

            var queryUnsign = StringHelper.ToUnsignString(query).ToLower();

            var search = (from context in _context.Singers.ToList()
                          where StringHelper.ToUnsignString(context.Name.ToLower()).Contains(queryUnsign)
                          select context.Name).ToArray();

            result.suggestions = search;
            var resultReturn = JsonConvert.SerializeObject(result);
            return resultReturn;
        }

        [HttpGet]
        public string AutoCompleteTheLoai(string query)
        {
            var result = new AutoCompleteResult();

            var queryUnsign = StringHelper.ToUnsignString(query).ToLower();

            var search = (from context in _context.Categories.ToList()
                          where StringHelper.ToUnsignString(context.Name.ToLower()).Contains(queryUnsign)
                          select context.Name).ToArray();

            result.suggestions = search;
            var resultReturn = JsonConvert.SerializeObject(result);
            return resultReturn;
        }

        [HttpGet]
        public string AutoCompleteGiaiDieu(string query)
        {
            var result = new AutoCompleteResult();

            var queryUnsign = StringHelper.ToUnsignString(query).ToLower();

            var search = (from context in _context.Chords.ToList()
                          where StringHelper.ToUnsignString(context.Name.ToLower()).Contains(queryUnsign)
                          select context.Name).ToArray();

            result.suggestions = search;
            var resultReturn = JsonConvert.SerializeObject(result);
            return resultReturn;
        }

        [HttpGet]
        public string AutoCompleteTone(string query)
        {
            var result = new AutoCompleteResult();

            var queryUnsign = StringHelper.ToUnsignString(query).ToLower();

            var search = (from context in _context.Chords.ToList()
                          where context.Name != null &&
                                StringHelper.ToUnsignString(context.Name.ToLower()).Contains(queryUnsign)
                          select context.Name).ToArray();


            result.suggestions = search;
            var resultReturn = JsonConvert.SerializeObject(result);
            return resultReturn;
        }

        public IActionResult DongBoUrl()
        {
            try
            {
                var model = _context.Songs.Where(p => p.Url == "Url").ToList();
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        item.Url = StringHelper.ToUnsignString(item.Name);
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return NoContent();
        }
    }
}
