import {
    CREATE_ORDER,
    RETRIEVE_ORDERS,
    UPDATE_ORDER,
    DELETE_ORDER
  } from "../actions/type";
  
  const initialState = [];
  
  function orderReducer(orders = initialState, action) {
    const { type, payload } = action;
  
    switch (type) {
      case CREATE_ORDER:
        return [...orders, payload];
  
      case RETRIEVE_ORDERS:
        return payload;
  
      case UPDATE_ORDER:
        return orders.map((order) => {
          if (order.orderId === payload.orderId) {
            return {
              ...order,
              ...payload,
            };
          } else {
            return order;
          }
        });
  
      case DELETE_ORDER:
        return orders.filter(({ orderId }) => orderId !== payload.orderId);
  
      
      default:
        return orders;
    }
  };
  
  export default orderReducer;