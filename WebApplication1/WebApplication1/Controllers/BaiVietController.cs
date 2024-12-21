using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using WebApplication1.Models;
using WebApplication1.ViewModels;

using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Controllers
{
    public class BaiVietController : Controller
    {
        private readonly HopAmChuan2Context _context;

        public BaiVietController(HopAmChuan2Context context)
        {
            _context = context;
        }


        // GET: BaiViet
        public IActionResult ChiTiet(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = _context.Songs.FirstOrDefault(s => s.Id == id);
            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var authorName = _context.SongAuthors
                .Where(a => a.IdSong == id)
                .Join(_context.Authors, sa => sa.IdAuthor, a => a.Id, (sa, a) => a.AuthorName)
                .FirstOrDefault() ?? "";

            var chordName = _context.SongChords
                .Where(c => c.IdSong == id)
                .Join(_context.Chords, sc => sc.IdChord, c => c.Id, (sc, c) => c.Name)
                .FirstOrDefault() ?? "";

            var categoryName = _context.SongCategories
                .Where(sc => sc.IdSong == id)
                .Join(_context.Categories, sc => sc.IdCategory, c => c.Id, (sc, c) => c.Name)
                .FirstOrDefault() ?? "";

            var singerName = _context.SongSingers
                .Where(ss => ss.IdSong == id)
                .Join(_context.Singers, ss => ss.IdSinger, s => s.Id, (ss, s) => s.Name)
                .FirstOrDefault() ?? "";

            var viewModel = new SongDetailViewModel
            {
                Song = model,
                //Lyrics = lyrics,
                AuthorName = authorName,
                ChordName = chordName,
                CategoryName = categoryName,
                SingerName = singerName
            };

            // Có thể bạn quan tâm
            ViewBag.YouCare = _context.Songs
                .OrderBy(s => Guid.NewGuid())
                .Take(7)
                .ToList();

            // Cùng thể loại
            ViewBag.CungTheLoai = _context.SongCategories
                .Where(sc => sc.IdSong == model.Id)
                .Join(_context.Categories, sc => sc.IdCategory, c => c.Id, (sc, c) => c)
                .Take(7)
                .ToList();

            return View(viewModel);
        }


        public ActionResult TimKiem(string key)
        {
            using (var ctx = new HopAmChuan2Context())
            {
                // Kiểm tra từ khóa rỗng
                if (string.IsNullOrWhiteSpace(key))
                {
                    ViewBag.Message = "Vui lòng nhập từ khóa để tìm kiếm!";
                    return View(new List<Song>()); // Trả về danh sách trống nếu không có từ khóa.
                }

                // Tìm kiếm bài hát theo tên
                var song = ctx.Songs
                    .Where(p => p.Name.Contains(key))
                    .FirstOrDefault();  // Lấy bài hát đầu tiên tìm thấy

                if (song == null)
                {
                    // Nếu không tìm thấy bài hát
                    ViewBag.Message = "Không tìm thấy bài hát nào phù hợp!";
                    return View(); // Hiển thị trang tìm kiếm với thông báo
                }
                else
                {
                    // Nếu tìm thấy bài hát, chuyển hướng đến trang chi tiết bài hát
                    return RedirectToAction("ChiTiet", "BaiViet", new { id = song.Id, metatitle = song.Tag });
                }
            }
        }



        public IActionResult Tag(string key)
        {
            string currentUnicodeKey = "";
            var LstKey = key.Split('~'); // format {key}~{id} 
            int MaBaiViet = Convert.ToInt32(LstKey[1]); // separate after ~

            var LstTag = _context.Songs.FirstOrDefault(s => s.Id == MaBaiViet).Tag.Split(',');
            foreach (var tag in LstTag)
            {
                if (StringHelper.ToUnsignString(tag.Trim()) == LstKey[0])
                {
                    currentUnicodeKey = tag.Trim();
                    break;
                }
            }

            ViewBag.key = currentUnicodeKey;

            var model = _context.Songs.Where(p => p.Name.Contains(currentUnicodeKey) || p.Content.Contains(currentUnicodeKey)).ToList();
            if (model.Count == 0)
            {
                model = _context.Songs.Where(s => s.Id == MaBaiViet).ToList();
            }


            ViewBag.sl = model.Count();
            return View(model);
        }

        public IActionResult GeneratePdf(int? id)
        {
            // Fetch the song with the given id from the context
            var song = _context.Songs.FirstOrDefault(s => s.Id == id);
            var authorName = _context.SongAuthors
                .Where(a => a.IdSong == id)
                .Join(_context.Authors, sa => sa.IdAuthor, a => a.Id, (sa, a) => a.AuthorName)
                .FirstOrDefault() ?? "";

            var chordName = _context.SongChords
                .Where(c => c.IdSong == id)
                .Join(_context.Chords, sc => sc.IdChord, c => c.Id, (sc, c) => c.Name)
                .FirstOrDefault() ?? "";

            var categoryName = _context.SongCategories
                .Where(sc => sc.IdSong == id)
                .Join(_context.Categories, sc => sc.IdCategory, c => c.Id, (sc, c) => c.Name)
                .FirstOrDefault() ?? "";

            var singerName = _context.SongSingers
                .Where(ss => ss.IdSong == id)
                .Join(_context.Singers, ss => ss.IdSinger, s => s.Id, (ss, s) => s.Name)
                .FirstOrDefault() ?? "";

            if (song == null)
            {
                // Handle case where song with the given id is not found
                return NotFound();
            }

            // Create a new PDF document with A4 size
            var document = new PdfDocument();
            var page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;

            // Define margins for the content
            const int margin = 40;
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 12);
            var nameFont = new XFont("Arial", 16, XFontStyleEx.Bold); // Larger font size for song name
            var infoFont = new XFont("Arial", 10); // Font size for additional information

            var yPos = margin;

            // Measure the width of the song name for center alignment
            var nameWidth = gfx.MeasureString(song.Name, nameFont).Width;
            var centerX = (page.Width - nameWidth) / 2; // Calculate the X coordinate for center alignment

            // Draw the song name at the top of the page with centered alignment
            gfx.DrawString(song.Name, nameFont, XBrushes.Black,
                new XRect(centerX, yPos, nameWidth, nameFont.GetHeight()),
                XStringFormats.TopLeft);
            yPos += (int)nameFont.GetHeight() + 10; // Increase yPos for the additional information

            // Prepare the additional information text
            var additionalInfo = $"Author: {authorName}\nChord: {chordName}\nCategory: {categoryName}\nSinger: {singerName}";

            // Measure the width of the additional information for right alignment
            var infoWidth = gfx.MeasureString(additionalInfo, infoFont).Width;
            var infoHeight = gfx.MeasureString(additionalInfo, infoFont).Height;

            // Split the additional information into lines
            var infoLines = additionalInfo.Split('\n');

            // Draw each line of the additional information below the song name, aligned to the right
            foreach (var line in infoLines)
            {
                gfx.DrawString(line, infoFont, XBrushes.Black,
                    new XRect(page.Width - margin - infoWidth, yPos, infoWidth, page.Height - yPos),
                    XStringFormats.TopLeft);
                yPos += (int)infoFont.GetHeight(); // Increase yPos for the next line of additional information
            }

            yPos += margin; // Increase yPos for the content

            // Split the content into lines without <p> tags
            var contentLines = Regex.Split(song.Content, @"<\/?p[^>]*>");

            // Draw each line of the content on the PDF document
            foreach (var line in contentLines)
            {
                var remainingHeight = page.Height - yPos;
                var lineHeight = (int)font.GetHeight();

                // Check if there's enough space on the current page for the line
                if (lineHeight > remainingHeight)
                {
                    // Add a new page if the current page is not enough
                    page = document.AddPage();
                    page.Size = PdfSharp.PageSize.A4;
                    gfx = XGraphics.FromPdfPage(page);
                    yPos = margin;
                }

                gfx.DrawString(line,
                    font, XBrushes.Black, new XRect(margin, yPos, page.Width - 2 * margin, lineHeight),
                    XStringFormats.TopLeft);
                yPos += lineHeight;
            }

            // Create a memory stream to store the PDF file
            var stream = new System.IO.MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;

            // Return the PDF file with the song information
            return File(stream, "application/pdf", $"{song.Name}.pdf");
        }
        

    }
}
