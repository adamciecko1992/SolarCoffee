namespace solarcoffee.data.models
{
    public  class SalesOrderItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
