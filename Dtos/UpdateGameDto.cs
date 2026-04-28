namespace GameStore.Api;

public record UpdateGameDto(
  string Name,
  string Genre,
  decimal Price,
  DateOnly ReleaseDate
);
