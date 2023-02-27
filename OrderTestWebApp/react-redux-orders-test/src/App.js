import React, { Component } from "react";
import { BrowserRouter as Router, Routes , Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";

import AddOrder from "./components/add-order.component";
import Order from "./components/order.component";
import OrdersList from "./components/orders-list.component";
class App extends Component {
    render() {
      return (
        <Router>
          <nav className="navbar navbar-expand navbar-dark bg-dark">
          <Link to={"/orders"} className="navbar-brand">
            Home
          </Link>
          <div className="navbar-nav mr-auto">
            <li className="nav-item">
              <Link to={"/orders"} className="nav-link">
                Orders
              </Link>
            </li>
            <li className="nav-item">
              <Link to={"/add"} className="nav-link">
                Add
              </Link>
            </li>
          </div>
        </nav>
        <div className="container mt-3">
          <Routes >
            <Route exact path='/orders' element={<OrdersList />} />
            <Route path='/add' element={<AddOrder />} />
            <Route path='/orders/:id' element={<Order />} />
          </Routes >
        </div>
        </Router>
      );
    }
  }

export default App;