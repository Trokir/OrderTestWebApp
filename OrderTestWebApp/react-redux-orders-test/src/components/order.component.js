import React, { Component } from "react";
import { connect } from "react-redux";
import { updateOrder, deleteOrder } from "../actions/orders";
import OrderDataService from "../services/order.service";

class Order extends Component {
  constructor(props) {
    super(props);
    this.onChangeOrderType = this.onChangeOrderType.bind(this);
    this.onChangeCustomerName = this.onChangeCustomerName.bind(this);
    this.onChangeCreatedByUserName = this.onChangeCreatedByUserName.bind(this);
    this.getOrder = this.getOrder.bind(this);
    this.updateContent = this.updateContent.bind(this);
    this.removeOrder = this.removeOrder.bind(this);

    this.state = {
      currentOrder: {
        orderId: "",
        customerName: "",
        createdDate: "",
        createdByUserName: "",
      },
      message: "",
    };
  }

  componentDidMount() {
    this.getOrder(this.props.match.params.orderId);
  }

  onChangeOrderType(e) {
    const orderType = e.target.value;

    this.setState(function (prevState) {
      return {
        currentOrder: {
          ...prevState.currentOrder,
          orderType: orderType,
        },
      };
    });
  }

  onChangeCustomerName(e) {
    const customerName = e.target.value;

    this.setState(function (prevState) {
      return {
        currentOrder: {
          ...prevState.currentOrder,
          customerName: customerName,
        },
      };
    });
  }

  onChangeCreatedByUserName(e) {
    const createdByUserName = e.target.value;

    this.setState(function (prevState) {
      return {
        currentOrder: {
          ...prevState.currentOrder,
          createdByUserName: createdByUserName,
        },
      };
    });
  }
  

  getOrder(orderId) {
    OrderDataService.get(orderId)
      .then((response) => {
        this.setState({
          currentOrder: response.data,
        });
        console.log(response.data);
      })
      .catch((e) => {
        console.log(e);
      });
  }

  

  updateContent() {
    this.props
      .updateOrder(this.state.currentOrder.id, this.state.currentOrder)
      .then((reponse) => {
        console.log(reponse);
        
        this.setState({ message: "The order was updated successfully!" });
      })
      .catch((e) => {
        console.log(e);
      });
  }

  removeOrder() {
    this.props
      .deleteOrder(this.state.currentOrder.orderId)
      .then(() => {
        this.props.history.push("/delete");
      })
      .catch((e) => {
        console.log(e);
      });
  }

  render() {
    const { currentOrder } = this.state;

    return (
      <div>
        {currentOrder ? (
          <div className="edit-form">
            <h4>Order</h4>
            <form>
              <div className="form-group">
                <label htmlFor="orderType">Order type</label>
                <input
                  type="text"
                  className="form-control"
                  id="orderType"
                  value={currentOrder.orderType}
                  onChange={this.onChangeOrderType}
                />
              </div>
              <div className="form-group">
                <label htmlFor="customerName">Customer Name</label>
                <input
                  type="text"
                  className="form-control"
                  id="customerName"
                  value={currentOrder.customerName}
                  onChange={this.onChangeCustomerName}
                />
              </div>
              <div className="form-group">
                <label htmlFor="createdByUserName">Created By User Name</label>
                <input
                  type="text"
                  className="form-control"
                  id="createdByUserName"
                  value={currentOrder.createdByUserName}
                  onChange={this.onChangeCreatedByUserName}
                />
              </div>
            </form>

            <button
              className="badge badge-danger mr-2"
              onClick={this.removeOrder}
            >
              Delete
            </button>

            <button
              type="submit"
              className="badge badge-success"
              onClick={this.updateContent}
            >
              Update
            </button>
            <p>{this.state.message}</p>
          </div>
        ) : (
          <div>
            <br />
            <p>Please click on an Order...</p>
          </div>
        )}
      </div>
    );
  }
}

export default connect(null, { updateOrder, deleteOrder })(Order);