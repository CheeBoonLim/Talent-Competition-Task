/* Order Detail section */
import React from 'react';

export default class OrderDetail extends React.Component {
    render() {
        return (
            <div className="five wide column order-detail">
                <div className="ui fluid card">
                    <div className="content">
                        <h2><span className="text-color">$50 &nbsp;</span>Order Details</h2>

                        <div className="package-details">
                            <span>
                                <i className="clock outline icon"></i>
                                <span className="delivery-time">2 days</span>
                                Delivery
                    </span>

                            <span>
                                <i className="undo icon"></i>
                                5 Revisions
                    </span>

                            <h3>Movie Poster</h3>
                            <p>A3 size 300DPI Stock images</p>

                            <div className="ui list">
                                <div className="item">
                                    <i className="blue check icon"></i>
                                    <div className="content">Print-Ready</div>
                                </div>
                                <div className="item">
                                    <i className="blue check icon"></i>
                                    <div className="content">High Resolution</div>
                                </div>
                                <div className="item">
                                    <i className="blue check icon"></i>
                                    <div className="content">Commercial Use</div>
                                </div>
                            </div>

                            <div className="ui teal button">Purchase</div>
                            <div className="ui teal button"><i className="shopping cart icon"></i></div>

                        </div>
                    </div>
                    <div className="extra content">
                        <div className="ui form">
                            <div className="inline field ui right floated">
                                <span>Quantity</span>
                                <select className="ui dropdown item">
                                    <option value="1">1($50)</option>s
                                    <option value="2">2($10)</option>
                                    <option value="3">3($150)</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}