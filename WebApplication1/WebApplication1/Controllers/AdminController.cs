using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.ViewModels;


namespace WebApplication1.Controllers;

[CheckLoginUser]
public class AdminController : Controller
{
    private readonly HopAmChuan2Context _context;

    public AdminController(HopAmChuan2Context context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        base.ViewBag.slBaiViet = _context.Songs.Count();
        base.ViewBag.slAuthors = _context.Singers.Count();
        base.ViewBag.slCategory = _context.Categories.Count();
        base.ViewBag.slChord = _context.Chords.Count();
        return View();
    }

    public IActionResult QuanLiBaiViet()
    {
        ArticleManagement viewModel = new ArticleManagement
        {
            Songs = _context.Songs.ToList(),
            Authors = _context.Authors.ToList(),
            SongAuthors = _context.SongAuthors.ToList(),
            SongSingers = _context.SongSingers.ToList(),
            Singers = _context.Singers.ToList(),
            Categories = _context.Categories.ToList(),
            SongCategories = _context.SongCategories.ToList(),
            Chords = _context.Chords.ToList(),
            SongChords = _context.SongChords.ToList()
        };
        return View(viewModel);
    }

    public IActionResult VietBaiMoi()
    {
        return View();
    }

    [HttpPost]
    public ActionResult VietBaiMoi(Song song, IFormCollection form)
    {
        try
        {
            using (HopAmChuan2Context ctx = new HopAmChuan2Context())
            {
                song.Url = StringHelper.ToUnsignString(song.Name);
                song.Tag = StringHelper.ToUnsignString(song.Name);
                song.Content = HttpUtility.HtmlDecode(song.Content);
                song.DateTime = DateTime.Now;
                song.Activity = false;
                if (!string.IsNullOrWhiteSpace(form["YouTube"]))
                {
                    song.Link = form["YouTube"];
                }

                ctx.Songs.Add(song);

                ctx.SaveChanges();
                AddOrUpdateAuthor(ctx, form["Author"], song.Id);
                AddOrUpdateChord(ctx, form["Chord"], song.Id);
                AddOrUpdateSinger(ctx, form["Singer"], song.Id);
                AddOrUpdateCategory(ctx, form["Category"], song.Id);
                ctx.SaveChanges();
            }
            return RedirectToAction("ChiTiet", "BaiViet", new { song.Id });
        }
        catch (SqlException sqlEx)
        {

            Console.WriteLine($"SQL Exception: {sqlEx.Message}");
            throw new Exception("A database error occurred while creating a new song.", sqlEx);
        }
        catch (Exception ex)
        {

            Console.WriteLine($"General Exception: {ex.Message}");
            throw new Exception("An error occurred while creating a new song.", ex);
        }
    }


    private void AddOrUpdateAuthor(HopAmChuan2Context ctx, string authorName, int songId)
    {
        string authorName2 = authorName;
        if (!string.IsNullOrWhiteSpace(authorName2))
        {
            Author author = ctx.Authors.FirstOrDefault((Author a) => a.AuthorName == authorName2) ?? new Author
            {
                AuthorName = authorName2
            };
            if (author.Id == 0)
            {
                ctx.Authors.Add(author);
            }
            ctx.SaveChanges();
            SongAuthor songAuthor = new SongAuthor
            {
                IdSong = songId,
                IdAuthor = author.Id
            };
            ctx.SongAuthors.Add(songAuthor);
        }
    }

    private void AddOrUpdateChord(HopAmChuan2Context ctx, string chordName, int songId)
    {
        string chordName2 = chordName;
        if (!string.IsNullOrWhiteSpace(chordName2))
        {
            Chord chord = ctx.Chords.FirstOrDefault((Chord c) => c.Name == chordName2) ?? new Chord
            {
                Name = chordName2
            };
            if (chord.Id == 0)
            {
                ctx.Chords.Add(chord);
            }
            ctx.SaveChanges();
            SongChord songChord = new SongChord
            {
                IdSong = songId,
                IdChord = chord.Id
            };
            ctx.SongChords.Add(songChord);
        }
    }

