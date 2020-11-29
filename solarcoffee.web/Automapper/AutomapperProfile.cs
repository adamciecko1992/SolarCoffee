using AutoMapper;
using solarcoffee.data.models;
using solarcoffee.web.ViewModels;

namespace solarcoffee.web.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerModel>();
            CreateMap<CustomerModel, Customer>();
            CreateMap<CustomerAdress, CustomerAdressModel>();
            CreateMap<CustomerAdressModel, CustomerAdress>();
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
       
            CreateMap<ProductInventory, ProductInventoryModel>();
            CreateMap<ProductInventoryModel, ProductInventory>();
            CreateMap<ProductInventorySnapshot, ProductInventorySnapshotModel>();
            CreateMap<SalesOrder, OrderModel>();
            CreateMap<OrderModel, SalesOrder>();
            CreateMap<InvoiceModel, SalesOrder>();
            CreateMap<SalesOrder, InvoiceModel>();
            CreateMap<SalesOrderItem, SalesItemOrderModel>();
            CreateMap<SalesItemOrderModel, SalesOrderItem>();
        }
    }
}