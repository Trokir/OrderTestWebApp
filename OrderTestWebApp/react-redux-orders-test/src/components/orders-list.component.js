import React, { Component } from "react";
import { connect } from "react-redux";
import { retrieveOrders, findOrdersByType } from "../actions/orders";
import MUIDataTable from "mui-datatables";

class OrdersList extends Component {
  constructor(props) {
    super(props);
    this.onChangeSearchOrderType = this.onChangeSearchOrderType.bind(this);
    this.refreshData = this.refreshData.bind(this);
    this.setActiveOrder = this.setActiveOrder.bind(this);
    this.findByType = this.findByType.bind(this);

    this.state = {
      currentOrder: null,
      currentIndex: -1,
      searchType: "",
    };
  }
  
  componentDidMount() {
    this.props.retrieveOrders();
  }

  onChangeSearchOrderType(e) {
    const searchType = e.target.value;

    this.setState({
        searchType: searchType,
    });
  }

  refreshData() {
    this.setState({
      currentOrder: null,
      currentIndex: -1,
    });
  }

  setActiveOrder(order, index) {
    this.setState({
      currentOrder: order,
      currentIndex: index,
    });
  }

  
  findByType() {
    this.refreshData();

    this.props.findOrdersByType(this.state.searchType);
  }

  render() {
    const { searchType} = this.state;
    const { orders } = this.props;
    const columns = [
      {
        name: "orderId",
        label: "ID",
        options: {
          filter: false,
          sort: false,
        },
      },
      {
        name: "orderType",
        label: "Order Type",
        options: {
          filter: true,
          sort: true,
        },
      },
      {
        name: "customerName",
        label: "Name",
        options: {
          filter: true,
          sort: true,
        },
      },
      {
        name: "createdDate",
        label: "Created Date",
        options: {
          filter: true,
          sort: true,
        },
      },
      {
        name: "createdByUserName",
        label: "Created By User Name",
        options: {
          filter: true,
          sort: true,
        },
      },
    ];
    const options = {
      filterType: "checkbox",
    };
    return (
      <div>
        <div className="col-md-8">
          <div className="input-group mb-3">
            <input
              type="text"
              className="form-control"
              placeholder="Search by orderType"
              value={searchType}
              onChange={this.onChangeSearchOrderType}
            />
            <div className="input-group-append">
              <button
                className="btn btn-outline-secondary"
                type="button"
                onClick={this.findByType}
              >
                Search
              </button>
            </div>
          </div>
        </div>
        <MUIDataTable
      title={"Orders List"}
      data={orders}
      columns={columns}
      options={options}
    />
      </div>
      
    );
  }
}


const mapStateToProps = (state) => {
  return {
    orders: state.orders,
  };
};

export default connect(mapStateToProps, { retrieveOrders, findOrdersByType })(OrdersList);