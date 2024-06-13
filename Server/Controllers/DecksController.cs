using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memorize.Server.Data;
using Memorize.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DecksController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public DecksController(ApiDbContext context)
        {
            _context = context;
        }

        /*
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeckDetails(int id)
        {
            var cards = _context.Cards.Where(x => x.DeckID == id);
            if (cards == null) return NotFound();
            return Ok(cards);
        }
        */

        [HttpGet]
        public async Task<ActionResult<List<Deck>>> GetDecks()
        {
            var decks = await _context.Decks.ToListAsync();
            return Ok(decks);
            
        }
        
        [HttpGet("deck/{id}")]
        public async Task<ActionResult<List<Deck>>> GetDeckDetails(int id)
        {
            var deck = await _context.Decks.FirstOrDefaultAsync(x => x.ID == id);
            if (deck == null) return NotFound();
            return Ok(deck);
        }

        
        [HttpGet("userDecks/{userId}")]
        public async Task<ActionResult<Deck>> GetUserDecks(int userId)
        {
            var decks = _context.Decks.Where(x => x.UserID == userId);
            if (decks == null) return NotFound();
            return Ok(decks);
        }

        [HttpGet("param/{query}")]
        public async Task<ActionResult<List<Deck>>> GetDecksByName(string query)
        {
            var decks = _context.Decks.Where(x => x.Name.Contains(query) && x.IsPublic == true);
            if (decks == null) return NotFound();
            return Ok(decks);
        }

        [HttpPost]
        public async Task<ActionResult<Deck>> CreateDeck(Deck deck)
        {
            _context.Decks.Add(deck);
            await _context.SaveChangesAsync();
            return Ok(deck);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeck(Deck deck)
        {
            var deckExists = await _context.Decks.FirstOrDefaultAsync(x => x.ID == deck.ID);
            if (deckExists == null) return NotFound();
            deckExists.Name = deck.Name;
            deckExists.Description = deck.Description;
            deckExists.HardRememberFactor = deck.HardRememberFactor;
            deckExists.DontRememberFactor = deck.DontRememberFactor;
            deckExists.GoodRememberFactor = deck.GoodRememberFactor;
            deckExists.EasyRememberFactor = deck.EasyRememberFactor;
            deckExists.FolderID = deck.FolderID;
            deckExists.IsPublic = deck.IsPublic;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeck(int id)
        {
            var deckExists = await _context.Decks.FirstOrDefaultAsync(x => x.ID == id);
            if (deckExists == null) return NotFound();

            _context.Decks.Remove(deckExists);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}