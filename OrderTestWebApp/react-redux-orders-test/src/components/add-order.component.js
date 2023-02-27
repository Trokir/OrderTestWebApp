import React, { Component } from "react";
import { connect } from "react-redux";
import { createOrder } from "../actions/orders";

class AddOrder extends Component {
  constructor(props) {
    super(props);
    this.onChangeOrderType = this.onChangeOrderType.bind(this);
    this.onChangeCustomerName = this.onChangeCustomerName.bind(this);
    this.onChangeCreatedDate = this.onChangeCreatedDate.bind(this);
    this.onChangeCreatedByUserName= this.onChangeCreatedByUserName.bind(this);
    
    this.saveOrder = this.saveOrder.bind(this);
    this.newOrder = this.newOrder.bind(this);

    this.state = {
        orderId: null,
      orderType: "",
      customerName: "",
      createdDate: "",
      createdByUserName: "",
      selected: false,
    };
  }
  
  onChangeOrderType(e) {
    this.setState({
      orderType: e.target.value,
    });
  }
  onChangeCustomerName(e) {
    this.setState({
      customerName: e.target.value,
    });
  }
  onChangeCreatedDate(e) {
    this.setState({
      createdDate: e.target.value,
    });
  }
  onChangeCreatedByUserName(e) {
    this.setState({
        createdByUserName: e.target.value,
    });
  }

  saveOrder() {
    const { orderId,orderType, customerName,createdDate,createdByUserName } = this.state;

    this.props
      .createOrder(orderId,orderType, customerName,createdDate,createdByUserName)
      .then((data) => {
        this.setState({
            orderId: data.orderId,
            orderType: data.orderType,
            customerName: data.customerName,
            createdDate: data.createdDate,
            createdByUserName: data.createdByUserName
        });
        console.log(data);
      })
      .catch((e) => {
        console.log(e);
      });
  }

  newOrder() {
    this.setState({
      id: null,
      orderType: "",
      customerName: "",
      createdDate: "",
      createdByUserName: ""
    });
  }

  render() {
    return (
      <div className="submit-form">
        {this.state.submitted ? (
          <div>
            <h4>You submitted successfully!</h4>
            <button className="btn btn-success" onClick={this.newTutorial}>
              Add
            </button>
          </div>
        ) : (
          <div>
            
            <div className="form-group">
              <label htmlFor="orderType">OrderType</label>
              <input
                type="text"
                className="form-control"
                id="orderType"
                required
                value={this.state.orderType}
                onChange={this.onChangeOrderType}
                name="orderType"
              />
            </div>

            <div className="form-group">
              <label htmlFor="customerName">CustomerName</label>
              <input
                type="text"
                className="form-control"
                id="customerName"
                required
                value={this.state.customerName}
                onChange={this.onChangeCustomerName}
                name="orderType"
              />
            </div>

            <div className="form-group">
              <label htmlFor="createdDate">CreatedDate</label>
              <input
                type="date"
                className="form-control"
                id="createdDate"
                required
                value={this.state.createdDate}
                onChange={this.onChangeCreatedDate}
                name="createdDate"
              />
            </div>

            <div className="form-group">
              <label htmlFor="createdByUserName">CreatedByUserName</label>
              <input
                type="text"
                className="form-control"
                id="createdByUserName"
                required
                value={this.state.createdByUserName}
                onChange={this.onChangeCreatedByUserName}
                name="createdByUserName"
              />
            </div>

            <button onClick={this.saveOrder} className="btn btn-success">
              Submit
            </button>
          </div>
        )}
      </div>
    );
  }
}


export default connect(null, { createOrder })(AddOrder);