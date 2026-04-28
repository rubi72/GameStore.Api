namespace GameStore.Api;

public record CreateGameDto(
  string Name,
  string Genre,
  decimal Price,
  DateOnly ReleaseDate
);