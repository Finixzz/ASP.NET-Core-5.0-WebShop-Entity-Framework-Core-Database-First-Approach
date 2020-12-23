using DAL.ModelHelpers;
using DAL.Models;
using DAL.ServiceFacadeInterfaces;
using DAL.ServiceInterfaces;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceFacades
{
    public class OrderFacade : IOrderFacade
    {
        private IOrderHeaderSQLRepository _orderHeaderRepository;
        private IOrderDetailSQLRepository _orderDetailRepository;
        private WebShopSampleContext _appDbContext;

        public OrderFacade(IOrderHeaderSQLRepository _orderHeaderRepository,
                          IOrderDetailSQLRepository _orderDetailRepository,
                          WebShopSampleContext _appDbContext)
        {
            this._orderHeaderRepository = _orderHeaderRepository;
            this._orderDetailRepository = _orderDetailRepository;
            this._appDbContext = _appDbContext;
        }
        public async Task<Order> SaveAsync(Order newOrder)
        {
            OrderHeader orderHeader = new OrderHeader();
            orderHeader.PayMethodId = newOrder.PayMethodId;
            orderHeader.ShipAddressId = newOrder.ShipAddressId;
            orderHeader = await _orderHeaderRepository.SaveAsync(orderHeader);

            newOrder.OrderHeaderId = orderHeader.OrderHeaderId;

            
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            for(int i = 0; i < newOrder.ItemList.Count; i++)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderHeaderId = orderHeader.OrderHeaderId;
                orderDetail.ItemId = newOrder.ItemList[i];
                orderDetail.Quantity = newOrder.QuantityList[i];
                orderDetail.SoldAtPrice = newOrder.SoldAtPriceList[i];

                orderDetails.Add(orderDetail);
            }

            _appDbContext.AddRange(orderDetails);
            await _appDbContext.SaveChangesAsync();

            for(int i = 0; i < orderDetails.Count; i++)
            {
                newOrder.OrderDetailList.Add(orderDetails[i].OrderDetailsId);
            }

            return newOrder;

        }
    }
}
