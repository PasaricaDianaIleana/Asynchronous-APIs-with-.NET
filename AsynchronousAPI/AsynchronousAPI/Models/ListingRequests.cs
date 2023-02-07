namespace AsynchronousAPI.Models
{
    public class ListingRequests
    {
        public int Id { get; set; }
        public string? RequestBody { get; set; }
        
        public string? EstimatedCompletionTime { get; set; }

        public string? RequestStatus { get; set; }

        //used to track the status of our request
        public string? RequestId { get; set; } =  Guid.NewGuid().ToString();


    }
}
