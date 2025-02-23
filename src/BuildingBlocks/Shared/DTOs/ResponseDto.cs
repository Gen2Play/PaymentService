namespace Shared.DTOs;

public class ResponseDto<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public int? Status { get; set; }

    public ResponseDto()
    {
    }
    
    public ResponseDto(T? data, string? message, int status)
    {
        Data = data;
        Message = message;
        Status = status;
    }
}