namespace EfCore2.Files;

public sealed record StoredProductImage
(
    string StoredFileName,
    long SizeInBytes,
    string ContentType
);