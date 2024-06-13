using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memorize.Client.Services.Implementations;
using Memorize.Client.Services.Interfaces;
using Memorize.Server.Data;
using Memorize.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public CardsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Deck>>> GetCards()
        {
            var decks = await _context.Decks.Where(x => x.UserID == AuthService.CurrentUser.ID).ToListAsync();
            if (decks == null) return NotFound();
            List<Card> cards = new List<Card>();
            foreach (var deck in decks)
            {
                cards.AddRange(_context.Cards.Where(x => x.DeckID == deck.ID));
            }
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Card>>> GetDeckCards(int id)
        {
            var cards = _context.Cards.Where(x => x.DeckID == id);
            if (cards == null) return NotFound();
            return Ok(cards);
        }

        [HttpGet("availableToday/{id}")]
        public async Task<ActionResult<List<Card>>> GetDeckCardsAvailableToday(int id)
        {
            var today = DateTime.Today;
            var cards = _context.Cards.Where(x => x.DeckID == id && x.NextAppear <= today); ;
            if (cards == null) return NotFound();
            return Ok(cards);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateCard([FromBody]Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return Ok(card);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateCard(Card card)
        {
            var cardExists = await _context.Cards.FirstOrDefaultAsync(x => x.ID == card.ID);
            if (cardExists == null) return NotFound();
            cardExists.FrontSide = card.FrontSide;
            cardExists.BackSide = card.BackSide;
            cardExists.DayCounter = card.DayCounter;
            cardExists.NextAppear = card.NextAppear;
            cardExists.Image = card.Image;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var cardExists = await _context.Cards.FirstOrDefaultAsync(x => x.ID == id);
            if (cardExists == null) return NotFound();

            _context.Cards.Remove(cardExists);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}