    private void AddOrUpdateSinger(HopAmChuan2Context ctx, string singerName, int songId)
    {
        string singerName2 = singerName;
        if (!string.IsNullOrWhiteSpace(singerName2))
        {
            Singer singer = ctx.Singers.FirstOrDefault((Singer s) => s.Name == singerName2) ?? new Singer
            {
                Name = singerName2
            };
            if (singer.Id == 0)
            {
                ctx.Singers.Add(singer);
            }
            ctx.SaveChanges();
            SongSinger songSinger = new SongSinger
            {
                IdSong = songId,
                IdSinger = singer.Id
            };
            ctx.SongSingers.Add(songSinger);
        }
    }

    private void AddOrUpdateCategory(HopAmChuan2Context ctx, string categoryName, int songId)
    {
        string categoryName2 = categoryName;
        if (!string.IsNullOrWhiteSpace(categoryName2))
        {
            Category category = ctx.Categories.FirstOrDefault((Category c) => c.Name == categoryName2) ?? new Category
            {
                Name = categoryName2
            };
            if (category.Id == 0)
            {
                ctx.Categories.Add(category);
            }
            ctx.SaveChanges();
            SongCategory songCategory = new SongCategory
            {
                IdSong = songId,
                IdCategory = category.Id
            };
            ctx.SongCategories.Add(songCategory);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SuaBaiViet(int id)
    {
        try
        {
            Song song = await _context.Songs.FirstOrDefaultAsync((Song s) => s.Id == id);
            if (song != null)
            {
                List<Author> authors = (await _context.SongAuthors.Where((SongAuthor sa) => sa.IdSong == (int?)song.Id).ToListAsync()).Select((SongAuthor sc) => _context.Authors.FirstOrDefault((Author cat) => (int?)cat.Id == sc.IdAuthor)).ToList();
                SongChord songChord = await _context.SongChords.FirstOrDefaultAsync((SongChord sc) => sc.IdSong == (int?)song.Id);
                Chord chord = await _context.Chords.FirstOrDefaultAsync((Chord c) => (int?)c.Id == songChord.IdChord);
                List<Category> categories = (await _context.SongCategories.Where((SongCategory sc) => sc.IdSong == (int?)song.Id).ToListAsync()).Select((SongCategory sc) => _context.Categories.FirstOrDefault((Category cat) => (int?)cat.Id == sc.IdCategory)).ToList();
                List<Singer> singers = (await _context.SongSingers.Where((SongSinger ss) => ss.IdSong == (int?)song.Id).ToListAsync()).Select((SongSinger ss) => _context.Singers.FirstOrDefault((Singer s) => (int?)s.Id == ss.IdSinger)).ToList();
                ArticleManagement articleManagement = new ArticleManagement
                {
                    Songs = new List<Song> { song },
                    Authors = authors,
                    Singers = singers,
                    Categories = categories,
                    Chords = new List<Chord> { chord }
                };
                return View(articleManagement);
            }
            return NotFound();
        }
        catch (Exception ex2)
        {
            Exception ex = ex2;
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> SuaBaiViet(Song song, IFormCollection form)
    {
        Song song2 = song;
        try
        {
            using HopAmChuan2Context ctx = new HopAmChuan2Context();
            Song songUpdate = ctx.Songs.FirstOrDefault((Song i) => i.Id == song2.Id);
            if (songUpdate != null)
            {
                songUpdate.Name = song2.Name;
                songUpdate.Url = StringHelper.ToUnsignString(song2.Name);
                songUpdate.Tag = song2.Tag;
                songUpdate.Content = HttpUtility.HtmlDecode(song2.Content);
                songUpdate.DateTime = DateTime.Now;
                string authorName = form["Author"];
                Author author = ctx.Authors.FirstOrDefault((Author a) => a.AuthorName == authorName);
                if (author != null)
                {
                    foreach (SongAuthor song_author in ctx.SongAuthors.Where((SongAuthor sa) => sa.IdAuthor == (int?)author.Id))
                    {
                        song_author.IdAuthor = author.Id;
                    }
                    ctx.SaveChanges();
                }
                string chordName = form["Chord"];
                Chord chord = ctx.Chords.FirstOrDefault((Chord c) => c.Name == chordName);
                if (chord != null)
                {
                    foreach (SongChord song_chord in ctx.SongChords.Where((SongChord sc) => sc.IdChord == (int?)chord.Id))
                    {
                        song_chord.IdChord = chord.Id;
                    }
                    ctx.SaveChanges();
                }
                string singerName = form["Singer"];
                Singer singer = ctx.Singers.FirstOrDefault((Singer s) => s.Name == singerName);
                if (singer != null)
                {
                    foreach (SongSinger song_singer in ctx.SongSingers.Where((SongSinger ss) => ss.IdSinger == (int?)singer.Id))
                    {
                        song_singer.IdSinger = singer.Id;
                    }
                    ctx.SaveChanges();
                }
                string categoryName = form["Category"];
                Category category = ctx.Categories.FirstOrDefault((Category c) => c.Name == categoryName);
                if (category != null)
                {
                    foreach (SongCategory song_category in ctx.SongCategories.Where((SongCategory sc) => sc.IdCategory == (int?)category.Id))
                    {
                        song_category.IdCategory = category.Id;
                    }
                    ctx.SaveChanges();
                }
                if (ctx.ChangeTracker.HasChanges())
                {
                    await ctx.SaveChangesAsync();
                }
                base.ViewBag.Added = true;
                return RedirectToAction("ChiTiet", "BaiViet", new
                {
                    id = songUpdate.Id
                });
            }
            return NotFound();
        }
        catch (Exception)
        {
            base.ModelState.AddModelError(string.Empty, "Error occurred while updating song.");
            // Trả về View với đối tượng ArticleManagement
            return View(song2);
        }
    }


    [HttpGet]
    public IActionResult DeleteSong(int id)
    {
        try
        {
            Song song = _context.Songs.FirstOrDefault((Song s) => s.Id == id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }
        catch (Exception)
        {
            return RedirectToAction("ChiTiet", "BaiViet");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteSongConfirmed(int id)
    {
        try
        {
            Song songToDelete = _context.Songs.FirstOrDefault((Song s) => s.Id == id);
            if (songToDelete != null)
            {
                List<SongAuthor> songAuthorToDelete = _context.SongAuthors.Where((SongAuthor sa) => sa.IdSong == (int?)songToDelete.Id).ToList();
                _context.SongAuthors.RemoveRange(songAuthorToDelete);
                List<SongChord> songChordsToDelete = _context.SongChords.Where((SongChord sa) => sa.IdSong == (int?)songToDelete.Id).ToList();
                _context.SongChords.RemoveRange(songChordsToDelete);
                List<SongSinger> songSingersToDelete = _context.SongSingers.Where((SongSinger ss) => ss.IdSong == (int?)songToDelete.Id).ToList();
                _context.SongSingers.RemoveRange(songSingersToDelete);
                List<SongCategory> songCategoriesToDelete = _context.SongCategories.Where((SongCategory sc) => sc.IdSong == (int?)songToDelete.Id).ToList();
                _context.SongCategories.RemoveRange(songCategoriesToDelete);
                _context.Songs.Remove(songToDelete);
                _context.SaveChanges();
                return RedirectToAction("ChiTiet", "BaiViet");
            }
            return NotFound();
        }
        catch (Exception)
        {
            return RedirectToAction("ChiTiet", "BaiViet");
        }
    }

    [HttpPost]
    public IActionResult ChangeIsActive(int Id)
    {
        Song editEtem = _context.Songs.FirstOrDefault((Song s) => s.Id == Id);
        if (editEtem != null)
        {
            editEtem.Activity = !editEtem.Activity;
            _context.SaveChanges();
        }
        return RedirectToAction("QuanLiBaiViet", "Admin");
    }

    //Ca sĩ
    [HttpGet]
    public IActionResult QuanLiCaSi()
    {
        var model = _context.Singers.ToList();
        return View(model);
    }

    [HttpGet]
    public IActionResult CaSiMoi()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CaSiMoi(string Name, string Nationality, IFormFile SingerImage)
    {
        string imagePath = null;

        // Save the uploaded image
        if (SingerImage != null && SingerImage.Length > 0)
        {
            var fileName = Path.GetFileName(SingerImage.FileName);
            var filePath = Path.Combine("wwwroot/img", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await SingerImage.CopyToAsync(stream);
            }

            imagePath = "/img/" + fileName;
        }

        try
        {
            using (var ctx = new HopAmChuan2Context())
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    // Check if the singer already exists
                    var existingSinger = ctx.Singers.FirstOrDefault(s => s.Name == Name);

                    if (existingSinger == null)
                    {
                        // Create new singer
                        var newSinger = new Singer
                        {
                            Name = Name,
                            Content = Nationality,
                            Image = imagePath
                        };
                        ctx.Singers.Add(newSinger);
                        TempData["SuccessMessage"] = "Thêm ca sĩ thành công.";
                    }
                    else
                    {
                        // Update existing singer
                        existingSinger.Content = Nationality;
                        if (imagePath != null)
                        {
                            existingSinger.Image = imagePath;
                        }
                        TempData["SuccessMessage"] = "Cập nhật ca sĩ thành công.";
                    }

                    ctx.SaveChanges();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return RedirectToAction("QuanLiCaSi", "Admin");
    }

    [HttpGet]
    public IActionResult SuaCaSi(int id)
    {
        var singer = _context.Singers.Find(id);
        if (singer == null)
        {
            return NotFound();
        }
        return View(singer);
    }

    [HttpPost]
    public async Task<IActionResult> SuaCaSi(int id, string Name, string Nationality, IFormFile SingerImage)
    {
        var singer = await _context.Singers.FindAsync(id);
        if (singer == null)
        {
            return NotFound();
        }

        string imagePath = singer.Image; // Default to existing image path

        // Save the uploaded image
        if (SingerImage != null && SingerImage.Length > 0)
        {
            var fileName = Path.GetFileName(SingerImage.FileName);
            var filePath = Path.Combine("wwwroot/img", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await SingerImage.CopyToAsync(stream);
            }

            imagePath = "/img/" + fileName;
        }

        try
        {
            singer.Name = Name;
            singer.Content = Nationality;
            singer.Image = imagePath;

            _context.Update(singer);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cập nhật ca sĩ thành công.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật ca sĩ.";
            Console.WriteLine(ex.Message);
        }

        return RedirectToAction("QuanLiCaSi", "Admin");
    }


    [HttpGet]
    public IActionResult DeleteSinger(int id)
    {
        var singer = _context.Singers.Find(id);
        if (singer == null)
        {
            return NotFound();
        }
        return View(singer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteSingerConfirmed(int id)
    {
        try
        {
            // Find the singer to delete
            Singer singerToDelete = _context.Singers.FirstOrDefault(s => s.Id == id);

            if (singerToDelete != null)
            {
                // Remove related entities if any, assuming relationships are set up with cascade delete
                List<SongSinger> songSingersToDelete = _context.SongSingers.Where(ss => ss.IdSinger == id).ToList();
                _context.SongSingers.RemoveRange(songSingersToDelete);

                // Remove the singer
                _context.Singers.Remove(singerToDelete);
                _context.SaveChanges();

                // Redirect to the QuanLiCaSi action in the Admin controller
                return RedirectToAction("QuanLiCaSi", "Admin");
            }

            // Return a not found response if the singer does not exist
            return NotFound();
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine(ex.Message);

            // Redirect to the QuanLiCaSi action in the Admin controller
            return RedirectToAction("QuanLiCaSi", "Admin");
        }
    }




}

