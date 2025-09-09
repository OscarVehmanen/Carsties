using AuctionService.DTOs;
using AuctionService.Entities;

namespace AuctionService.RequestHelpers;

public static class MappingProfiles
{
    public static Auction MapToAuction(CreateAuctionDto dto)
    {
        return new Auction
        {
            ReservePrice = dto.ReservePrice,
            AuctionEnd = dto.AuctionEnd,
            Status = Status.Live,
            Item = new Item
            {
                Make = dto.Make,
                Model = dto.Model,
                Year = dto.Year,
                Color = dto.Color,
                Mileage = dto.Mileage,
                ImageUrl = dto.ImageUrl
            }
        };
    }

    public static AuctionDto MapToAuctionDto(Auction auction)
    {
        return new AuctionDto
        {
            Id = auction.Id,
            ReservePrice = auction.ReservePrice,
            Seller = auction.Seller,
            Winner = auction.Winner,
            SoldAmount = auction.SoldAmount ?? 0,
            CurrentHighBid = auction.CurrentHighBid ?? 0,
            CreatedAt = auction.CreatedAt,
            UpdatedAt = auction.UpdatedAt,
            AuctionEnd = auction.AuctionEnd,
            Status = auction.Status.ToString(),
            Make = auction.Item.Make,
            Model = auction.Item.Model,
            Year = auction.Item.Year,
            Color = auction.Item.Color,
            Mileage = auction.Item.Mileage,
            ImageUrl = auction.Item.ImageUrl
        };
    }

    public static void MapToExistingAuction(Auction auction, UpdateAuctionDto dto)
    {
        auction.Item.Make = dto.Make ?? auction.Item.Make;
        auction.Item.Model = dto.Model ?? auction.Item.Model;
        auction.Item.Year = dto.Year ?? auction.Item.Year;
        auction.Item.Color = dto.Color ?? auction.Item.Color;
        auction.Item.Mileage = dto.Mileage ?? auction.Item.Mileage;
    }

    public static void MapToNewAuction(Auction auction, CreateAuctionDto dto)
    {
        auction.ReservePrice = dto.ReservePrice;
        auction.AuctionEnd = dto.AuctionEnd;
        auction.Status = Status.Live;
        auction.Item = new Item
        {
            Make = dto.Make,
            Model = dto.Model,
            Year = dto.Year,
            Color = dto.Color,
            Mileage = dto.Mileage,
            ImageUrl = dto.ImageUrl
        };
    }
}
