using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionsController(AuctionDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        var auctions = await context.Auctions
            .Include(a => a.Item)
            .OrderBy(a => a.Item.Make)
            .ToListAsync();

        var auctionDtos = auctions.Select(MappingProfiles.MapToAuctionDto).ToList();
        return Ok(auctionDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await context.Auctions
            .Include(a => a.Item)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (auction == null)
        {
            return NotFound();
        }

        var auctionDto = MappingProfiles.MapToAuctionDto(auction);
        return Ok(auctionDto);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto dto)
    {
        var auction = MappingProfiles.MapToAuction(dto);
        auction.Seller = "test"; // Temporary until auth is added

        context.Auctions.Add(auction);
        var result = await context.SaveChangesAsync() > 0;

        if (!result)
        {
            return BadRequest("Failed to create auction");
        }

        return CreatedAtAction(nameof(GetAuctionById), new { auction.Id },
            MappingProfiles.MapToAuctionDto(auction));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto dto)
    {
        var auction = await context.Auctions
            .Include(a => a.Item)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (auction == null)
        {
            return NotFound();
        }

        MappingProfiles.MapToExistingAuction(auction, dto);
        auction.UpdatedAt = DateTime.UtcNow;

        var result = await context.SaveChangesAsync() > 0;

        if (!result)
        {
            return BadRequest("Failed to update auction");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await context.Auctions.FindAsync(id);

        if (auction == null)
        {
            return NotFound();
        }

        context.Auctions.Remove(auction);
        var result = await context.SaveChangesAsync() > 0;

        if (!result)
        {
            return BadRequest("Failed to delete auction");
        }

        return NoContent();
    }
}
