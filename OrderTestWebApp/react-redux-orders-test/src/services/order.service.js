import http from "../http-common";

class OrderDataService {
  getAll() {
    return http.get("/Orders/getAll");
  }
  get(id) {
    return http.get(`/Orders/getOrderById?id=${id}`);
  }
  create(data) {
    return http.post("/Orders/addNew", data);
  }

  update(data) {
    return http.put(`/Orders/updateOrder`, data);
  }

  delete(id) {
    return http.delete(`/orders/remove?id=${id}`);
  }

  
  findByTipe(type) {
    return http.get(`/Orders/getOrdersByType?orderType=${type}`);
  }
}

export default new OrderDataService();