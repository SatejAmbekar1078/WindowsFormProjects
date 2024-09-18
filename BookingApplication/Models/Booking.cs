namespace BookingApplication.Models
{
    public class Booking
    {
        public int Id { get; set; } // Primary Key
        public DateTime TripDate { get; set; }
        public TimeSpan PickupTime { get; set; }
        public TimeSpan DropTime { get; set; }
        public string BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string DriverName { get; set; }
        public string DriverContact { get; set; }
        public string CarType { get; set; }
        public string CarNumber { get; set; }
        public string PackageStartTime { get; set; }
        public string PackageEndTime { get; set; }
        public string PaymentMode { get; set; }
        public string VendorName { get; set; }
        public string PackType { get; set; }
        public string PackageName { get; set; }
        public DateTime TripReturnDate { get; set; }
        public string PickupPlace { get; set; }
        public string DropPlace { get; set; }
        public bool IsVIP { get; set; }
        public string BookingStatus { get; set; }
        public string PickupPlaceFromInfo { get; set; }
    }
}
