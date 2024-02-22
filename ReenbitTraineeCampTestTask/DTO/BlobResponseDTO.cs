namespace ReenbitTraineeCampTestTask.DTO
{
    public class BlobResponseDTO
    {
        public BlobResponseDTO() 
        {
            Blob = new BlobDTO();        
        }

        public string? Status { get; set; }
        public BlobDTO Blob { get; set; }
    }
}
