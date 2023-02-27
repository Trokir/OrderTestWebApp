import {
    CREATE_ORDER, 
   RETRIEVE_ORDERS ,
   UPDATE_ORDER,
  DELETE_ORDER 
  } from "./type";
  
  import OrderDataService from "../services/order.service";
  
  export const createOrder = (orderId,orderType, customerName,createdDate,createdByUserName) => async (dispatch) => {
   try {
      const res = await OrderDataService.create({orderId,orderType, customerName,createdDate,createdByUserName});
  
      dispatch({
        type: CREATE_ORDER,
        payload: res.data,
      });
  
      return Promise.resolve(res.data);
    } catch (err) {
      return Promise.reject(err);
    }
  };
  
  export const retrieveOrders = () => async (dispatch) => {
    try {
      const res = await OrderDataService.getAll();
  
      dispatch({
        type: RETRIEVE_ORDERS,
        payload: res.data,
      });
    } catch (err) {
      console.log(err);
    }
  };
  
  export const updateOrder = (data) => async (dispatch) => {
    try {
      const res = await OrderDataService.update(data);
  
      dispatch({
        type: UPDATE_ORDER,
        payload: data,
      });
  
      return Promise.resolve(res.data);
    } catch (err) {
      return Promise.reject(err);
    }
  };
  
  export const deleteOrder= (orderId) => async (dispatch) => {
    try {
      await OrderDataService.delete(orderId);
  
      dispatch({
        type: DELETE_ORDER,
        payload: { orderId },
      });
    } catch (err) {
      console.log(err);
    }
  };
  
  
  
  export const findOrdersByType= (type) => async (dispatch) => {
    try {
      const res = await OrderDataService.findByTipe(type);
  
      dispatch({
        type: RETRIEVE_ORDERS,
        payload: res.data,
      });
    } catch (err) {
      console.log(err);
    }
  };