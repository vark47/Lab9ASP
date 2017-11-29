using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lab9.Models;

namespace Lab9.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly SongContext _context;

        public ValuesController(SongContext context)
        {
            _context = context;
        }

     
        [HttpGet]
        public IEnumerable<Song> Get()
        {
            var songs = from m in _context.Song
                         select m;

            
            return songs.ToList();
        }

        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var songs = from m in _context.Song
                         where id == m.ID
                         select m;

            return new ObjectResult(
                 songs.SingleOrDefault<Song>());
        }

       
        [HttpPost]
        public IActionResult Post([FromBody]Song song)
        {
            if (song == null)
            {
                return BadRequest();
            }

            _context.Song.Add(song);
            _context.SaveChanges();

            return new ObjectResult(song);
           
        }

        

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song song)
        {
            if (song == null || song.ID != id)
            {
                return BadRequest();
            }

            var songToChange = _context.Song.FirstOrDefault(m => m.ID == id);
            if (songToChange == null)
            {
                return NotFound();
            }

            songToChange.Title = song.Title;
            songToChange.Writer = song.Writer;
            songToChange.Composer = song.Composer;
            songToChange.ReleaseDate = song.ReleaseDate;
            songToChange.Artist = song.Artist;
            songToChange.Album = song.Album;
            songToChange.Rating = song.Rating;

            _context.Song.Update(songToChange);
            _context.SaveChanges();
            return new ObjectResult(songToChange);
        }

      
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var song = _context.Song.FirstOrDefault(m => m.ID == id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Song.Remove(song);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

